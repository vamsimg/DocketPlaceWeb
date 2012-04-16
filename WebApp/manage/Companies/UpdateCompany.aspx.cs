using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using MailChimp.Types;
using DocketPlace.Business;

namespace WebApp.manage.Companies
{
	public partial class UpdateCompany : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company current_company;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			current_company = Helpers.GetCurrentCompany();

			if (!(Helpers.IsAuthorizedOwner(loggedInAdmin, current_company) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notowner");
			}

			if (!IsPostBack)
			{
				PopulateDetails();
			}
			PopuplateBreadcrumbs();

		}

		private void PopulateDetails()
		{
			NameTextBox.Text = current_company.name;
			ABNTextBox.Text = current_company.abn;

			ContactNameTextBox.Text = current_company.contact_name;
			ContactEmailTextBox.Text = current_company.contact_email;

			AddressTextBox.Text = current_company.address;
			SuburbTextBox.Text = current_company.suburb;
			PostcodeTextBox.Text = current_company.postcode;

			PhoneTextBox.Text = current_company.phone;
			FaxTextBox.Text = current_company.fax;
			MobileTextBox.Text = current_company.mobile;

			TechnicalContactTextBox.Text = current_company.technical_contact;
			WebsiteTextBox.Text = current_company.website;

			RetailerRadioButtonList.SelectedValue = current_company.is_retailer.ToString();

			StoreReceiptsRadioButtonList.SelectedValue = current_company.are_receipts_stored.ToString();
			QRCodeRadioButtonList.SelectedValue = current_company.enableQRCodes.ToString();

			MailchimpTextBox.Text = current_company.mailchimp_apikey;
			MCMasterListLabel.Text = current_company.mc_masterlist_id;
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
			Level2.Text = "Update Company";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);


		}

		protected void UpdateCompanyButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					current_company.name = NameTextBox.Text;
					current_company.abn = ABNTextBox.Text;

					current_company.contact_name = ContactNameTextBox.Text;
					current_company.contact_email = ContactEmailTextBox.Text;

					current_company.address = AddressTextBox.Text;
					current_company.suburb = SuburbTextBox.Text;
					current_company.postcode = PostcodeTextBox.Text;

					current_company.phone = PhoneTextBox.Text;
					current_company.fax = FaxTextBox.Text;
					current_company.mobile = MobileTextBox.Text;

					current_company.technical_contact = TechnicalContactTextBox.Text;
					current_company.website = WebsiteTextBox.Text;

					current_company.is_retailer = Convert.ToBoolean(RetailerRadioButtonList.SelectedValue);

					current_company.are_receipts_stored = Convert.ToBoolean(StoreReceiptsRadioButtonList.SelectedValue);
					current_company.enableQRCodes = Convert.ToBoolean(QRCodeRadioButtonList.SelectedValue);

					current_company.Save();
					UpdateCompanyErrorLabel.Text = "Updated details successfully.";
					LogDetailsChange();
				}
				catch (Exception ex)
				{
					LogHelper.WriteError(ex.ToString());
					UpdateCompanyErrorLabel.Text = "Company details were not updated successfully. Please email help@docketplace.com.au.";
				}
			}
		}


		protected void MailchimpButtonButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					string apiKey = MailchimpTextBox.Text.Trim();

					MailChimp.MCApi chimp = new MailChimp.MCApi(apiKey, true);

					var lists = chimp.Lists(null, 0, Opt<int>.None);
					if (lists.Data.Count != 0)
					{
						current_company.mailchimp_apikey = apiKey;
						current_company.mc_masterlist_id = lists.Data.First().ListID;
						current_company.Save();
						MailchimpErrorLabel.Text = "Mailchimp successfully connected. You can now sync all your customers.";
						MCMasterListLabel.Text = current_company.mc_masterlist_id;
						LogDetailsChange();
					}
					else
					{
						MailchimpErrorLabel.Text = "No master list in Mailchimp is available. Please login to www.mailchimp.com and create 1 List";
					}

				}
				catch (MailChimp.Types.MCException ex)
				{
					MailchimpErrorLabel.Text = ex.Message;
				}
				catch (Exception ex)
				{
					LogHelper.WriteError(ex.ToString());
					MailchimpErrorLabel.Text = "Mailchimp not updated successfully. Please email help@docketplace.com.au.";
				}
			}
		}

		private void LogDetailsChange()
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();
			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 3;

			new_logentry.company_id = current_company.company_id;
			new_logentry.owner_id = loggedInAdmin.admin_id;
			new_logentry.description = "Company Details updated. IP: " + Request.UserHostAddress;
			new_logentry.Save();
		}

	}
}