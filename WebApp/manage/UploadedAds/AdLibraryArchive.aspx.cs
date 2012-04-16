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
	public partial class AdLibraryArchive : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Admin loggedInAdmin = Helpers.GetLoggedInAdmin();
			Company current_company = Helpers.GetCurrentCompany();

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
			Level2.NavigateUrl = "/manage/UploadedAds/AdLibrary.aspx";


			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Archived Ads";


			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}



		private void RefreshAdLibrary(Company current_company)
		{
			AdLibraryListView.DataSource = current_company.UploadedAdsBycompany_.Where(p => p.is_active == false);
			AdLibraryListView.DataBind();

			if (current_company.UploadedAdsBycompany_.Where(p => p.is_active == false).Count() == 0)
			{
				AdUpdateErrorLabel.Text = "No Archived Ads";
			}
		}

		protected void ActivateAdButton_Command(object sender, CommandEventArgs e)
		{
			Company current_company = Helpers.GetCurrentCompany();

			Button b = (Button)sender;
			int uploadedad_id = Convert.ToInt32(b.CommandArgument);

			try
			{
				UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);
				current_ad.is_active = true;
				current_ad.Save();
				RefreshAdLibrary(current_company);
				AdUpdateErrorLabel.Text = "Ad activated successfully. Go to the Ad Library to view it.";
			}
			catch
			{
				AdUpdateErrorLabel.Text = ErrorHelper.generic;
			}
		}
	}
}