using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage
{
	public partial class Dashboard : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			PopuplateBreadcrumbs();

			RefreshCampaignsGridView(currentCompany);

		}

		private void CheckPermission()
		{
			if (Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/manage/Rewards/RewardsHome.aspx");
			}
			else if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Dashboard";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}

		private void RefreshCampaignsGridView(Company currentCompany)
		{
			CampaignsGridView.DataSource = currentCompany.CampaignsBycompany_.FindAll(c => c.is_archived == false && c.start_datetime < DateTime.Now && c.end_datetime > DateTime.Now);
			CampaignsGridView.DataBind();
		}
	}
}