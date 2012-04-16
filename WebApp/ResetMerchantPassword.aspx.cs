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
	public partial class ResetMerchantPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Reset Merchant Password";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}



		protected void ResetPasswordButton_Click(object sender, EventArgs e)
		{
			string email = EmailTextBox.Text;


			//Check to see if the user exists in the database.
			Admin current_admin = Admin.GetAdminsByEmail(EmailTextBox.Text);

			if (current_admin == null)
			{
				FailureTextLiteral.Text = "Account not found. Please check the email address or contact the website administrator";
				return;
			}
			else
			{
				try
				{
					string new_password = current_admin.ResetPassword();
					EmailHelper.PasswordResetEmail(current_admin.email, new_password);
					FailureTextLiteral.Text = "Your new password has been sent to the email address above. Please check your account in 5 minutes";
					EmailTextBox.Text = "";
					LogChange(current_admin);
				}
				catch (Exception ex)
				{
					FailureTextLiteral.Text = "Your password reset attempt was unsuccessfull. Please contact help@docketplace.com.au";
					throw ex;
				}
			}
		}

		private void LogChange(Admin current_admin)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 1;

			new_logentry.admin_id = current_admin.admin_id;
			new_logentry.owner_id = current_admin.admin_id;
			new_logentry.description = "Password reset. IP: " + Request.UserHostAddress;
			new_logentry.Save();
		}
	}
}