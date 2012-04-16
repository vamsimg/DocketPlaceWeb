using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business.Framework;
using System.Data;
using WebApp.AppCode;
using DocketPlace.Business;

namespace WebApp.manage.Rewards
{
	public partial class ViewVouchers : System.Web.UI.Page
	{

		private Admin loggedInAdmin;
		private Company currentCompany;



		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			PopuplateBreadcrumbs();

			PopulateVouchers();
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
			Level2.Text = "Rewards";
			Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Latest Vouchers";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		private void PopulateVouchers()
		{
			EntityList<Voucher> vouchers = currentCompany.VouchersBycompany_;

			DataTable newTable = new DataTable();

			newTable.Columns.Add("voucher_id");
			newTable.Columns.Add("customer_id");
			newTable.Columns.Add("code");
			newTable.Columns.Add("dollar_value");
			newTable.Columns.Add("creation_datetime");
			newTable.Columns.Add("expiry_datetime");
			newTable.Columns.Add("used_datetime");


			foreach (Voucher item in vouchers)
			{
				string usedDateTime = "";
				if (Helpers.isDateSet(item.used_datetime))
				{
					usedDateTime = Helpers.ConvertServerDateTimetoLocal(item.used_datetime).ToString("dd-MMM-yyyy");
				}
				newTable.Rows.Add(item.voucher_id, item.customer_id, item.code, item.dollar_value.ToString("$#"), Helpers.ConvertServerDateTimetoLocal(item.creation_datetime).ToString("dd-MMM-yyyy"), Helpers.ConvertServerDateTimetoLocal(item.expiry_datetime).ToString("dd-MMM-yyyy"), usedDateTime);
			}

			VouchersGridView.DataSource = newTable;
			VouchersGridView.DataBind();
		}

	}
}