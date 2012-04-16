using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp
{
	public partial class EmailDocket : System.Web.UI.Page
	{
		private Docket currentDocket;

		protected void Page_Load(object sender, EventArgs e)
		{
			Session.Clear();
			if (HttpContext.Current.Request.QueryString["docket_id"] == null)
			{
				Response.Redirect("/status.aspx?msg=generic");
			}

			int docket_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["docket_id"]);

			currentDocket = Docket.GetDocket(docket_id);

			if (currentDocket == null)
			{
				EmailErrorLiteral.Text = "This is not a valid receipt. Please contact help@docketplace.com.au";
			}

			if (HttpContext.Current.Request.QueryString["docket_code"] == null)
			{
				Response.Redirect("/status.aspx?msg=generic");
			}

			string docket_code = HttpContext.Current.Request.QueryString["docket_code"];

			if (currentDocket.code != docket_code)
			{
				Response.Redirect("/status.aspx?msg=generic");
			}

			if (currentDocket.customer_ != null)
			{
				EmailErrorLiteral.Text = "This receipt has already been claimed. To access the e-receipt please login into the Customer Portal using the Login link above.";
			}
		}


		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				string sanitisedEmail = EmailTextBox.Text.ToLower().Trim();
				try
				{
					Customer currentCustomer = Customer.GetCustomerByEmail(sanitisedEmail);

					if (currentCustomer != null)
					{
						currentDocket.customer_id = currentCustomer.customer_id;
						currentDocket.Save();

						Member newMember = RewardsHelper.CreateMemberRecord(currentDocket.store_, currentCustomer, null);
						EmailHelper.ReceiptEmail(sanitisedEmail, currentDocket.raw_content, currentDocket.store_.company_.name);
					}
					else
					{
						Customer newCustomer = Customer.CreateCustomer();
						newCustomer.title = "";
						newCustomer.first_name = "";
						newCustomer.last_name = "";

						newCustomer.email = sanitisedEmail;
						newCustomer.email_broken = false;
						newCustomer.mobile = "";
						newCustomer.postcode = "";
						newCustomer.verification_code = Helpers.GenerateFiveDigitRandom();
						newCustomer.is_active = true;


						newCustomer.creation_datetime = DateTime.Now;

						string newPassword = Helpers.GenerateFiveDigitRandom();
						newCustomer.password_hash = BusinessHelper.computeSHAhash(newPassword, newCustomer.creation_datetime);


						newCustomer.Save();
						newCustomer.Refresh();


						Member newMember = Member.CreateMemberBycustomer_(newCustomer);
						newMember.company_id = currentDocket.store_.company_id;
						newMember.store_id = currentDocket.store_.store_id;

						newMember.reward_points = 0;
						newMember.total_revenue = currentDocket.total;
						newMember.frequency = 1;
						newMember.creation_datetime = DateTime.Now;
						newMember.Save();

						currentDocket.customer_id = newCustomer.customer_id;
						currentDocket.Save();

						//Send receipt by email.	
						EmailHelper.CustomerAccountCreationEmail(sanitisedEmail, "", newPassword, currentDocket.store_.company_.name);
						EmailHelper.ReceiptEmail(sanitisedEmail, currentDocket.raw_content, currentDocket.store_.company_.name);
						Response.Redirect("/EmailDocketSent.aspx");
					}
				}
				catch (Exception ex)
				{
					EmailErrorLiteral.Text = "An error has occurred, please contact help@docketplace.com.au";
					throw;
				}
			}

		}
	}
}