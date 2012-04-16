using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.AdMatches
{
	public partial class PendingAdMatches : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			Admin loggedInAdmin = Helpers.GetLoggedInAdmin();

			Company current_company = Helpers.GetCurrentCompany();

			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, current_company) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			BackHyperLink.NavigateUrl = "/manage/Company/ViewCompany.aspx?company_id=" + current_company.company_id;


			PopulatePendingMatches(current_company);

		}


		private void PopulatePendingMatches(Company current_company)
		{
			foreach (Store store in current_company.StoresBycompany_)
			{
				foreach (AdMatch admatch in AdMatch.GetPendingMatches(store.store_id))
				{

					var admatch_control = (manage.AdMatches.PendingAdMatchUserControl)LoadControl("PendingAdMatchUserControl.ascx");
					admatch_control.admatch_id = admatch.admatch_id;
					PendingAdMatchesPanel.Controls.Add(admatch_control);

					Literal brclear = new Literal();

					brclear.Text = "<div class=\"brclear\"></div> ";

					PendingAdMatchesPanel.Controls.Add(brclear);
				}
			}
		}
	}
}