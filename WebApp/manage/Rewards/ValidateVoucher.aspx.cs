using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;

namespace WebApp.manage.Rewards
{
     public partial class ValidateVoucher : System.Web.UI.Page
     {
          private Admin loggedInAdmin;
          private Company currentCompany;



          protected void Page_Load(object sender, EventArgs e)
          {
               loggedInAdmin = Helpers.GetLoggedInAdmin();
               currentCompany = Helpers.GetCurrentCompany();
               CheckPermission();

               PopuplateBreadcrumbs();              
          }

         
         

          private void CheckPermission()
          {
               if (!(Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany) || Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany)))
               {
                    Response.Redirect("/status.aspx?error=notadmin");
               }              
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
               Level2.Text = "Customers & Rewards";
               Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

               Literal arrows2 = new Literal();
               arrows2.Text = " >> ";

               breadCrumbPanel.Controls.Add(arrows2);
               breadCrumbPanel.Controls.Add(Level2);

               HyperLink Level3 = new HyperLink();
               Level3.Text = "Validate Voucher";

               Literal arrows3 = new Literal();
               arrows3.Text = " >> ";

               breadCrumbPanel.Controls.Add(arrows3);
               breadCrumbPanel.Controls.Add(Level3);
          }



          protected void ValidateButton_Click(object sender, EventArgs e)
          {
               if (IsValid)
               {
                    int voucherID = Convert.ToInt32(VoucherIDTextBox.Text);
                    string voucherCode = VoucherCodeTextBox.Text;

                    try
                    {
                         var currentVoucher = Voucher.GetVoucher(voucherID);

                         if (currentVoucher == null)
                         {
                              ValidateErrorLabel.Text = "Voucher not found";
                         }
                         else if (currentVoucher.company_id != currentCompany.company_id)
                         {
                              ValidateErrorLabel.Text = "Not your company's voucher";
                         }
                         else if (voucherCode != currentVoucher.code)
                         {
                              ValidateErrorLabel.Text = "Incorrect voucher code";
                         }
                         else
                         {
                              currentVoucher.used_datetime = DateTime.Now;
                              currentVoucher.Save();
                              ValidateErrorLabel.Text = "Voucher validated successfully";
                         }                         
                    }
                    catch
                    {
                         ValidateErrorLabel.Text = "An error has occurred. Please contact help@docketplace.com.au";
                         throw;
                    }

               }
          }
     }
}