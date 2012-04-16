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
	public partial class ViewReceipt : System.Web.UI.Page
	{
		private Docket currentDocket;
		private Customer loggedInCustomer;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInCustomer = Helpers.GetLoggedInCustomer();
			currentDocket = Helpers.GetCurrentDocket();
			loggedInCustomer = Helpers.GetLoggedInCustomer();
			if (currentDocket.customer_id != loggedInCustomer.customer_id)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=generic");
			}
			else
			{
				DocketTextLiteral.Text = currentDocket.raw_content;
			}
		}
	}
}