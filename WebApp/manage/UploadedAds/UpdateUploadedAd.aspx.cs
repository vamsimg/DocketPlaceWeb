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
	public partial class UpdateUploadedAd : System.Web.UI.Page
	{
		Admin loggedInAdmin;
		Company current_company;
		UploadedAd current_ad;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			current_company = Helpers.GetCurrentCompany();

			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, current_company) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			current_ad = Helpers.GetCurrentUploadedAd();

			if (!IsPostBack)
			{
				TitleTextBox.Text = current_ad.title;
				FooterTextBox.Text = current_ad.footer;
				NotesTextBox.Text = current_ad.notes;
				ActiveRadioButtonList.SelectedValue = current_ad.is_active.ToString();
				CreationLabel.Text = current_ad.creation_datetime.ToString();
			}

			AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);
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
			Level2.NavigateUrl = "/manage/UploadedAds/AdLibrary.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Update Ad";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}



		protected void UpdateDetailsButton_Click(object sender, EventArgs e)
		{
			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, current_company) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			UploadedAd current_ad = Helpers.GetCurrentUploadedAd();

			if (IsValid)
			{
				try
				{
					current_ad.title = TitleTextBox.Text;
					current_ad.footer = FooterTextBox.Text;
					current_ad.notes = NotesTextBox.Text;
					current_ad.is_active = Convert.ToBoolean(ActiveRadioButtonList.SelectedValue);

					current_ad.Save();
					UpdateDetailsErrorLabel.Text = "Ad Details updated successfully.";
					LogDetailsChange(current_ad);
				}
				catch (Exception ex)
				{
					LogEntry new_entry = LogDetailsFailure(ex, current_ad);

					UpdateDetailsErrorLabel.Text = "Error updating details. Email help@docketplace.com.au with the follwoing error code: " + new_entry.logentry_id.ToString();
				}
			}
		}


		private LogEntry LogDetailsFailure(Exception ex, UploadedAd current_ad)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 2;

			new_logentry.uploadedad_id = current_ad.uploadedad_id;
			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Error";

			new_logentry.Save();
			new_logentry.Refresh();

			Fault new_fault = Fault.CreateFaultBylogentry_(new_logentry);
			new_fault.creation_datetime = new_logentry.creation_datetime;
			new_fault.faultcode_id = 5002;

			new_fault.description = ex.Message;

			new_fault.Save();
			return new_logentry;
		}

		private void LogDetailsChange(UploadedAd current_ad)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 3;

			new_logentry.company_id = current_company.company_id;
			new_logentry.uploadedad_id = current_ad.uploadedad_id;

			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Uploaded Ad Details updated. IP: " + Request.UserHostAddress;
			new_logentry.Save();
		}


		protected void UpdateImageButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				byte[] data = AdFileUpload.FileBytes;

				System.Drawing.Image image = Helpers.ValidateImage(data);

				current_ad.data = BusinessHelper.EncodeAd(image);

				current_ad.Save();
				current_ad.Refresh();
				AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);
			}

		}
	}
}