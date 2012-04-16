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
	public partial class ResetCustomerPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Reset Customer Password";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}



		protected void ResetPasswordButton_Click(object sender, EventArgs e)
		{
			string email = EmailTextBox.Text;


			//Check to see if the user exists in the database.
			Customer current_Customer = Customer.GetCustomerByEmail(EmailTextBox.Text);

			if (current_Customer == null)
			{
				FailureTextLiteral.Text = "Account not found. Please check the email address or contact the website administrator at help@docketplace.com.au";
				return;
			}
			else
			{
				try
				{
					string new_password = current_Customer.ResetPassword();
					EmailHelper.PasswordResetEmail(current_Customer.email, new_password);
					FailureTextLiteral.Text = "Your new password has been sent to the email address above. Please check your account in 5 minutes";
					EmailTextBox.Text = "";
					//LogChange(current_Customer);
				}
				catch (Exception ex)
				{

					FailureTextLiteral.Text = "Your password reset attempt was unsuccessfull. Please contact help@docketplace.com.au";
					throw;
				}
			}
		}

		//private void LogChange(Customer current_Customer)
		//{
		//     LogEntry new_logentry = LogEntry.CreateLogEntry();
		//     new_logentry.creation_datetime = DateTime.Now;
		//     new_logentry.logcode_id = 1;

		//     new_logentry.Customer_id = current_Customer.Customer_id;
		//     new_logentry.owner_id = current_Customer.Customer_id;
		//     new_logentry.description = "Password reset. IP: " + Request.UserHostAddress;
		//     new_logentry.Save();
		//}
	}
}