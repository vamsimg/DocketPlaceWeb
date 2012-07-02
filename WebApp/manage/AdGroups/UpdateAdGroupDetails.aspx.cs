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
	public partial class UpdateAdGroupDetails : System.Web.UI.Page
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

			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentAdGroup.campaign_.company_))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			PopuplateBreadcrumbs();

			if (!IsPostBack)
			{
				BackAdGroupHyperLink.NavigateUrl = "/manage/AdGroups/ManageAdGroup.aspx?adgroup_id=" + currentAdGroup.adgroup_id;

				if (!IsPostBack)
				{
					TitleTextBox.Text = currentAdGroup.title;
					NotesTextBox.Text = currentAdGroup.notes;

					StartDateTextBox.Text = currentAdGroup.start_datetime.ToShortDateString();
					EndDateTextBox.Text = currentAdGroup.end_datetime.ToShortDateString();
				}
			}
		}



		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Companies/ViewCompany.aspx";

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
			Level3.Text = "Update Ad Group";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		protected void UpdateAdGroupButton_Click(object sender, EventArgs e)
		{

			if (IsValid)
			{
				DateTime startDate = Convert.ToDateTime(StartDateTextBox.Text);
				DateTime endDate = Convert.ToDateTime(EndDateTextBox.Text);

				if (startDate < currentCampaign.start_datetime)
				{
					UpdateAdGroupErrorLabel.Text = "The start date is earlier than the this Campaign's start date. Go back and adjust the current campaign's start date.";
				}
				else if (endDate > currentCampaign.end_datetime)
				{
					UpdateAdGroupErrorLabel.Text = "The end date is later than the this Campaign's end date. Go back and adjust the current campaign's end date.";
				}
				else
				{
					try
					{
						currentAdGroup.title = TitleTextBox.Text;
						currentAdGroup.notes = NotesTextBox.Text;

						currentAdGroup.start_datetime = startDate;
						currentAdGroup.end_datetime = endDate;

						currentAdGroup.Save();

						foreach (AdMatch item in currentAdGroup.AdMatchesByadgroup_)
						{
							item.start_datetime = startDate;
							item.end_datetime = endDate;
							item.expiry_datetime = endDate.AddDays(-1);
							item.Save();
						}


						Response.Redirect("/manage/AdGroups/ManageAdGroup.aspx?adgroup_id=" + currentAdGroup.adgroup_id, true);

					}
					catch (Exception ex)
					{
						LogEntry error = LogHelper.LogFailure(loggedInAdmin.admin_id, 7002, ex);
						error.campaign_id = currentCampaign.campaign_id;
						error.company_id = currentCompany.company_id;
						error.adgroup_id = currentAdGroup.adgroup_id;
						error.Save();

						UpdateAdGroupErrorLabel.Text = "An error has occurred updating this Ad Group. Please contact help@docketplace.com.au with the following error code: " + error.logentry_id.ToString();
					}
				}
			}
		}
	}
}