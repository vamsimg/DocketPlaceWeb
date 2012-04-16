using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Data;

namespace WebApp.manage.Rewards
{
	public partial class ViewTransactions : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;



		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			PopuplateBreadcrumbs();

			if (!IsPostBack)
			{
				DateTime startDate = Helpers.ConvertServerDateTimetoLocal(DateTime.Now.AddDays(-1));
				StartDateTextBox.Text = startDate.ToShortDateString();
				EndDateTextBox.Text = startDate.ToShortDateString();
				PopulateDockets();
			}
		}

		private void PopulateDockets()
		{
			DateTime startDate = Convert.ToDateTime(StartDateTextBox.Text);
			DateTime endDate = Convert.ToDateTime(EndDateTextBox.Text).AddHours(23.99);

			if (startDate > endDate)
			{
				UpdateErrorLiteral.Text = "Start date must be earlier than end date.";
			}
			else
			{

				// Create the output table.
				DataTable docketList = new DataTable();

				docketList.Columns.Add("docket_id");
				docketList.Columns.Add("local_id");
				docketList.Columns.Add("customer_id");
				docketList.Columns.Add("customer_exists");
				docketList.Columns.Add("store_id");
				docketList.Columns.Add("total");
				docketList.Columns.Add("reward_points");
				docketList.Columns.Add("creation_datetime");

				int salesCount = 0;
				decimal totalSales = 0;
				foreach (Store item in currentCompany.StoresBycompany_)
				{
					foreach (Docket sale in Docket.GetDocketsByDatesAndStore(startDate, endDate, item.store_id))
					{
						salesCount++;
						totalSales += sale.total;

						DataRow new_row = docketList.NewRow();

						new_row["docket_id"] = sale.docket_id;
						new_row["local_id"] = sale.local_id;
						if (sale.customer_ == null)
						{
							new_row["customer_exists"] = "false";
							new_row["customer_id"] = 0;
						}
						else
						{
							new_row["customer_exists"] = "true";
							new_row["customer_id"] = sale.customer_id;
						}

						new_row["store_id"] = sale.store_id;
						new_row["total"] = sale.total;
						new_row["reward_points"] = sale.reward_points;
						new_row["creation_datetime"] = sale.creation_datetime;

						docketList.Rows.Add(new_row);
					}
				}

				int totalDays = (endDate - startDate).Days + 1;

				TotalDaysLiteral.Text = totalDays.ToString();

				SalesCountLiteral.Text = salesCount.ToString();

				TotalRevenueLiteral.Text = totalSales.ToString("#0.00");

				if (salesCount > 0)
				{
					AverageDailySaleCountLiteral.Text = (salesCount / totalDays).ToString("#0");

					AverageDailyRevenueLiteral.Text = (totalSales / totalDays).ToString("#0.00");

					AverageSaleLiteral.Text = (totalSales / salesCount).ToString("#0.00");
				}

				DocketsGridView.DataSource = docketList;
				DocketsGridView.DataBind();
			}
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
			Level3.Text = "Latest Sales";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		protected void UpdateButton_Click(object sender, EventArgs e)
		{
			PopulateDockets();
		}
	}
}