using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;

namespace WebApp
{
	public partial class Status : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string error_code = Request.QueryString["msg"];

			switch (error_code)
			{
				case "generic":
					ErrorMessageLabel.Text = ErrorHelper.generic;
					break;
				case "notloggedin":
					ErrorMessageLabel.Text = ErrorHelper.notlloggedin;
					break;
				default:
					ErrorMessageLabel.Text = ErrorHelper.generic;
					break;
			}
			PopuplateBreadcrumbs();
		}


		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Error";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}

	}
}