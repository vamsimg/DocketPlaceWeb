using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Companies
{
	public partial class ViewCompany : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;


		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CompanyNameLiteral.Text = currentCompany.name;

			CheckPermission();

			PopuplateBreadcrumbs();
			PopulateDetails();
		}

		private void CheckPermission()
		{
			if (Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/manage/Rewards/RewardsHome.aspx");
			}
			else if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
		}



		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "My Company";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);


		}

		private void PopulateDetails()
		{
			NameFieldLabel.Text = currentCompany.name;
			ABNFieldLabel.Text = currentCompany.abn;

			ContactNameFieldLabel.Text = currentCompany.contact_name;
			ContactEmailFieldLabel.Text = currentCompany.contact_email;

			AddressFieldLabel.Text = currentCompany.address;
			SuburbFieldLabel.Text = currentCompany.suburb;
			StateFieldLabel.Text = currentCompany.state;
			PostcodeFieldLabel.Text = currentCompany.postcode;

			PhoneFieldLabel.Text = currentCompany.phone;
			FaxFieldLabel.Text = currentCompany.fax;
			MobileFieldLabel.Text = currentCompany.mobile;

			TechnicalContactFieldLabel.Text = currentCompany.technical_contact;

			WebsiteFieldLabel.Text = currentCompany.website;
			NotesFieldLabel.Text = currentCompany.notes;

			if (currentCompany.is_retailer)
			{
				ReatilerFieldLabel.Text = "Retailer";
			}
			else if (currentCompany.is_advertiser)
			{
				ReatilerFieldLabel.Text = "Advertiser";
			}

			if (currentCompany.are_receipts_stored)
			{
				StoreReceiptsFieldLabel.Text = "Yes";
			}
			else
			{
				StoreReceiptsFieldLabel.Text = "No";
			}
		}

	}
}