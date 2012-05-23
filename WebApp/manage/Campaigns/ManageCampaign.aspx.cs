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
	public partial class ManageCampaign : System.Web.UI.Page
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

			if (!IsPostBack)
			{
				PopulateDetails();

				UpdateCampaignHyperLink.NavigateUrl = "/manage/Campaigns/UpdateCampaign.aspx?campaign_id=" + currentCampaign.campaign_id;

				FillNewAdGroup(currentCampaign);



				RefreshAdGroupsGridView(currentCampaign);
			}

			//Clear error label
		}

		private void PopulateDetails()
		{
			TitleFieldLabel.Text = currentCampaign.title;
			NotesFieldLabel.Text = currentCampaign.notes;

			CreatorFieldLabel.Text = currentCampaign.creator_.full_name;
			CreatedFieldLabel.Text = currentCampaign.creation_datetime.ToShortDateString();

			StartDateFieldLabel.Text = currentCampaign.start_datetime.ToShortDateString();
			EndDateFieldLabel.Text = currentCampaign.end_datetime.ToShortDateString();
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
			Level2.NavigateUrl = "/manage/Campaigns/Campaigns.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Manage Campaign";


			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		private void FillNewAdGroup(Campaign current_campaign)
		{
			StartDateTextBox.Text = current_campaign.start_datetime.ToShortDateString();
			EndDateTextBox.Text = current_campaign.end_datetime.ToShortDateString();


			//AvailableBudgetFieldLabel.Text = "$" + available_budget.ToString();
			//if (available_budget > 10)
			//{
			//     BudgetRangeValidator.ErrorMessage = "Please enter an amount between $10 and $" + available_budget.ToString() + ". The Ad Group budget must be less than or equal to the available budget below.";
			//     BudgetRangeValidator.MaximumValue = available_budget.ToString();
			//}
			//else
			//{
			//     BudgetRangeValidator.ErrorMessage = "Please enter an amount between $10 and $" + available_budget.ToString() + ". The Ad Group budget must be less than or equal to the available budget below.";
			//     BudgetRangeValidator.MaximumValue = "10";
			//     CreateAdGroupPopupButton.Enabled = false;
			//     GenericErrorLabel.Text = "Campaign Budget has been filled";
			//}

		}

		private void RefreshAdGroupsGridView(Campaign current_campaign)
		{
			AdGroupsGridView.DataSource = current_campaign.AdGroupsBycampaign_;
			AdGroupsGridView.DataBind();
		}


		protected void CreateAdGroupButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					AdGroup new_adgroup = currentCampaign.CreateAdGroup();

					new_adgroup.title = TitleTextBox.Text;
					new_adgroup.notes = NotesTextBox.Text;
					new_adgroup.budget = 0;

					new_adgroup.start_datetime = Convert.ToDateTime(StartDateTextBox.Text);
					new_adgroup.end_datetime = Convert.ToDateTime(EndDateTextBox.Text);

					new_adgroup.creator_id = loggedInAdmin.admin_id;
					new_adgroup.creation_datetime = DateTime.Now;


					new_adgroup.is_active = false;

					new_adgroup.Save();
					new_adgroup.Refresh();
					Response.Redirect("/manage/Adgroups/ManageAdGroup.aspx?adgroup_id=" + new_adgroup.adgroup_id);
				}
				catch (Exception ex)
				{
					NewAGroupModalPopupExtender.Show();
					CreateAdGroupErrorLabel.Text = ErrorHelper.generic;
				}
			}

		}  
	}
}