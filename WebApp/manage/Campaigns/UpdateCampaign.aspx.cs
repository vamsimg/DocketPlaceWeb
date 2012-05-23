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
	public partial class UpdateCampaign : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		private Company currentCompany;

		private Campaign currentCampaign;


		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();

			currentCompany = Helpers.GetCurrentCompany();

			currentCampaign = Helpers.GetCurrentCampaign();

			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			PopuplateBreadcrumbs();

			CancelHyperLink.NavigateUrl = "/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + currentCampaign.campaign_id;

			if (!IsPostBack)
			{
				TitleTextBox.Text = currentCampaign.title;
				NotesTextBox.Text = currentCampaign.notes;

				StartDateTextBox.Text = currentCampaign.start_datetime.ToShortDateString();
				EndDateTextBox.Text = currentCampaign.end_datetime.ToShortDateString();
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
			Level2.Text = "Campaign";
			Level2.NavigateUrl = "/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + currentCampaign.campaign_id.ToString();

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Update Campaign";


			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}


		protected void UpdateCampaignButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				DateTime startDate = Convert.ToDateTime(StartDateTextBox.Text);
				DateTime endDate = Convert.ToDateTime(EndDateTextBox.Text);

				try
				{
					currentCampaign.is_active = false;
					currentCampaign.is_archived = false;


					currentCampaign.title = TitleTextBox.Text;
					currentCampaign.notes = NotesTextBox.Text;

					currentCampaign.start_datetime = startDate;
					currentCampaign.end_datetime = endDate;

					currentCampaign.Save();
					Response.Redirect("/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + currentCampaign.campaign_id, true);

				}
				catch (Exception ex)
				{
					LogEntry error = LogHelper.LogFailure(loggedInAdmin.admin_id, 7002, ex);
					error.campaign_id = currentCampaign.campaign_id;
					error.company_id = currentCompany.company_id;
					error.Save();

					UpdateCampaignErrorLabel.Text = "An error has occurred creating this campaign. Please contact help@docketplace.com.au with the following error code: " + error.logentry_id.ToString();
				}

			}

		}
	}
}