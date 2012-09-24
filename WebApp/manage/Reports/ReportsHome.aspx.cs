using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;

namespace WebApp.manage.Reports
{
     public partial class ReportsHome : System.Web.UI.Page
     {
          private Admin loggedInAdmin;
          protected Company currentCompany;


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
               Level2.Text = "Reports";


               Literal arrows2 = new Literal();
               arrows2.Text = " >> ";

               breadCrumbPanel.Controls.Add(arrows2);
               breadCrumbPanel.Controls.Add(Level2);
          }

          protected void CategoriesButton_Click(object sender, EventArgs e)
          {
               try
               {
                    foreach (Store store in currentCompany.StoresBycompany_)
                    {
                         ReportsHelper.RefreshDepartmentsAndCategories(store);
                    }
                    CategoriesErrorLabel.Text = "Update successful";
               }
               catch
               {
                    CategoriesErrorLabel.Text = "An error occurred updating the departments and categories.";
                    throw;
               }
          }

     }
}