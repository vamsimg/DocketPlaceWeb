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
	public partial class UpdateEmail : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			CurrentEmailLabel.Text = loggedInAdmin.email;

			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Update Email";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}


		protected void UpdateEmailButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				Admin existing_admin = Admin.GetAdminsByEmail(EmailTextBox.Text);
				if (existing_admin != null)
				{
					ChangeEmailErrorLabel.Text = "A user with this email already exists. Please check it again or contact customer service.";
				}
				else
				{
					try
					{
						Admin loggedInAdmin = Helpers.GetLoggedInAdmin();

						string sanitisedEmail = EmailTextBox.Text.ToLower().Trim();

						loggedInAdmin.email = sanitisedEmail;
						loggedInAdmin.Save();

						LogEntry new_logentry = LogHelper.LogChange(1, "Email changed.", loggedInAdmin.admin_id);

						new_logentry.admin_id = loggedInAdmin.admin_id;
						new_logentry.Save();

						CurrentEmailLabel.Text = loggedInAdmin.email;
						ChangeEmailErrorLabel.Text = "Email updated successfully";

					}
					catch (Exception ex)
					{
						LogEntry new_logentry = LogHelper.LogFailure(loggedInAdmin.admin_id, 1004, ex);
						new_logentry.admin_id = loggedInAdmin.admin_id;
						new_logentry.Save();
						ChangeEmailErrorLabel.Text = "Your email change was unsuccessful. Please email help@docketplace.com.au with the following fault number: " + new_logentry.logentry_id;
					}
				}
			}
		}
	}
}