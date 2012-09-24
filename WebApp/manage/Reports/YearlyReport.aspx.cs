using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Data;

namespace WebApp.manage.Reports
{
	public partial class YearlyReport : System.Web.UI.Page
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
			Level3.Text = "Yearly Report";

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
			int year = Convert.ToInt32(YearDropDownList.SelectedValue);
               // Make sure to provide store level splitting with multi store companies.
               int storeID = Convert.ToInt32(StoresDropDownList.SelectedValue);

			DataTable results = ReportsHelper.RunYearlyReportQuery(year, storeID);

			PopulateSummary(results);
			PopulateCharts(results);
			PopulateDocketItems(year, storeID);
			PopulateCustomers(year, storeID);
		}

		private void PopulateCustomers(int year, int storeID)
		{
			DateTime startDate = new DateTime(year, 1, 1);
			DateTime endDate = startDate.AddDays(GetDaysInAYear(year) + 1);
			DataTable results = ReportsHelper.RunCustomersReportQuery(startDate, endDate, storeID);
			CustomersGridView.DataSource = results;
			CustomersGridView.DataBind();
		}


          private void PopulateDocketItems(int year, int storeID)
          {
               DateTime startDate = new DateTime(year, 1, 1);
               DateTime endDate = startDate.AddDays(GetDaysInAYear(year) + 1);

               var departmentSummary = ReportsHelper.RunDepartmentsSummaryQuery(startDate, endDate, storeID);
               var netProfits = departmentSummary.AsEnumerable().Sum(x => x.Field<double>("net_profits"));

               ProfitsLabel.Text = netProfits.ToString("#0.00");

               DepartmentsSummaryGridView.DataSource = departmentSummary;
               DepartmentsSummaryGridView.DataBind();

               DepartmentCategorySummaryGridView.DataSource = ReportsHelper.RunDepartmentCategoriesSummaryQuery(startDate, endDate, storeID);
               DepartmentCategorySummaryGridView.DataBind();
          }
      

		private int GetDaysInAYear(int year)
		{
			int days = 0;
			for (int i = 1; i <= 12; i++)
			{
				days += DateTime.DaysInMonth(year, i);
			}
			return days;
		}

		private void PopulateSummary(DataTable results)
		{
               int salesCount = 0;
			decimal totalSales = 0;

               salesCount = results.AsEnumerable().Sum(x => x.Field<int>("monthly_count"));
               if (salesCount > 0)
               {
                   
                    totalSales = results.AsEnumerable().Sum(x => x.Field<decimal>("monthly_total"));
               }

			SalesCountLiteral.Text = salesCount.ToString();
			TotalRevenueLiteral.Text = totalSales.ToString("#0.00");

			if (salesCount > 0)
			{
				AverageSaleLiteral.Text = (totalSales / salesCount).ToString("#0.00");
			}
		}

		private void PopulateCharts(DataTable results)
		{
			// Set chart data source
			MonthlyCountChart.DataSource = results;

			// Set series members names for the X and Y values
			MonthlyCountChart.Series["Series1"].XValueMember = "month";
			MonthlyCountChart.Series["Series1"].YValueMembers = "monthly_count";

			// Data bind to the selected data source
			MonthlyCountChart.DataBind();


			// Set chart data source
			MonthlyRevenueChart.DataSource = results;

			// Set series members names for the X and Y values
			MonthlyRevenueChart.Series["Series1"].XValueMember = "month";
			MonthlyRevenueChart.Series["Series1"].YValueMembers = "monthly_total";

			// Data bind to the selected data source
			MonthlyRevenueChart.DataBind();

			// Set chart data source
			MonthlyAverageSalesChart.DataSource = results;

			// Set series members names for the X and Y values
			MonthlyAverageSalesChart.Series["Series1"].XValueMember = "month";
			MonthlyAverageSalesChart.Series["Series1"].YValueMembers = "monthly_average";

			// Data bind to the selected data source
			MonthlyAverageSalesChart.DataBind();
		}


		protected void UpdateButton_Click(object sender, EventArgs e)
		{
			PopulateData();
		}
	}
}