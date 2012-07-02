using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Data;

namespace WebApp.manage.Stores
{
	public partial class Stores : System.Web.UI.Page
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
				//CurrentDateTextBox.Text = DateTime.Now.ToShortDateString();
			}

			RefreshStoreList();
			PopuplateBreadcrumbs();

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
			Level2.Text = "Stores";


			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

		}

		private void RefreshStoreList()
		{
			DataTable stores = Helpers.ListToDataTable(current_company.StoresBycompany_.ToList());

			stores.Columns.Add("image_location");
			foreach (DataRow row in stores.Rows)
			{
				UploadedAd current_uploadedad = UploadedAd.GetUploadedAd((int)row["default_uploadedad_id"]);
				if (current_uploadedad != null)
				{
					row["image_location"] = Helpers.GenerateImage(current_uploadedad.data);
				}
				else
				{
					row["image_location"] = "/images/DummyAd.png";
				}
			}



			AllStoresListView.DataSource = stores;
			AllStoresListView.DataBind();



			foreach (ListViewItem item in AllStoresListView.Items)
			{

				HiddenField hf = item.FindControl("StoreIDHiddenField") as HiddenField;
				int store_id = Convert.ToInt32(hf.Value);

				DateTime desiredDate = Convert.ToDateTime(DateTime.Now);

				Store currentStore = Store.GetStore(store_id);


				ListView RequestedAdsListView = item.FindControl("CurrentAdsListView") as ListView;

				// Create the output table to list requested ads .
				DataTable adList = new DataTable();

				adList.Columns.Add("ad_id");
				adList.Columns.Add("match_id");
				adList.Columns.Add("title");
				adList.Columns.Add("data");
				adList.Columns.Add("num_wanted");
				adList.Columns.Add("num_left");
				adList.Columns.Add("daily_quota");
				//adList.Columns.Add("buyer");
				adList.Columns.Add("start_datetime");
				adList.Columns.Add("end_datetime");

				//IEnumerable<AdMatch> storeMatches = currentStore.AdMatchesBystore_.Where(m=>m.start_datetime <= desiredDate && desiredDate<=m.end_datetime && m.is_active==true);

				//foreach (AdMatch match_item in storeMatches )
				//{
				//    foreach (RequestedAd ad_item in match_item.RequestedAdsByadmatch_)
				//    {

				//        DataRow new_row = adList.NewRow();

				//        new_row["ad_id"] = ad_item.uploadedad_id;
				//        new_row["match_id"] = ad_item.admatch_id;
				//        new_row["title"] = ad_item.uploadedad_.title;
				//        new_row["data"] = ad_item.uploadedad_.data;
				//        new_row["num_wanted"] = ad_item.num_wanted;
				//        new_row["num_left"] = ad_item.num_wanted-ad_item.num_printed;
				//        new_row["daily_quota"] = ad_item.daily_quota;

				//        new_row["start_datetime"] = match_item.start_datetime.ToShortDateString();

				//        new_row["end_datetime"] = match_item.end_datetime.ToShortDateString();


				//        adList.Rows.Add(new_row);
				//    }
				//}

				foreach (RequestedAd ad in RequestedAd.GetCurrentAdsForStore(store_id))
				{
					DataRow new_row = adList.NewRow();

					new_row["ad_id"] = ad.uploadedad_id;
					new_row["match_id"] = ad.admatch_id;
					new_row["title"] = ad.uploadedad_.title;
					new_row["data"] = ad.uploadedad_.data;
					new_row["num_wanted"] = ad.num_wanted;
					new_row["num_left"] = ad.num_wanted - ad.num_printed;
					new_row["daily_quota"] = ad.daily_quota;

					new_row["start_datetime"] = ad.admatch_.start_datetime.ToShortDateString();

					new_row["end_datetime"] = ad.admatch_.end_datetime.ToShortDateString();

					adList.Rows.Add(new_row);
				}


				RequestedAdsListView.DataSource = adList;
				RequestedAdsListView.DataBind();

			}
		}   
	}
}