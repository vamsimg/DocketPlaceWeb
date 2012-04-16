using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using System.Collections;
using WebApp.AppCode;

namespace WebApp.manage.AdGroups
{
	public partial class NewMatchesUserControl : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Show all stores owned by the Company.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ShowOwnButton_Click(object sender, EventArgs e)
		{
			AdGroup current_adgroup = Helpers.GetCurrentAdGroup();

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
		protected void ShowAllButton_Click(object sender, EventArgs e)
		{
			AdGroup current_adgroup = Helpers.GetCurrentAdGroup();

			StoresGridView.DataSource = Store.GetStores().Where(s => s.company_id != current_adgroup.campaign_.company_id);
			StoresGridView.DataBind();

			if (current_adgroup.campaign_.company_.is_retailer == true)
			{
				RefreshAdLibrary(current_adgroup, 5);
			}
			else
			{
				RefreshAdLibrary(current_adgroup, 10);
			}
		}


		private void RefreshAdLibrary(AdGroup current_adgroup, int ad_price)
		{
			PriceLabel.Text = ad_price.ToString() + "c";
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
			NewMatchesErrorLabel.Text = "";
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
						new_admatch.is_active = true;
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

								new_request.is_active = true;
								new_request.Save();
							}
							catch (Exception ex)
							{
								NewMatchesErrorLabel.Text = "1. An error occurred generating ad requests. Please try again";
							}
						}
					}
					catch (Exception ex)
					{
						NewMatchesErrorLabel.Text = "2. An error occurred generating ad matches. Please try again";
					}

				}
				ClearErrorMessages();
				StoresPanel.Visible = true;
				AdsPanel.Visible = false;
			}
		}
	}
}