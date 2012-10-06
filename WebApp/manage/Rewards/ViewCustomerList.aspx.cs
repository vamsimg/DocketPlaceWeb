using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailChimp.Types;
using WebApp.AppCode;
using DocketPlace.Business;
using System.Text;
using System.Data;

namespace WebApp.manage.Rewards
{
	public partial class ViewCustomerList : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private CustomerList currentList;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			currentList = Helpers.GetCurrentList();


			CheckPermission();
			PopuplateBreadcrumbs();
			EnableMailchimp();

			if (!IsPostBack)
			{
				PopulateCustomers();
			}

		}

		private void EnableMailchimp()
		{
			if (!String.IsNullOrEmpty(currentCompany.mailchimp_apikey) && !String.IsNullOrEmpty(currentCompany.mc_masterlist_id))
			{
				MailchimpPanel.Visible = true;
				MailchimpPanel.Enabled = true;
			}
		}

		private void PopulateCustomers()
		{
			TitleLabel.Text = currentList.title;
			NotesLabel.Text = currentList.notes;
			CustomersGridView.DataSource = currentList.GetCustomerRecords();
			CustomersGridView.DataBind();
		}

		private void CheckPermission()
		{
			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			if (currentList.company_id != currentCompany.company_id)
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			if (Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany) && currentCompany.smsEnabled)
			{
				SMSPanel.Visible = true;
				SMSPanel.Enabled = true;
			}
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Companies/ViewCompany.aspx";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);

			HyperLink Level2 = new HyperLink();
			Level2.Text = "Customers & Rewards";
			Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "View Customer List";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}


		protected void SendSMSButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				string message = MessageTextBox.Text;

				if (message.Length == 0)
				{
					SendSMSLiteral.Text = "Message is empty";
				}
				else
				{
					try
					{
						List<string> validMobiles = CalculateSMSCosts();

						if (validMobiles.Count == 0)
						{
							SendSMSLiteral.Text = "No valid mobile numbers selected.";
						}
						else
						{
							var mobiles = new StringBuilder();

							foreach (string item in validMobiles)
							{
								mobiles.Append(item).Append(",");
							}

							if (!SMSHelper.QuotaAvailable(validMobiles.Count))
							{
								SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
							}
							else
							{
								OutgoingSMS newMessage = OutgoingSMS.CreateOutgoingSMSByadmin_(loggedInAdmin);

								newMessage.count = validMobiles.Count;
								newMessage.message_text = message;
								newMessage.notes = NotesTextBox.Text + "\n" + currentList.notes;
								newMessage.sent_datetime = DateTime.Now;
								newMessage.receipient_list = mobiles.ToString().TrimEnd(',');
								newMessage.verification_sms = Helpers.GenerateFiveDigitRandom();
								newMessage.customerlist_id = currentList.customerlist_id;

								newMessage.Save();
								newMessage.Refresh();

								try
								{
									newMessage.response_list = SMSHelper.SendBulkSMS(validMobiles, message, newMessage.outgoingSMS_id);
									newMessage.Save();

									BillingItem newItem = BillingItem.CreateBillingItemBycompany_(currentCompany);

									newItem.description = validMobiles.Count.ToString() + " Bulk SMS sent.";
									newItem.quantity = validMobiles.Count;
									newItem.unit_cost = SMSHelper.SMScost;
									newItem.total_amount = newItem.quantity * newItem.unit_cost;

									newItem.creation_datetime = DateTime.Now;

									newItem.Save();
									newItem.Refresh();

									newMessage.billingitem_id = newItem.billingitem_id;
									newMessage.Save();

									SendSMSLiteral.Text = validMobiles.Count.ToString() + " messages sent successfully.";

									EmailHelper.SendGenericEmail("vamsi@docketplace.com.au", "Bulk SMS sent by " + currentCompany.name, "");
								}
								catch
								{
									newMessage.Delete();
									SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
								}

							}
						}
					}
					catch
					{
						SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
						throw;
					}
				}
			}
		}


		protected void CalculateCostButton_Click(object sender, EventArgs e)
		{
			List<string> validMobiles = CalculateSMSCosts();
		}

		private List<string> CalculateSMSCosts()
		{
			int invalidMobilesCount = 0;

			List<string> validMobiles = new List<string>();

			string invalidMobiles = "";

			int noSMSCount = 0;

			foreach (DataRow row in currentList.GetCustomerRecords().Rows)
			{
				if (row["mobile"] != DBNull.Value)
				{
					string dirtyMobile = SMSHelper.sanitiseMobile((string)row["mobile"]);
					string cleanMobile = SMSHelper.sanitiseMobile(dirtyMobile);


					if (dirtyMobile != "")
					{
						if (!SMSHelper.isMobileValid(cleanMobile) || (bool)row["mobile_broken"])
						{
							invalidMobilesCount++;
							invalidMobiles += "," + dirtyMobile;

						}
						else if ((bool)row["no_sms"])
						{
							noSMSCount++;

						}
						else
						{
							validMobiles.Add(cleanMobile);
						}
					}
				}
			}

			decimal totalCost = (decimal)(validMobiles.Count * SMSHelper.SMScost);

			SMSCostLiteral.Text = validMobiles.Count.ToString() + " valid mobile numbers @ $" + SMSHelper.SMScost.ToString("#0.00") + "  each = $" + totalCost.ToString("#0.00");

			if (invalidMobilesCount > 0)
			{
				SMSCostLiteral.Text += "<br /> Also " + invalidMobilesCount.ToString() + " invalid number(s) which have not been included.";
			}

			if (noSMSCount > 0)
			{
				SMSCostLiteral.Text += "<br /> Also " + noSMSCount.ToString() + " unsubscribed number(s) which have not been included.";
			}

			LogHelper.WriteError("Dirty Mobiles: " + invalidMobiles);
			return validMobiles;
		}


		protected void PreviewSMSButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				PreviewSMSLiteral.Text = MessageTextBox.Text + "Unsub:Reply NO";
			}
		}

		protected void MailchimpButtonButton_Click(object sender, EventArgs e)
		{
			MailChimp.MCApi chimp = new MailChimp.MCApi(currentCompany.mailchimp_apikey, true);

			try
			{
				bool deleted = chimp.ListInterestGroupingDel(currentList.mc_grouping_id);
			}
			catch (Exception ex)
			{
				//Do nothing.
			}

			try
			{
				string groupTitle = currentList.customerlist_id.ToString() + " " + currentList.title;

				List<string> groupsTemp = new List<string>();
				groupsTemp.Add(groupTitle);
				int groupingID = chimp.ListInterestGroupingAdd(currentCompany.mc_masterlist_id, groupTitle, List.GroupingType.Hidden, groupsTemp);

				currentList.mc_grouping_id = groupingID;
				currentList.Save();

				// Setup Subscribe Options that will be used for all records add or updated during the BatchListSubscribe
				var options = new List.SubscribeOptions { DoubleOptIn = false, EmailType = List.EmailType.Html, UpdateExisting = true, ReplaceInterests = true };


				var newGroupings = new List<List.Grouping>() 
                    {
                         new List.Grouping(groupTitle, new string[] {groupTitle}),                  
                    };



				var batch = new List<List.Merges>();

				DataTable customers = currentList.GetCustomerRecords();

				foreach (DataRow row in customers.Rows)
				{
					if (!row.IsNull("email"))
					{
						if (!((bool)row["no_email"] || (bool)row["email_broken"]))
						{
							string first_name = "";
							string last_name = "";

							if (!row.IsNull("first_name"))
							{
								first_name = (string)row["first_name"];
							}

							if (!row.IsNull("last_name"))
							{
								last_name = (string)row["last_name"];
							}

							batch.Add(new List.Merges((string)row["email"], List.EmailType.Html, newGroupings)
				          {
				                  {"FNAME", first_name},
				                  {"LNAME", last_name},							
				          }
							);
						}
					}
				}

				var returned = chimp.ListBatchSubscribe(currentCompany.mc_masterlist_id, batch, options);

				MailchimpErrorLabel.Text += "Email records Added " + returned.AddCount + "</br>";
				MailchimpErrorLabel.Text += "Email records Updated " + returned.UpdateCount + "</br>";
				MailchimpErrorLabel.Text += "Email records Errors " + returned.ErrorCount + "</br>";

				LogHelper.WriteError("Mailchimp sync for " + currentCompany.name);
				foreach (var error in returned.Errors)
				{
					LogHelper.WriteError("ERROR: " + error.Email + " " + error.Message + "\r\n");
				}

			}
			catch (MailChimp.Types.MCException ex)
			{
				MailchimpErrorLabel.Text = ex.Message;
			}
			catch (Exception ex)
			{
				LogHelper.WriteError(ex.ToString());
				MailchimpErrorLabel.Text = "Mailchimp group not updated successfully. Please email help@docketplace.com.au.";
			}
		}
	}
}