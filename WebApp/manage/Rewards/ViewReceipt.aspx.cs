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
	public partial class ViewReceipt : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private Docket currentDocket;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			currentDocket = Helpers.GetCurrentDocket();
			if (currentDocket.store_.company_id == currentCompany.company_id)
			{
				DocketTextLiteral.Text = currentDocket.raw_content.Replace("\r", "<br />").Replace("\n", "<br />");
			}
			else
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=generic");
			}

			PopuplateBreadcrumbs();
		}


		private void CheckPermission()
		{
			if (!(Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany) || Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
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
			Level2.Text = "Customers & Rewards";
			Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "View Receipt";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}
	}
}