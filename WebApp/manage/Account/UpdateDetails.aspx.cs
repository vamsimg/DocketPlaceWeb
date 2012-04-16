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
	public partial class UpdateDetails : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();

			if (!IsPostBack)
			{
				FirstNameTextBox.Text = loggedInAdmin.first_name;
				LastNameTextBox.Text = loggedInAdmin.last_name;
				PhoneTextBox.Text = loggedInAdmin.phone;
				MobileTextBox.Text = loggedInAdmin.mobile;
			}

			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Update Details";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}


		protected void UpdateDetailsSubmitButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					loggedInAdmin.first_name = FirstNameTextBox.Text;
					loggedInAdmin.last_name = LastNameTextBox.Text;
					loggedInAdmin.phone = PhoneTextBox.Text;
					loggedInAdmin.mobile = MobileTextBox.Text;
					loggedInAdmin.Save();

					LogEntry new_logentry = LogHelper.LogChange(1, "Details changed.", loggedInAdmin.admin_id);

					new_logentry.admin_id = loggedInAdmin.admin_id;
					new_logentry.Save();

					StatusLabel.Text = "Updated Details Successfully";
				}
				catch (Exception ex)
				{
					LogEntry new_logentry = LogHelper.LogFailure(loggedInAdmin.admin_id, 1002, ex);
					new_logentry.admin_id = loggedInAdmin.admin_id;
					new_logentry.Save();
					StatusLabel.Text = "Your details were not successfully updated. Please email help@docketplace.com.au with the following fault number: " + new_logentry.logentry_id;
				}
			}
		}
	}
}