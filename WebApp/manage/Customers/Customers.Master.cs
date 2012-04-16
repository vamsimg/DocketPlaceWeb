using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.manage.Customers
{
	public partial class Customers : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["customer_id"] != null)
			{
				TopNavPanel.Visible = true;
				MiddleNavPanel.Visible = true;

				NotLoggedInNavbarPanel.Visible = false;
			}
			else
			{
				LoginStatus.Visible = false;
			}

		}

		protected void LoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
		{
			Session.Clear();
			Session.Abandon();
		}
	}
}