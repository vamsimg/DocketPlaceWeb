using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Rewards
{
	public partial class DailyReports : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;

		void Page_PreInit(object sender, EventArgs e)
		{
			if (Request.QueryString["print"] == "true")
			{
				MasterPageFile = "~/print.master";
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			if (!IsPostBack)
			{
				DateTime startDate = Helpers.ConvertServerDateTimetoLocal(DateTime.Now);
				DailyDateTextBox.Text = startDate.ToShortDateString();
			}
			PopuplateBreadcrumbs();
		}

		private void CheckPermission()
		{
			if (!(Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
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
			Level3.Text = "Daily Report";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}


		private void PopulateData()
		{
			DateTime reportDate = Convert.ToDateTime(DailyDateTextBox.Text);

			// Make sure to provide store level splitting with multi store companies.
			int storeID = currentCompany.StoresBycompany_[0].store_id;

			DateTime endDate = reportDate.AddDays(1);

			DataTable memberResults = ReportsHelper.RunCustomersReportQuery(reportDate, endDate, storeID);
			DataTable anonymousResults = ReportsHelper.RunAnonymousCustomersReportQuery(reportDate, endDate, storeID);
			DataTable allSales = ReportsHelper.RunDailyReportQuery(reportDate, storeID);

			PopulateSalesSummary(memberResults, anonymousResults, allSales);

			PopulateSalesCharts(allSales);

			PopulateCustomers(reportDate, storeID);
			PopulateDocketItems(reportDate, storeID);

		}


		private void PopulateSalesSummary(DataTable memberResults, DataTable anonymousResults, DataTable allSales)
		{
			DataTable salesSummary = new DataTable();

			salesSummary.Columns.Add("type");
			salesSummary.Columns.Add("total_count");
			salesSummary.Columns.Add("total_revenue");
			salesSummary.Columns.Add("average_sale");

			int totalMemberSalesCount = memberResults.AsEnumerable().Sum(x => x.Field<int>("frequency"));
			decimal totalMemberRevenue = memberResults.AsEnumerable().Sum(x => x.Field<decimal>("total_revenue"));
			decimal averageMemberSale = 0;
			if (totalMemberSalesCount > 0)
			{
				averageMemberSale = totalMemberRevenue / totalMemberSalesCount;
			}

			salesSummary.Rows.Add("Member", totalMemberSalesCount, totalMemberRevenue.ToString("#0"), averageMemberSale.ToString("#0"));

			int totalAnonymousSalesCount = anonymousResults.AsEnumerable().Sum(x => x.Field<int>("frequency"));
			decimal totalAnonymousRevenue = anonymousResults.AsEnumerable().Sum(x => x.Field<decimal>("total_revenue"));
			decimal averageAnonymousSale = 0;
			if (totalAnonymousSalesCount > 0)
			{
				averageAnonymousSale = totalAnonymousRevenue / totalAnonymousSalesCount;
			}

			salesSummary.Rows.Add("Anonymous", totalAnonymousSalesCount, totalAnonymousRevenue.ToString("#0"), averageAnonymousSale.ToString("#0"));


			int salesCount = allSales.AsEnumerable().Sum(x => x.Field<int>("hourly_count"));
			decimal totalSales = allSales.AsEnumerable().Sum(x => x.Field<decimal>("hourly_total"));
			decimal averageSale = 0;
			if (salesCount > 0)
			{
				averageSale = totalSales / salesCount;
			}

			salesSummary.Rows.Add("Total", salesCount, totalSales.ToString("#0"), averageSale.ToString("#0"));
			SalesSummaryGridView.DataSource = salesSummary;
			SalesSummaryGridView.DataBind();
		}


		private void PopulateCustomers(DateTime reportDate, int storeID)
		{
			DataTable results = ReportsHelper.RunCustomersReportQuery(reportDate, reportDate.AddDays(1), storeID);
			CustomersGridView.DataSource = results;
			CustomersGridView.DataBind();
		}

		private void PopulateDocketItems(DateTime reportDate, int storeID)
		{
			DataTable results = ReportsHelper.RunItemsReportQuery(reportDate, reportDate.AddDays(1), storeID);
			DocketItemsGridView.DataSource = results;
			DocketItemsGridView.DataBind();
		}

		private void PopulateSalesCharts(DataTable results)
		{
			// Set chart data source
			HourlyCountChart.DataSource = results;

			// Set series members names for the X and Y values
			HourlyCountChart.Series["Series1"].XValueMember = "hour";
			HourlyCountChart.Series["Series1"].YValueMembers = "hourly_count";

			// Data bind to the selected data source
			HourlyCountChart.DataBind();


			// Set chart data source
			HourlyRevenueChart.DataSource = results;

			// Set series members names for the X and Y values
			HourlyRevenueChart.Series["Series1"].XValueMember = "hour";
			HourlyRevenueChart.Series["Series1"].YValueMembers = "hourly_total";

			// Data bind to the selected data source
			HourlyRevenueChart.DataBind();

			// Set chart data source
			HourlyAverageSalesChart.DataSource = results;

			// Set series members names for the X and Y values
			HourlyAverageSalesChart.Series["Series1"].XValueMember = "hour";
			HourlyAverageSalesChart.Series["Series1"].YValueMembers = "hourly_average";

			// Data bind to the selected data source
			HourlyAverageSalesChart.DataBind();
		}

		protected void UpdateButton_Click(object sender, EventArgs e)
		{
			PopulateData();
		}
	}
}