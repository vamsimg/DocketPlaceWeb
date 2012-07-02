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
	public partial class UpdateStoreDetails : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private Store currentStore;


		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			currentStore = Helpers.GetCurrentStore();


			if (!Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notsuperuser");
			}
			else if (!Helpers.IsStoreAccessible(currentStore, currentCompany))
			{
				Response.Redirect("/status.aspx?error=generic");
			}

			StoreLiteral.Text = currentStore.suburb;

			if (!IsPostBack)
			{
				PopulateDetails();
				PopulateDefaultAd();
			}
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
			Level2.NavigateUrl = "/manage/Stores/Stores.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Update Store";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}


		private void PopulateDefaultAd()
		{
			AdsDropDownList.DataSource = currentCompany.UploadedAdsBycompany_.Where(p => p.is_active = true);
			AdsDropDownList.DataTextField = "title";
			AdsDropDownList.DataValueField = "uploadedad_id";
			AdsDropDownList.DataBind();
			AdsDropDownList.SelectedValue = currentStore.default_uploadedad_id.ToString();


			UploadedAd current_ad = currentStore.default_uploadedad_;
			AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);
		}

		private void PopulateDetails()
		{
			StoreIDLiteral.Text = currentStore.store_id.ToString();
			StoreContactTextBox.Text = currentStore.store_contact;
			AddressTextBox.Text = currentStore.address;
			SuburbTextBox.Text = currentStore.suburb;
			StateDropDownList.SelectedValue = currentStore.state;
			PostcodeTextBox.Text = currentStore.postcode;

			PrintersDropDownList.SelectedValue = currentStore.num_printers.ToString();
		}



		protected void UpdateStoreButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					currentStore.store_contact = StoreContactTextBox.Text;
					currentStore.address = AddressTextBox.Text;
					currentStore.suburb = SuburbTextBox.Text;

					currentStore.state = StateDropDownList.SelectedValue;
					currentStore.postcode = PostcodeTextBox.Text;

					currentStore.num_printers = Convert.ToInt32(PrintersDropDownList.SelectedValue);

					currentStore.default_uploadedad_id = Convert.ToInt32(AdsDropDownList.SelectedValue);

					currentStore.Save();

					LogEntry new_entry = LogHelper.LogChange(3, "Store details updatetd", loggedInAdmin.admin_id);

					new_entry.store_id = currentStore.store_id;
					new_entry.company_id = currentCompany.company_id;
					new_entry.Save();

					ErrorLabel.Text = "Store Details updated successfully.";
				}
				catch (Exception ex)
				{
					LogEntry new_entry = LogHelper.LogFailure(loggedInAdmin.admin_id, 2004, ex);
					new_entry.company_id = currentCompany.company_id;
					new_entry.store_id = currentStore.store_id;
					new_entry.Save();

					ErrorLabel.Text = "Updating the store details failed. Please email help@docketplace.com.au with the following error number:" + new_entry.logentry_id.ToString();

				}
			}
		}


		private LogEntry LogDetailsFailure(Exception ex)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 2;

			new_logentry.company_id = currentCompany.company_id;
			new_logentry.store_id = currentStore.store_id;
			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Error";

			new_logentry.Save();
			new_logentry.Refresh();

			Fault new_fault = Fault.CreateFaultBylogentry_(new_logentry);
			new_fault.creation_datetime = new_logentry.creation_datetime;
			new_fault.faultcode_id = 2004;

			new_fault.description = ex.Message;

			new_fault.Save();
			return new_logentry;
		}

		private void LogDetailsChange()
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 3;

			new_logentry.company_id = currentCompany.company_id;
			new_logentry.store_id = currentStore.store_id;
			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Store Details updated. IP: " + Request.UserHostAddress;
			new_logentry.Save();
		}






		protected void AdsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int uploadedad_id = Convert.ToInt32(AdsDropDownList.SelectedValue);
			UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);
			AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);
		}


	}
}