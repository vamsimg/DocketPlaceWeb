using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using System.Collections;
using WebApp.AppCode;

namespace WebApp.manage.Campaigns
{
	public partial class CreateNewCampaign : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		private Company currentCompany;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();

			currentCompany = Helpers.GetCurrentCompany();

			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
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
			Level2.NavigateUrl = "/manage/Campaigns/Campaigns.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Create a New Campaign";


			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		private void ShowOwnStores()
		{
			StoresGridView.DataSource = currentCompany.StoresBycompany_;
			StoresGridView.DataBind();
		}

		private void RefreshAdLibrary()
		{
			AdLibraryListView.DataSource = currentCompany.UploadedAdsBycompany_.Where(p => p.is_active == true);
			AdLibraryListView.DataBind();
		}


		protected void DetailsNextButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{

					if (SetupRadioButtonList.SelectedValue == "advanced")
					{
						Campaign advancedCampaign = CreateANewCampaign();
						Response.Redirect("/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + advancedCampaign.campaign_id.ToString());
					}
					else
					{
						ShowOwnStores();
						DetailsPanel.Visible = false;
						StoresPanel.Visible = true;
					}
				}
				catch (Exception ex)
				{
					LogEntry error = LogHelper.LogFailure(loggedInAdmin.admin_id, 7001, ex);
					error.company_id = currentCompany.company_id;
					error.Save();

					CreateCampaignErrorLabel.Text = "An error has occurred creating this campaign. Please contact help@docketplace.com.au with the following error code: " + error.logentry_id.ToString();
				}
			}

		}

		private Campaign CreateANewCampaign()
		{
			Campaign new_campaign = Campaign.CreateCampaign();

			new_campaign.company_id = currentCompany.company_id;
			new_campaign.creator_id = loggedInAdmin.admin_id;

			new_campaign.title = TitleTextBox.Text;
			new_campaign.notes = NotesTextBox.Text;
			new_campaign.budget = 0;

			new_campaign.start_datetime = Convert.ToDateTime(StartDateTextBox.Text);
			new_campaign.end_datetime = Convert.ToDateTime(EndDateTextBox.Text);
			new_campaign.creation_datetime = DateTime.Now;

			new_campaign.is_active = true;
			new_campaign.is_archived = false;

			new_campaign.Save();
			new_campaign.Refresh();
			return new_campaign;
		}



		protected void BackButton_Click(object sender, EventArgs e)
		{
			AdLibraryPanel.Visible = false;
			StoresPanel.Visible = true;
		}


		protected void SelectAdsButton_Click(object sender, EventArgs e)
		{
			List<int> stores = new List<int>();
			foreach (GridViewRow row in StoresGridView.Rows)
			{
				CheckBox store_checked = (CheckBox)row.FindControl("StoreSelectCheckBox");
				if (store_checked.Checked == true)
				{
					stores.Add(Convert.ToInt32(row.Cells[1].Text));
				}
			}

			if (stores.Count != 0)
			{
				StoresPanel.Visible = false;
				AdsPanel.Visible = true;
				RefreshAdLibrary();
			}
			else
			{
				StoreSelectErrorLabel.Text = "No stores selected.";
			}
		}


		protected void SaveAdsButton_Click(object sender, EventArgs e)
		{
			int ad_counter = 0;

			Hashtable ad_list = new Hashtable();

			foreach (ListViewItem item in AdLibraryListView.Items)
			{

				CheckBox SelectAdCheckBox = item.FindControl("SelectAdCheckBox") as CheckBox;
				if (SelectAdCheckBox.Checked)
				{
					ad_counter++;

					HiddenField hf = item.FindControl("UploadedAdIDHiddenField") as HiddenField;
					int uploadedad_id = Convert.ToInt32(hf.Value);

					TextBox tb = item.FindControl("QuantityTextBox") as TextBox;

					int quantity = Convert.ToInt32(tb.Text) * 100;

					ad_list.Add(uploadedad_id, quantity);
				}
			}

			if (ad_counter == 0)
			{
				SelectAdsErrorLabel.Text = "No Ads were selected.";
			}
			else
			{
				List<int> stores = new List<int>();
				foreach (GridViewRow row in StoresGridView.Rows)
				{
					CheckBox store_checked = (CheckBox)row.FindControl("StoreSelectCheckBox");
					if (store_checked.Checked == true)
					{
						stores.Add(Convert.ToInt32(row.Cells[1].Text));
					}
				}

				Campaign simpleCampaign = CreateANewCampaign();

				AdGroup newAdGroup = simpleCampaign.CreateAdGroup();

				newAdGroup.title = "Default";

				newAdGroup.budget = 0;

				newAdGroup.start_datetime = Convert.ToDateTime(StartDateTextBox.Text);
				newAdGroup.end_datetime = Convert.ToDateTime(EndDateTextBox.Text);

				newAdGroup.creator_id = loggedInAdmin.admin_id;
				newAdGroup.creation_datetime = DateTime.Now;


				newAdGroup.is_active = false;

				newAdGroup.Save();
				newAdGroup.Refresh();


				foreach (int store_id in stores)
				{
					AdMatch newAdMatch = newAdGroup.CreateAdMatch();
					try
					{
						newAdMatch.store_id = store_id;
						newAdMatch.is_published = true;
						newAdMatch.is_approved = true;
						newAdMatch.is_active = false;
						newAdMatch.start_datetime = newAdGroup.start_datetime;
						newAdMatch.end_datetime = newAdGroup.end_datetime;

						newAdMatch.creation_datetime = DateTime.Now;
						newAdMatch.expiry_datetime = DateTime.Now + new TimeSpan(14, 0, 0, 0, 0);
						newAdMatch.Save();


						foreach (DictionaryEntry entry in ad_list)
						{
							RequestedAd new_request = newAdMatch.CreateRequestedAd();
							try
							{
								new_request.uploadedad_id = (int)entry.Key;

								new_request.num_wanted = (int)entry.Value;
								new_request.num_printed = 0;

								TimeSpan active_days = newAdMatch.end_datetime - newAdMatch.start_datetime;

								new_request.daily_quota = new_request.num_wanted / active_days.Days;

								new_request.is_active = false;
								new_request.Save();
							}
							catch (Exception ex)
							{
								SelectAdsErrorLabel.Text = "1. An error occurred generating ad matches. Please try again";
							}
						}
					}
					catch (Exception ex)
					{
						SelectAdsErrorLabel.Text = "2. An error occurred generating ad matches. Please try again";
					}

				}

				Response.Redirect("/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + simpleCampaign.campaign_id.ToString());
			}
		}
	}
}