using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Customers
{
	public partial class MyVouchers : System.Web.UI.Page
	{
		private Customer loggedInCustomer;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInCustomer = Helpers.GetLoggedInCustomer();
			PopuplateBreadcrumbs();
		}


		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "My Vouchers";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}
	}
}