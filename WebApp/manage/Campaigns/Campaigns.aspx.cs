using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Campaigns
{
	public partial class Campaigns : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company current_company;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			current_company = Helpers.GetCurrentCompany();

			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, current_company))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			if (!IsPostBack)
			{
				RefreshCampaignsGridView(current_company);
			}
			PopuplateBreadcrumbs();

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
			Level2.Text = "Campaigns";


			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

		}

		private void RefreshCampaignsGridView(Company current_company)
		{
			CampaignsGridView.DataSource = current_company.CampaignsBycompany_.FindAll(c => c.is_archived == false);
			CampaignsGridView.DataBind();
		}


		protected void ViewCampaignImageButton_Command(object sender, CommandEventArgs e)
		{
			ImageButton ib = (ImageButton)sender;
			int campaign_id = Convert.ToInt32(ib.CommandArgument);

			Response.Redirect("/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + campaign_id.ToString());
		}
	}
}