using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;

namespace WebApp.manage.Billing
{
	public partial class CurrentBillingItems : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;



		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			PopuplateBreadcrumbs();
		}

		private void CheckPermission()
		{
			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany))
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
			Level2.Text = "Billing";
			Level2.NavigateUrl = "/manage/Billing/CurrentBillingItems.aspx";

			Literal arrows2 = new Literal();

			breadCrumbPanel.Controls.Add(Level2);


		}
	}
}