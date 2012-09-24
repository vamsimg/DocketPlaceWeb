using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business.Framework;
using System.Data;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Reports
{
	public partial class MonthlyReport : System.Web.UI.Page
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
			PopuplateBreadcrumbs();

               if (!IsPostBack)
               {
                    PopulateStores();
               }
		}

		private void CheckPermission()
		{
			if (!Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Companies/ViewCompany.aspx";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);

			HyperLink Level2 = new HyperLink();
               Level2.Text = "Reports";
               Level2.NavigateUrl = "/manage/Reports/ReportsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Monthly Report";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

          /// <summary>
          /// Check for hacking
          /// </summary>
          private void PopulateStores()
          {
               StoresDropDownList.DataSource = currentCompany.StoresBycompany_;
               StoresDropDownList.DataBind();              
          }


		/// <summary>
		/// Need significant review
		/// </summary>
		private void PopulateData()
		{
			int month = Convert.ToInt32(MonthDropDownList.SelectedValue);
			int year = Convert.ToInt32(YearDropDownList.SelectedValue);

			// Make sure to provide store level splitting with multi store companies.
               int storeID = Convert.ToInt32(StoresDropDownList.SelectedValue);

			DateTime startDate = new DateTime(year, month, 1);
			DateTime endDate = startDate.AddDays(DateTime.DaysInMonth(year, month) + 1);

			DataTable memberResults = ReportsHelper.RunCustomersReportQuery(startDate, endDate, storeID);
			DataTable anonymousResults = ReportsHelper.RunAnonymousCustomersReportQuery(startDate, endDate, storeID);
			DataTable allSales = ReportsHelper.RunMonthlyDayReportQuery(month, year, storeID);

			PopulateSalesSummary(memberResults, anonymousResults, allSales);

			PopulateRewardsSummary(month, year, currentCompany.company_id);

			PopulateSalesCharts(allSales);

			PopulateDocketItems(startDate, endDate, storeID);

			DataTable newMemberStatistics = ReportsHelper.RunMonthlyNewCustomersQuery(month, year, storeID);

			NewMemberCountLiteral.Text = newMemberStatistics.AsEnumerable().Sum(x => x.Field<int>("daily_count")).ToString();

			CustomersGridView.DataSource = memberResults;
			CustomersGridView.DataBind();

			MonthlyMembersLiteral.Text = memberResults.AsEnumerable().Count().ToString();

			// Set chart data source
			NewMembersChart.DataSource = newMemberStatistics;

			// Set series members names for the X and Y values
			NewMembersChart.Series["Series1"].XValueMember = "day";
			NewMembersChart.Series["Series1"].YValueMembers = "daily_count";

			// Data bind to the selected data source
			NewMembersChart.DataBind();

		}

		private void PopulateRewardsSummary(int month, int year, int companyID)
		{
			DateTime startDate = new DateTime(year, month, 1).AddMonths(-1);

			DateTime endDate = new DateTime(year, month, 1).AddMonths(1);

			DateTime thisMonth = new DateTime(year, month, 1);

			EntityList<Voucher> allVouchers = Voucher.GetVouchersByDatesAndCompany(startDate, endDate, companyID);

			DataTable rewardsSummary = new DataTable();

			rewardsSummary.Columns.Add("key");
			rewardsSummary.Columns.Add("value");
			rewardsSummary.Columns.Add("dollar_value");

			IEnumerable<Voucher> vouchersCreatedThisMonth = allVouchers.Where(v => v.creation_datetime >= thisMonth && v.creation_datetime < endDate);

			IEnumerable<Voucher> vouchersRedeemedThisMonth = allVouchers.Where(v => v.used_datetime >= thisMonth && v.used_datetime < endDate);

			IEnumerable<Voucher> vouchersOutstanding = allVouchers.Where(v => v.expiry_datetime >= DateTime.Now);

			rewardsSummary.Rows.Add("Vouchers Created", vouchersCreatedThisMonth.Count(), vouchersCreatedThisMonth.Sum(v => v.dollar_value).ToString("#0"));
			rewardsSummary.Rows.Add("Vouchers Redeemed", vouchersRedeemedThisMonth.Count(), vouchersRedeemedThisMonth.Sum(v => v.dollar_value).ToString("#0"));
			rewardsSummary.Rows.Add("Vouchers Outstanding @" + DateTime.Now.ToString("dd-MMM-yyyy"), vouchersOutstanding.Count(), vouchersOutstanding.Sum(v => v.dollar_value).ToString("#0"));

			RewardsSummaryGridView.DataSource = rewardsSummary;
			RewardsSummaryGridView.DataBind();
		}

		private void PopulateSalesSummary(DataTable memberResults, DataTable anonymousResults, DataTable allSales)
		{
			DataTable salesSummary = new DataTable();

			salesSummary.Columns.Add("type");
			salesSummary.Columns.Add("total_count");
			salesSummary.Columns.Add("total_revenue");
			salesSummary.Columns.Add("average_sale");


			int totalMemberSalesCount = 0;
               decimal totalMemberRevenue = 0;

               totalMemberSalesCount = memberResults.AsEnumerable().Sum(x => x.Field<int>("frequency"));
               if (totalMemberSalesCount > 0)
               {
                    
                    totalMemberRevenue = memberResults.AsEnumerable().Sum(x => x.Field<decimal>("total_revenue"));
               }


			decimal averageMemberSale = 0;
			if (totalMemberSalesCount > 0)
			{
				averageMemberSale = totalMemberRevenue / totalMemberSalesCount;
			}

			salesSummary.Rows.Add("Member", totalMemberSalesCount, totalMemberRevenue.ToString("#0"), averageMemberSale.ToString("#0"));

               int totalAnonymousSalesCount = 0; 
               decimal totalAnonymousRevenue = 0;

               totalAnonymousSalesCount = anonymousResults.AsEnumerable().Sum(x => x.Field<int>("frequency"));
               if (totalAnonymousSalesCount > 0)
               {                
                    totalAnonymousRevenue = anonymousResults.AsEnumerable().Sum(x => x.Field<decimal>("total_revenue"));
               }

			decimal averageAnonymousSale = 0;
			if (totalAnonymousSalesCount > 0)
			{
				averageAnonymousSale = totalAnonymousRevenue / totalAnonymousSalesCount;
			}

			salesSummary.Rows.Add("Anonymous", totalAnonymousSalesCount, totalAnonymousRevenue.ToString("#0"), averageAnonymousSale.ToString("#0"));

               int salesCount = 0;
               decimal totalSales = 0;

               if (allSales.Rows.Count > 0)
               {
                    salesCount = allSales.AsEnumerable().Sum(x => x.Field<int>("daily_count"));
                    totalSales = allSales.AsEnumerable().Sum(x => x.Field<decimal>("daily_total"));
               }

			decimal averageSale = 0;
			if (salesCount > 0)
			{
				averageSale = totalSales / salesCount;
			}

			salesSummary.Rows.Add("Total", salesCount, totalSales.ToString("#0"), averageSale.ToString("#0"));
			SalesSummaryGridView.DataSource = salesSummary;
			SalesSummaryGridView.DataBind();
		}


          private void PopulateDocketItems(DateTime startDate, DateTime endDate, int storeID)
          {
               var departmentSummary = ReportsHelper.RunDepartmentsSummaryQuery(startDate, endDate, storeID);
               var netProfits = departmentSummary.AsEnumerable().Sum(x => x.Field<double>("net_profits"));

               ProfitsLabel.Text = netProfits.ToString("#0.00");

               DepartmentsSummaryGridView.DataSource = departmentSummary;
               DepartmentsSummaryGridView.DataBind();

               DepartmentCategorySummaryGridView.DataSource = ReportsHelper.RunDepartmentCategoriesSummaryQuery(startDate, endDate, storeID);
               DepartmentCategorySummaryGridView.DataBind();
          }





		private void PopulateSalesCharts(DataTable allSales)
		{
			// Set chart data source
			DailyCountChart.DataSource = allSales;

			// Set series members names for the X and Y values
			DailyCountChart.Series["Series1"].XValueMember = "day";
			DailyCountChart.Series["Series1"].YValueMembers = "daily_count";

			// Data bind to the selected data source
			DailyCountChart.DataBind();


			// Set chart data source
			DailyRevenueChart.DataSource = allSales;

			// Set series members names for the X and Y values
			DailyRevenueChart.Series["Series1"].XValueMember = "day";
			DailyRevenueChart.Series["Series1"].YValueMembers = "daily_total";

			// Data bind to the selected data source
			DailyRevenueChart.DataBind();

			// Set chart data source
			DailyAverageSalesChart.DataSource = allSales;

			// Set series members names for the X and Y values
			DailyAverageSalesChart.Series["Series1"].XValueMember = "day";
			DailyAverageSalesChart.Series["Series1"].YValueMembers = "daily_average";

			// Data bind to the selected data source
			DailyAverageSalesChart.DataBind();
		}


		protected void UpdateButton_Click(object sender, EventArgs e)
		{
			PopulateData();
		}
	}
}