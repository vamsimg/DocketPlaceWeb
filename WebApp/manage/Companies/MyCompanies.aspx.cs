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
	public partial class MyCompanies : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();

			if (!Helpers.IsSuperUser(loggedInAdmin))
			{
				Response.Redirect("/status.aspx?error=notsuperuser");
			}

			PopulateCompanies();

			PopuplateBreadcrumbs();
		}



		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			Literal Level1 = new Literal();
			Level1.Text = "My Companies";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}


		private void PopulateCompanies()
		{
			IEnumerable<Company> adminCompanies = Permission.GetPermissions().Where(p => p.admin_id == loggedInAdmin.admin_id && p.role_nameId == "Admin").Select(p => p.company_);
			CompaniesDropDownList.DataSource = adminCompanies;
			CompaniesDropDownList.DataBind();
		}

		protected void SelectButton_Click(object sender, EventArgs e)
		{
			int company_id = Convert.ToInt32(CompaniesDropDownList.SelectedValue);

			Session["company_id"] = company_id;
			Session["company_name"] = CompaniesDropDownList.SelectedItem;
			Response.Redirect("/manage/Company/ViewCompany.aspx");
		}
	}
}