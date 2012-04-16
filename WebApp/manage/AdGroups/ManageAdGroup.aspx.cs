using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.AdGroups
{
	public partial class ManageAdGroup : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		private Company currentCompany;

		private Campaign currentCampaign;

		private AdGroup currentAdGroup;


		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();

			currentCompany = Helpers.GetCurrentCompany();


			currentAdGroup = Helpers.GetCurrentAdGroup();

			currentCampaign = currentAdGroup.campaign_;

			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, currentAdGroup.campaign_.company_) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			PopuplateBreadcrumbs();

			if (!IsPostBack)
			{
				UpdateAdGroupHyperLink.NavigateUrl = "/manage/AdGroups/UpdateAdGroupDetails.aspx?adgroup_id=" + currentAdGroup.adgroup_id;
				CreateAdMatchesHyperLink.NavigateUrl = "/manage/AdMatches/CreateAdMatches.aspx?adgroup_id=" + currentAdGroup.adgroup_id;
			}

			PopulateDetails();

			ShowAdMatches(currentAdGroup);

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
			Level3.Text = "Manage Ad Group";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		private void PopulateDetails()
		{
			AdGroupTitleFieldLabel.Text = currentAdGroup.title;
			CampaignTitleFieldLabel.Text = currentCampaign.title;

			NotesFieldLabel.Text = currentAdGroup.notes;

			CreatorFieldLabel.Text = currentAdGroup.creator_.full_name;
			CreatedFieldLabel.Text = currentAdGroup.creation_datetime.ToShortDateString();

			StartDateFieldLabel.Text = currentAdGroup.start_datetime.ToShortDateString();
			EndDateFieldLabel.Text = currentAdGroup.end_datetime.ToShortDateString();

			TimeSpan dateDiff = currentAdGroup.end_datetime.Subtract(currentAdGroup.start_datetime);

			TotalDaysFieldLabel.Text = dateDiff.Days.ToString();

		}

		public void ShowAdMatches(AdGroup current_adgroup)
		{
			foreach (AdMatch admatch in current_adgroup.AdMatchesByadgroup_)
			{
				var admatch_control = (manage.AdGroups.AdMatchUserControl)LoadControl("AdMatchUserControl.ascx");
				admatch_control.admatch = admatch;
				AdMatchesPanel.Controls.Add(admatch_control);

				Literal brclear = new Literal();

				brclear.Text = "<div class=\"brclear\"></div> ";
				AdMatchesPanel.Controls.Add(brclear);
			}
		}    
	}
}