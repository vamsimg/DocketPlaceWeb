using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.UploadedAds
{
	public partial class AdLibrary : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company current_company;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			current_company = Helpers.GetCurrentCompany();

			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, current_company) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			if (!IsPostBack)
			{
				RefreshAdLibrary(current_company);
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
			Level2.Text = "Ad Library";


			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

		}


		protected void UploadPicButton_Click(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();

			current_company = Helpers.GetCurrentCompany();

			if (IsValid)
			{
				try
				{
					UploadedAd new_ad = UploadedAd.CreateUploadedAd();

					new_ad.company_id = current_company.company_id;

					new_ad.title = TitleTextBox.Text;

					byte[] data = AdFileUpload.FileBytes;

					System.Drawing.Image image = Helpers.ValidateImage(data);

					new_ad.data = BusinessHelper.EncodeAd(image);

					new_ad.notes = NotesTextBox.Text;
					new_ad.footer = FooterTextBox.Text;
					new_ad.global_barcode = "123";
					new_ad.is_active = true;
					new_ad.creation_datetime = DateTime.Now;

					new_ad.Save();
					RefreshAdLibrary(current_company);
				}
				catch (Exception ex)
				{
					LogEntry new_entry = LogFailure(ex);

					AdUploadErrorLabel.Text = "Error creating new ad." + ex.ToString();
					NewAdModalPopupExtender.Show();
				}
			}
		}


		private LogEntry LogFailure(Exception ex)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 3;

			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Error";

			new_logentry.Save();
			new_logentry.Refresh();

			Fault new_fault = Fault.CreateFaultBylogentry_(new_logentry);
			new_fault.creation_datetime = new_logentry.creation_datetime;
			new_fault.faultcode_id = 5001;

			new_fault.description = ex.Message;

			new_fault.Save();
			return new_logentry;
		}



		private void RefreshAdLibrary(Company current_company)
		{
			AdLibraryListView.DataSource = current_company.UploadedAdsBycompany_.Where(p => p.is_active == true);
			AdLibraryListView.DataBind();
		}


		protected void DeactivateAdButton_Command(object sender, CommandEventArgs e)
		{
			Company current_company = Helpers.GetCurrentCompany();

			Button b = (Button)sender;
			int uploadedad_id = Convert.ToInt32(b.CommandArgument);

			try
			{
				UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);
				current_ad.is_active = false;
				current_ad.Save();
				RefreshAdLibrary(current_company);
				AdUpdateErrorLabel.Text = "Ad deactivated successfully. Go to the Ad Library Archive to view it.";
			}
			catch
			{
				AdUpdateErrorLabel.Text = ErrorHelper.generic;
			}
		}
	}
}