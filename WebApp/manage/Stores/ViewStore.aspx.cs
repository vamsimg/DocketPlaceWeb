using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Stores
{
	public partial class ViewStore : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private Store currentStore;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			currentStore = Helpers.GetCurrentStore();


			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
			else if (!Helpers.IsStoreAccessible(currentStore, currentCompany))
			{
				Response.Redirect("/status.aspx?error=generic");
			}

			StoreLiteral.Text = currentStore.suburb;
			PopuplateBreadcrumbs();
			PopulateDetails();

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
			Level2.Text = "Stores";
			Level2.NavigateUrl = "/manage/Stores/Stores.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "View Store";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}


		private void PopulateDetails()
		{
			StoreIDLabel.Text = currentStore.store_id.ToString();
			StorePasswordLabel.Text = currentStore.password;
			StoreContactFieldLabel.Text = currentStore.store_contact;

			AddressFieldLabel.Text = currentStore.address;
			SuburbFieldLabel.Text = currentStore.suburb;
			StateFieldLabel.Text = currentStore.state;
			PostcodeFieldLabel.Text = currentStore.postcode;

			PrintersFieldLabel.Text = currentStore.num_printers.ToString();
			VolumeFieldLabel.Text = currentStore.avg_volume.ToString();

		}     
	}
}