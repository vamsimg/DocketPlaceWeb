using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Collections;
using System.Data;

namespace WebApp.manage.AdMatches
{
	public partial class CreateAdMatches : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		private AdGroup current_adgroup;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			current_adgroup = Helpers.GetCurrentAdGroup();

			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, current_adgroup.campaign_.company_) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			if (!IsPostBack)
			{
				ShowOwnStores();
			}
			PopuplateBreadcrumbs();

			//if (current_adgroup.campaign_.company_.is_retailer == false)
			//{
			//     ShowOwnButton.Enabled = false;
			//}

			//AdGroupBudgetLabel.Text = "$" + current_adgroup.budget;

			//PopulateExistingRequestedAds(current_adgroup);
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
			Level2.NavigateUrl = "/manage/Campaigns/ManageCampaign.aspx?campaign_id=" + current_adgroup.campaign_id.ToString();

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Ad Group";
			Level3.NavigateUrl = "manage/Adgroups/ManageAdGroup.aspx?adgroup_id=" + current_adgroup.adgroup_id.ToString();

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);

			HyperLink Level4 = new HyperLink();
			Level4.Text = "Ad Placement";

			Literal arrows4 = new Literal();
			arrows4.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows4);
			breadCrumbPanel.Controls.Add(Level4);
		}


		/// <summary>
		/// Show all stores owned by the Company.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ShowOwnButton_Click(object sender, EventArgs e)
		{
			ShowOwnStores();
		}

		private void ShowOwnStores()
		{
			StoresGridView.DataSource = current_adgroup.campaign_.company_.StoresBycompany_;
			StoresGridView.DataBind();

			RefreshAdLibrary(current_adgroup, 1);
		}

		/// <summary>
		/// Show all Stores in the system.
		/// TODO: Replace this button with a proper search module.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		//protected void ShowAllButton_Click(object sender, EventArgs e)
		//{
		//     StoresGridView.DataSource = Store.GetStores().Where(s => s.company_id != current_adgroup.campaign_.company_id);
		//     StoresGridView.DataBind();

		//     if (current_adgroup.campaign_.company_.is_retailer == true)
		//     {
		//          RefreshAdLibrary(current_adgroup, 5);
		//     }
		//     else
		//     {
		//          RefreshAdLibrary(current_adgroup, 10);
		//     }
		//}


		private void RefreshAdLibrary(AdGroup current_adgroup, int ad_price)
		{
			//PriceLabel.Text = ad_price.ToString() + "c";
			AdLibraryListView.DataSource = current_adgroup.campaign_.company_.UploadedAdsBycompany_.Where(p => p.is_active == true);
			AdLibraryListView.DataBind();
		}

		protected void SelectAdsButton_Click(object sender, EventArgs e)
		{
			ClearErrorMessages();

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
			}
			else
			{
				StoreSelectErrorLabel.Text = "No stores selected.";
			}
		}


		protected void ClearErrorMessages()
		{
			StoreSelectErrorLabel.Text = "";
			SelectAdsErrorLabel.Text = "";

		}

		protected void BackButton_Click(object sender, EventArgs e)
		{
			ClearErrorMessages();
			StoresPanel.Visible = true;
			AdsPanel.Visible = false;

		}
		protected void SaveAdsButton_Click(object sender, EventArgs e)
		{
			ClearErrorMessages();
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

				AdGroup current_adgroup = Helpers.GetCurrentAdGroup();


				foreach (int store_id in stores)
				{
					AdMatch new_admatch = current_adgroup.CreateAdMatch();
					try
					{
						new_admatch.store_id = store_id;
						new_admatch.is_published = true;
						new_admatch.is_approved = true;
						new_admatch.is_active = false;
						new_admatch.start_datetime = current_adgroup.start_datetime;
						new_admatch.end_datetime = current_adgroup.end_datetime;

						new_admatch.creation_datetime = DateTime.Now;
						new_admatch.expiry_datetime = DateTime.Now + new TimeSpan(14, 0, 0, 0, 0);
						new_admatch.Save();


						foreach (DictionaryEntry entry in ad_list)
						{
							RequestedAd new_request = new_admatch.CreateRequestedAd();
							try
							{
								new_request.uploadedad_id = (int)entry.Key;

								new_request.num_wanted = (int)entry.Value;
								new_request.num_printed = 0;

								TimeSpan active_days = new_admatch.end_datetime - new_admatch.start_datetime;

								new_request.daily_quota = new_request.num_wanted / active_days.Days;

								new_request.is_active = false;
								new_request.Save();
							}
							catch (Exception ex)
							{
								PageErrorLabel.Text = "1. An error occurred generating ad requests. Please try again";
							}
						}
					}
					catch (Exception ex)
					{
						PageErrorLabel.Text = "2. An error occurred generating ad matches. Please try again";
					}

				}

				StoresPanel.Visible = false;
				AdsPanel.Visible = false;

				Response.Redirect("/manage/Adgroups/ManageAdGroup.aspx?adgroup_id=" + current_adgroup.adgroup_id.ToString());
			}
		}

		protected void PopulateExistingRequestedAds(AdGroup current_adgroup)
		{
			List<RequestedAd> existing_requestedads = new List<RequestedAd>();



			// Create the output table.
			DataTable adlist = new DataTable();

			adlist.Columns.Add("store");
			adlist.Columns.Add("store_id");
			adlist.Columns.Add("company");
			adlist.Columns.Add("company_id");
			adlist.Columns.Add("title");
			adlist.Columns.Add("num_wanted");
			adlist.Columns.Add("daily_quota");

			foreach (AdMatch admatch in current_adgroup.AdMatchesByadgroup_)
			{
				existing_requestedads.AddRange(admatch.RequestedAdsByadmatch_);
			}

			foreach (RequestedAd item in existing_requestedads)
			{
				DataRow new_row = adlist.NewRow();

				new_row["store"] = item.admatch_.store_.suburb;
				new_row["store_id"] = item.admatch_.store_id;
				new_row["company"] = item.admatch_.store_.company_.name;
				new_row["company_id"] = item.admatch_.store_.company_id;
				new_row["title"] = item.uploadedad_.title;
				new_row["num_wanted"] = item.num_wanted;
				new_row["daily_quota"] = item.daily_quota;


				adlist.Rows.Add(new_row);

			}

			ExistingAdsRequestsGridView.DataSource = adlist;
			ExistingAdsRequestsGridView.DataBind();
		}
	}
}