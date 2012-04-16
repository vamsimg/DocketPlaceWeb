using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Stores
{
	public partial class CreateNewStore : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company current_company;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			current_company = Helpers.GetCurrentCompany();

			if (!(Helpers.IsAuthorizedOwner(loggedInAdmin, current_company) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notsuperuser");
			}

			if (!IsPostBack)
			{
				TitleLiteral.Text = current_company.name;
				AdsDropDownList.DataSource = current_company.UploadedAdsBycompany_.Where(p => p.is_active = true);
				AdsDropDownList.DataTextField = "title";
				AdsDropDownList.DataValueField = "uploadedad_id";
				AdsDropDownList.DataBind();

				int uploadedad_id = Convert.ToInt32(AdsDropDownList.Items[0].Value);
				UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);
				AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);

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
			Level2.Text = "Stores";
			Level2.NavigateUrl = "/manage/Stores/Stores.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Create Store";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}



		protected void CreateStoreButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{

				try
				{
					Store new_store = Store.CreateStore();

					new_store.company_id = current_company.company_id;



					new_store.store_contact = StoreContactTextBox.Text;
					new_store.address = AddressTextBox.Text;
					new_store.suburb = SuburbTextBox.Text;

					new_store.state = StateDropDownList.SelectedValue;
					new_store.postcode = PostcodeTextBox.Text;

					new_store.num_printers = Convert.ToInt32(PrintersDropDownList.SelectedValue);

					new_store.avg_volume = Convert.ToInt32(VolumeDropDownList.SelectedValue);
					new_store.default_uploadedad_id = Convert.ToInt32(AdsDropDownList.SelectedValue);

					new_store.creation_datetime = DateTime.Now;

					new_store.Save();
					new_store.Refresh();

					string new_password = new_store.ResetPassword();

					string store_details = new_store.address + " " + new_store.suburb + " for " + current_company.name;

					LogEntry new_entry = LogHelper.LogChange(3, "Store created", loggedInAdmin.admin_id);
					new_entry.store_id = new_store.store_id;
					new_entry.company_id = current_company.company_id;
					new_entry.Save();

					Response.Redirect("/manage/Stores/ViewStore.aspx?store_id=" + new_store.store_id);

				}				
				catch (Exception ex)
				{
					LogEntry new_entry = LogHelper.LogFailure(loggedInAdmin.admin_id, 2003, ex);
					new_entry.company_id = current_company.company_id;
					new_entry.Save();

					StoreListErrorLabel.Text = "Error creating new store. Please email help@docketplace.com.au the following error code: " + new_entry.logentry_id.ToString();
				}
			}
		}

		protected void AdsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int uploadedad_id = Convert.ToInt32(AdsDropDownList.SelectedValue);
			UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);
			AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);
		}
	}
}