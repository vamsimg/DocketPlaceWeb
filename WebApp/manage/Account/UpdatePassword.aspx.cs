using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Account
{
	public partial class UpdatePassword : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Change Password";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}


		protected void ChangePasswordButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					Admin loggedInAdmin = Helpers.GetLoggedInAdmin();
					loggedInAdmin.UpdatePassword(NewPasswordTextBox.Text);

					LogEntry new_logentry = LogHelper.LogChange(1, "Password changed.", loggedInAdmin.admin_id);

					new_logentry.admin_id = loggedInAdmin.admin_id;
					new_logentry.Save();
					PasswordChangeErrorLabel.Text = "Password updated successfully";
				}
				catch (Exception ex)
				{
					LogEntry new_logentry = LogHelper.LogFailure(loggedInAdmin.admin_id, 1003, ex);
					new_logentry.admin_id = loggedInAdmin.admin_id;
					new_logentry.Save();
					PasswordChangeErrorLabel.Text = "Your password change was unsuccessful. Please email help@docketplace.com.au with the following fault number: " + new_logentry.logentry_id;
				}
			}
		}   
	}
}