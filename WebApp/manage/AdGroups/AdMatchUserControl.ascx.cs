using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using System.Data;
using WebApp.AppCode;

namespace WebApp.manage.AdGroups
{
	public partial class AdMatchUserControl : System.Web.UI.UserControl
	{
		private AdMatch _admatch;

		public AdMatch admatch
		{
			get { return _admatch; }
			set { _admatch = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{

			RetailerHyperLink.Text = _admatch.store_.company_.name;
			RetailerHyperLink.NavigateUrl = "/manage/Company/CompanyListing.aspx?company_id=" + _admatch.store_.company_id;

			StoreHyperLink.Text = _admatch.store_.suburb;
			StoreHyperLink.NavigateUrl = "/manage/Stores/ViewStore.aspx?store_id=" + _admatch.store_id;

			//BudgetFieldLabel.Text = "$" + current_admatch.total_budget + " = " + current_admatch.total_ads_wanted + "@" + current_admatch.ad_price + "c each";

			RefreshStatus();

		}

		private void RefreshStatus()
		{
			PopulateRequestedAds();
			if (admatch.is_published == true)
			{
				//PublishButton.Enabled = false;

				if (admatch.is_approved == true)
				{

					PrintButton.Enabled = true;
					if (admatch.is_active == false)
					{
						StatusFieldLabel.Text = "Ready for Printing";
						PrintButton.Text = "Start Print";
					}
					else
					{
						StatusFieldLabel.Text = "Printing Started";
						PrintButton.Text = "Stop Print";
					}

				}
				else if (admatch.is_rejected == true)
				{
					StatusFieldLabel.Text = "Rejected by Retailer";
				}
				else
				{
					StatusFieldLabel.Text = "Waiting for Approval";
				}
			}
			else
			{
				StatusFieldLabel.Text = "Unpublished";
			}

			if (admatch.total_ads_printed > 0)
			{
				DeleteMatchButton.Enabled = false;
				DeleteMatchButton.Visible = false;
			}

		}

		protected void PopulateRequestedAds()
		{
			//Create the output table to list requested ads .
			DataTable adList = new DataTable();

			adList.Columns.Add("uploadedad_id");
			adList.Columns.Add("admatch_id");
			adList.Columns.Add("title");
			adList.Columns.Add("data");
			adList.Columns.Add("num_wanted");
			adList.Columns.Add("num_printed");
			adList.Columns.Add("daily_quota");


			foreach (RequestedAd ad_item in admatch.RequestedAdsByadmatch_)
			{

				DataRow new_row = adList.NewRow();

				new_row["uploadedad_id"] = ad_item.uploadedad_id;
				new_row["admatch_id"] = ad_item.admatch_id;
				new_row["title"] = ad_item.uploadedad_.title;
				new_row["data"] = ad_item.uploadedad_.data;
				new_row["num_wanted"] = ad_item.num_wanted;
				new_row["num_printed"] = ad_item.num_printed;
				new_row["daily_quota"] = ad_item.daily_quota;

				adList.Rows.Add(new_row);
			}

			RequestedAdsListView.DataSource = adList;
			RequestedAdsListView.DataBind();

		}

		/// <summary>
		/// To be implemented.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void PublishButton_Click(object sender, EventArgs e)
		{
			Admin loggedInAdmin = Helpers.GetLoggedInAdmin();

			_admatch.PublishAdMatch();

			RefreshStatus();

			if (!_admatch.is_own_admatch)
			{
				string recipient_email = _admatch.store_.company_.contact_email;
				string recipient_name = _admatch.store_.company_.contact_name;
				string store = _admatch.store_.suburb;
				string retailer = _admatch.store_.company_.name;
				string advertiser = _admatch.adgroup_.campaign_.company_.name;
				string admin_name = loggedInAdmin.full_name;

				EmailHelper.AdMatchRequestEmail(recipient_email, recipient_name, retailer, store, advertiser, admin_name);
			}
			Response.Redirect("/manage/AdGroups/ManageAdGroup.aspx?adgroup_id=" + _admatch.adgroup_id.ToString());
		}


		protected void PrintButton_Click(object sender, EventArgs e)
		{
			Admin loggedInAdmin = Helpers.GetLoggedInAdmin();

			if (_admatch.is_active == false)
			{
				_admatch.is_active = true;
				foreach (RequestedAd requested_ad in _admatch.RequestedAdsByadmatch_)
				{
					requested_ad.is_active = true;
				}
				_admatch.Save();
				StatusFieldLabel.Text = "Printing Started";
				PrintButton.Text = "Stop Print";
			}
			else
			{
				_admatch.is_active = false;
				foreach (RequestedAd requested_ad in _admatch.RequestedAdsByadmatch_)
				{
					requested_ad.is_active = false;
				}
				_admatch.Save();

				StatusFieldLabel.Text = "Approved for Printing";
				PrintButton.Text = "Start Print";
			}
		}

		protected void DeleteMatchButton_Click(object sender, EventArgs e)
		{
			Admin loggedInAdmin = Helpers.GetLoggedInAdmin();

			admatch.RequestedAdsByadmatch_.DeleteAll();
			admatch.Delete();

			Response.Redirect("/manage/Adgroups/ManageAdGroup.aspx?adgroup_id=" + admatch.adgroup_id.ToString());

		}
	}
}