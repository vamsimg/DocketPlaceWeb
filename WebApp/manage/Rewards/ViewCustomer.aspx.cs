using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Data;

namespace WebApp.manage.Rewards
{
	public partial class ViewCustomer : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private Customer currentCustomer;
		private Member currentMember;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();



			currentCustomer = Helpers.GetCurrentCustomer();
			currentMember = currentCustomer.MembersBycustomer_.Where(m => m.company_id == currentCompany.company_id).First();

			PopuplateBreadcrumbs();

			if (currentMember == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=customernotfound");
			}

			if (!IsPostBack)
			{
				HydrateCustomer();
				PopulateDockets();

				if (currentCompany.is_rewards == true)
				{
					PointsPanel.Enabled = true;
					PointsPanel.Visible = true;

					PopulateVouchers();
					PopulatePointsLog();
					PopulateSMSBox();
					PopulateItems();
				}

			}
		}

		private void PopulateItems()
		{
			DataTable bigTable = new DataTable();

			foreach (Store item in currentCompany.StoresBycompany_)
			{
				bigTable.Merge(ReportsHelper.RunCustomerItemsQuery(item.store_id, currentCustomer.customer_id));
			}

			DocketItemsGridView.DataSource = bigTable;
			DocketItemsGridView.DataBind();
		}

		private void PopulateSMSBox()
		{
			MessageTextBox.Text = "Hello " + currentCustomer.full_name;
		}

		private void PopulateDockets()
		{
			IEnumerable<Docket> customerDockets = currentCustomer.DocketsBycustomer_.Where(d => d.store_.company_.company_id == currentCompany.company_id);

			DataTable newTable = new DataTable();

			newTable.Columns.Add("docket_id");
			newTable.Columns.Add("creation_datetime");
			newTable.Columns.Add("total");
			newTable.Columns.Add("reward_points");

			foreach (Docket item in customerDockets)
			{
				newTable.Rows.Add(item.docket_id, item.creation_datetime.ToString("dd-MMM-yyyy"), "$" + item.total.ToString("#0.00"), item.reward_points);
			}

			DocketsGridView.DataSource = newTable;
			DocketsGridView.DataBind();

			TotalRevenueLiteral.Text = currentMember.total_revenue.ToString("#0.00");
			FrequencyLiteral.Text = currentMember.frequency.ToString();
			AverageSaleLiteral.Text = currentMember.averageSale.ToString("#0.00");
		}

		private void PopulateVouchers()
		{
			IEnumerable<Voucher> customerVouchers = currentCustomer.VouchersBycustomer_.Where(v => v.company_id == currentCompany.company_id);

			DataTable newTable = new DataTable();

			newTable.Columns.Add("voucher_id");
			newTable.Columns.Add("dollar_value");
			newTable.Columns.Add("creation_datetime");
			newTable.Columns.Add("expiry_datetime");
			newTable.Columns.Add("used_datetime");



			foreach (Voucher item in customerVouchers)
			{
				string usedDateTime = "";
				if (Helpers.isDateSet(item.used_datetime))
				{
					usedDateTime = Helpers.ConvertServerDateTimetoLocal(item.used_datetime).ToString("dd-MMM-yyyy");
				}

				newTable.Rows.Add(item.voucher_id, "$" + item.dollar_value.ToString("#0"), Helpers.ConvertServerDateTimetoLocal(item.creation_datetime).ToString("dd-MMM-yyyy"),
					Helpers.ConvertServerDateTimetoLocal(item.expiry_datetime).ToString("dd-MMM-yyyy"), usedDateTime);
			}

			VouchersGridView.DataSource = newTable;
			VouchersGridView.DataBind();
		}

		private void PopulatePointsLog()
		{
			IEnumerable<PointsLog> logEntries = currentCustomer.PointsLogsBycustomer_.Where(e => e.company_id == currentCompany.company_id).OrderByDescending(e => e.creation_datetime);

			DataTable newTable = new DataTable();

			newTable.Columns.Add("pointslog_id");
			newTable.Columns.Add("creation_datetime");
			newTable.Columns.Add("description");
			newTable.Columns.Add("reward_points");
			newTable.Columns.Add("admin_name");
			newTable.Columns.Add("docket_id");


			foreach (PointsLog item in logEntries)
			{
				string adminName = "";
				if (item.admin_ != null)
				{
					adminName = item.admin_.full_name;
				}
				string docket_id = "";
				if (item.docket_ != null)
				{
					docket_id = item.docket_id.ToString();
				}
				newTable.Rows.Add(item.pointlog_id, Helpers.ConvertServerDateTimetoLocal(item.creation_datetime).ToString("dd-MMM-yyyy"), item.description, item.reward_points, adminName, docket_id);
			}

			PointsLogGridView.DataSource = newTable;
			PointsLogGridView.DataBind();
		}

		private void HydrateCustomer()
		{
			DocketPlaceCustomerIDLabel.Text = currentMember.customer_id.ToString();
			LocalCustomerIDLabel.Text = currentMember.local_customer_id;
			BarcodeTextBox.Text = currentMember.local_barcode_id;
			GradeLabel.Text = currentMember.grade;
			TitleLabel.Text = currentCustomer.title;
			FirstNameLabel.Text = currentCustomer.first_name;
			LastNameLabel.Text = currentCustomer.last_name;
			EmailLabel.Text = currentCustomer.email;
			MobileLabel.Text = currentCustomer.mobile;
			PhoneLabel.Text = currentCustomer.phone;

			SuburbLabel.Text = currentCustomer.suburb;
			StateLabel.Text = currentCustomer.state;
			PostcodeLabel.Text = currentCustomer.postcode;

			//NoSMSCheckBox.Checked = currentMember.no_sms;
			//NoEmailCheckBox.Checked = currentMember.no_email;

			CurrentPointsLabel.Text = currentMember.reward_points.ToString();
		}


		private void CheckPermission()
		{
			if (!(Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany) || Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Company/ViewCompany.aspx";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);

			HyperLink Level2 = new HyperLink();
			Level2.Text = "Rewards";
			Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Our Customers";
			Level3.NavigateUrl = "/manage/Rewards/AllCustomers.aspx";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);

			HyperLink Level4 = new HyperLink();
			Level4.Text = currentCustomer.full_name;

			Literal arrows4 = new Literal();
			arrows4.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows4);
			breadCrumbPanel.Controls.Add(Level4);
		}


		protected void AddPointsButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				int pointsToAdd = Convert.ToInt32(AddPointsTextBox.Text);
				string description = AddDescriptionTextBox.Text;

				currentMember.reward_points += pointsToAdd;

				PointsLog newEntry = currentCustomer.CreatePointsLog();
				newEntry.admin_id = loggedInAdmin.admin_id;
				newEntry.company_id = currentCompany.company_id;
				newEntry.creation_datetime = DateTime.Now;
				newEntry.reward_points = pointsToAdd;
				newEntry.description = description;
				newEntry.Save();

				currentMember.Save();

				AddPointsErrorLabel.Text = "Points added successfully.";
				CurrentPointsLabel.Text = currentMember.reward_points.ToString();

				PopulatePointsLog();
			}
		}


		protected void RemovePointsButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				int pointsToRemove = Convert.ToInt32(RemovePointsTextBox.Text);
				string description = RemoveDescriptionTextBox.Text;

				currentMember.reward_points -= pointsToRemove;

				PointsLog newEntry = currentCustomer.CreatePointsLog();
				newEntry.admin_id = loggedInAdmin.admin_id;
				newEntry.company_id = currentCompany.company_id;
				newEntry.creation_datetime = DateTime.Now;
				newEntry.reward_points = -pointsToRemove;
				newEntry.description = description;
				newEntry.Save();

				currentMember.Save();

				RemovePointsErrorLabel.Text = "Points removed successfully.";
				CurrentPointsLabel.Text = currentMember.reward_points.ToString();

				PopulatePointsLog();
			}

		}

		protected void UpdateBarcodeButton_Click(object sender, EventArgs e)
		{
			try
			{
				currentMember.local_barcode_id = BarcodeTextBox.Text;
				currentMember.Save();
				UpdateBarcodeLabel.Text = "Barcode updated successfully.";
				LogBarcodeChange();
			}
			catch
			{
				throw;
			}
		}

		private void LogBarcodeChange()
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 10;

			new_logentry.company_id = currentCompany.company_id;
			new_logentry.customer_id = currentCustomer.customer_id;
			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Barcode Updated updated. IP: " + Request.UserHostAddress;
			new_logentry.Save();
		}

		protected void SendSMSButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				string mobile = SMSHelper.sanitiseMobile(currentCustomer.mobile);

				string message = MessageTextBox.Text;

				if (message.Length == 0)
				{
					SendSMSLiteral.Text = "Message is empty";
					return;
				}
				else if (!SMSHelper.isMobileValid(mobile))
				{
					SendSMSLiteral.Text = "The customer's mobile is invalid. Check that it is 10 characters long and starts with 04. It may be their landline instead";
					return;
				}
				else
				{
					try
					{
						if (!SMSHelper.QuotaAvailable(1))
						{
							SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
						}
						else
						{
							string essendexID = SMSHelper.SendSingleSMS(mobile, message);
							if (essendexID != null)
							{
								OutgoingSMS newMessage = OutgoingSMS.CreateOutgoingSMSByadmin_(loggedInAdmin);

								newMessage.count = 1;
								newMessage.message_text = message;
								newMessage.notes = "";
								newMessage.sent_datetime = DateTime.Now;
								newMessage.receipient_list = currentCustomer.mobile;
								newMessage.verification_sms = Helpers.GenerateFiveDigitRandom();
								newMessage.response_list = essendexID;

								newMessage.Save();
								newMessage.Refresh();

								BillingItem newItem = BillingItem.CreateBillingItemBycompany_(currentCompany);
								newItem.description = "1 SMS sent to " + currentCustomer.full_name;
								newItem.quantity = 1;
								newItem.unit_cost = SMSHelper.SMScost;
								newItem.total_amount = newItem.quantity * newItem.unit_cost;

								newItem.creation_datetime = DateTime.Now;

								newItem.Save();
								SendSMSLiteral.Text = "SMS successfully sent.";
							}
							else
							{
								SendSMSLiteral.Text = "SMS Failed";
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

		//protected void UpdateCommsButton_Click(object sender, EventArgs e)
		//{
		//     if (IsValid)
		//     {
		//          currentMember.no_sms = NoSMSCheckBox.Checked;
		//          currentMember.no_email = NoEmailCheckBox.Checked;
		//          currentMember.Save();
		//     }
		//}
	}
}