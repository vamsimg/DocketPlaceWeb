using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
	public partial class web : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (Session["admin_id"] != null)
			{
				NotLoggedInNavbarPanel.Visible = false;
				//LoginPanel.Visible = false;

				TopNavPanel.Visible = true;
				MiddleNavPanel.Visible = true;
			}
			else
			{
				LoginStatus.Visible = false;
			}


			if (Session["company_id"] != null)
			{
				CompanyLiteral.Text = "@ " + Session["company_name"];
			}


		}

		protected void LoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
		{
			Session.Clear();
			Session.Abandon();
		}
	}
}