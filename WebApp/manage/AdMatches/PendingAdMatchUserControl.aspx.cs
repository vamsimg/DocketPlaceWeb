using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using System.Data;
using WebApp.AppCode;

namespace WebApp.manage.AdMatches
{
	public partial class PendingAdMatchUserControl : System.Web.UI.Page
	{
		private int _admatch_id;

		public int admatch_id
		{
			get { return _admatch_id; }
			set { _admatch_id = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			AdMatch current_admatch = AdMatch.GetAdMatch(admatch_id);


			AdvertiserHyperLink.Text = current_admatch.adgroup_.campaign_.company_.name;
			AdvertiserHyperLink.NavigateUrl = "/manage/Companies/CompanyListing.aspx?company_id=" + current_admatch.adgroup_.campaign_.company_id;

			StoreHyperLink.Text = current_admatch.store_.suburb + " " + current_admatch.admatch_id;
			StoreHyperLink.NavigateUrl = "/manage/Store/ViewStore.aspx?store_id=" + current_admatch.store_id;

			BudgetFieldLabel.Text = "$" + current_admatch.total_budget + " = " + current_admatch.total_ads_wanted + "@" + current_admatch.ad_price + "c each";

			PopulateRequestedAds(current_admatch);
		}



		protected void PopulateRequestedAds(AdMatch current_match)
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


			foreach (RequestedAd ad_item in current_match.RequestedAdsByadmatch_)
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

		protected void AcceptButton_Click(object sender, EventArgs e)
		{
			try
			{

				Admin loggedInAdmin = Helpers.GetLoggedInAdmin();
				AdMatch current_admatch = AdMatch.GetAdMatch(admatch_id);
				current_admatch.ApproveAdMatch();

				string recipient_email = current_admatch.adgroup_.campaign_.creator_.email;
				string recipient_name = current_admatch.adgroup_.campaign_.creator_.full_name;
				string store = current_admatch.store_.suburb;
				string retailer = current_admatch.store_.company_.name;
				string advertiser = current_admatch.adgroup_.campaign_.company_.name;
				string admin_name = loggedInAdmin.full_name;

				EmailHelper.AdMatchAcceptEmail(recipient_email, recipient_name, retailer, store, advertiser, admin_name);

				Response.Redirect("/manage/AdMatches/PendingAdMatches.aspx");
			}
			catch (Exception ex)
			{
			}
		}


		protected void RejectButton_Click(object sender, EventArgs e)
		{
			try
			{
				Admin loggedInAdmin = Helpers.GetLoggedInAdmin();
				AdMatch current_admatch = AdMatch.GetAdMatch(admatch_id);
				current_admatch.RejectAdMatch();

				string recipient_email = current_admatch.adgroup_.campaign_.creator_.email;
				string recipient_name = current_admatch.adgroup_.campaign_.creator_.full_name;

				string store = current_admatch.store_.suburb;
				string retailer = current_admatch.store_.company_.name;
				string advertiser = current_admatch.adgroup_.campaign_.company_.name;
				string admin_name = loggedInAdmin.full_name;

				EmailHelper.AdMatchRejectEmail(recipient_email, recipient_name, retailer, store, advertiser, admin_name);
				Response.Redirect("/manage/AdMatches/PendingAdMatches.aspx");
			}
			catch (Exception ex)
			{
			}
		}		
	}
}