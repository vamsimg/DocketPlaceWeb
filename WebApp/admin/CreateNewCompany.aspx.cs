using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using DocketPlace.Business.Framework;
using System.Threading;

namespace WebApp.admin
{
     public partial class CreateNewCompany : System.Web.UI.Page
     {
          private Admin loggedInAdmin;

          protected void Page_Load(object sender, EventArgs e)
          {
               loggedInAdmin = Helpers.GetLoggedInAdmin();

               if (!Helpers.IsSuperUser(loggedInAdmin))
               {
                    Response.Redirect("/status/errormessage.aspx?error=" + ErrorHelper.notsuperuser);
               }
          }

          protected void CreateCompanyButton_Click(object sender, EventArgs e)
          {
               if (IsValid)
               {
                    Admin loggedInAdmin = Helpers.GetLoggedInAdmin();

                    EntityList<Company> companies = Company.GetCompaniesByABN(ABNTextBox.Text);
                    if (companies.Count != 0)
                    {
                         CreateCompanyErrorLabel.Text = "A company with this ABN already exists. Please check it again or contact customer service.";
                    }
                    else
                    {
                         try
                         {
                              bool is_retailer = Convert.ToBoolean(RetailerRadioButtonList.SelectedValue);
                              bool are_receipts_stored = Convert.ToBoolean(StoreReceiptsRadioButtonList.SelectedValue);

                              Company new_company = Company.CreateCompany();

                              new_company.name = NameTextBox.Text;
                              new_company.abn = ABNTextBox.Text.Trim();
                              new_company.contact_name = ContactNameTextBox.Text;

                              new_company.contact_email = ContactEmailTextBox.Text.Trim();

                              new_company.address = AddressTextBox.Text;
                              new_company.suburb = SuburbTextBox.Text;
                              new_company.state = StateDropDownList.SelectedValue;

                              new_company.postcode = PostcodeTextBox.Text;
                              new_company.phone = PhoneTextBox.Text;
                              new_company.fax = FaxTextBox.Text;
                              new_company.mobile = MobileTextBox.Text;

                              new_company.technical_contact = TechnicalContactTextBox.Text;
                              new_company.website = WebsiteTextBox.Text;

                              new_company.is_advertiser = true;
                              new_company.is_retailer = is_retailer;
                              new_company.smsEnabled = true;
                              new_company.country = "Australia";

                              new_company.creation_datetime = DateTime.Now;

                              new_company.is_active = false;

                              new_company.Save();
                              new_company.Refresh();

                              Session["company_id"] = new_company.company_id;
                              Response.Redirect("/manage/Companies/ViewCompany.aspx", false);
                         }
                       
                         catch (Exception ex)
                         {
                              LogHelper.WriteError(ex.ToString());

                              CreateCompanyErrorLabel.Text = "Error creating new company" + ErrorHelper.generic;
                         }
                    }
               }
          }     
     }
}