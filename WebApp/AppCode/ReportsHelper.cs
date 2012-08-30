using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApp.AppCode
{

	/// <summary>
	/// Summary description for ReportsHelper
	/// </summary>
	public static class ReportsHelper
	{

		public static DataTable RunDailyReportQuery(DateTime reportDate, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object
			SqlCommand command = new SqlCommand("SELECT datepart(hh,creation_datetime) AS hour, SUM(total) as hourly_total, AVG(total) as hourly_average , COUNT(*) as hourly_count FROM Dockets WHERE  store_id = @storeID and creation_datetime >= @reportDate AND creation_datetime < dateadd(dd,1, @reportDate)  GROUP BY datepart(hh,creation_datetime)", connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@reportDate", reportDate);
			SqlParameter param2 = new SqlParameter("@storeID", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);



			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunWeeklyHourlyReportQuery(DateTime reportDate, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object
			SqlCommand command = new SqlCommand("SELECT datepart(hh,creation_datetime) AS hour, SUM(total) as hourly_total, AVG(total) as hourly_average , COUNT(*) as hourly_count FROM Dockets WHERE  store_id = @storeID and creation_datetime >= @reportDate AND creation_datetime < dateadd(dd,8, @reportDate)  GROUP BY datepart(hh,creation_datetime)", connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@reportDate", reportDate);
			SqlParameter param2 = new SqlParameter("@storeID", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);



			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunWeeklyDayReportQuery(DateTime reportDate, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object
			SqlCommand command = new SqlCommand("SELECT datepart(dd,creation_datetime) AS day, SUM(total) as daily_total, AVG(total) as daily_average , COUNT(*) as daily_count FROM Dockets WHERE  store_id = @storeID and creation_datetime >= @reportDate AND creation_datetime < dateadd(dd,7, @reportDate)  GROUP BY datepart(dd,creation_datetime)", connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@reportDate", reportDate);
			SqlParameter param2 = new SqlParameter("@storeID", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);



			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunMonthlyDayReportQuery(int month, int year, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object
			SqlCommand command = new SqlCommand("SELECT datepart(dd,creation_datetime) AS day, SUM(total) as daily_total, AVG(total) as daily_average , COUNT(*) as daily_count FROM Dockets WHERE  store_id = @storeID and DATEPART(mm, creation_datetime) = @month and DATEPART(yy, creation_datetime) = @year  GROUP BY datepart(dd,creation_datetime)", connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@month", month);
			SqlParameter param2 = new SqlParameter("@year", year);
			SqlParameter param3 = new SqlParameter("@storeID", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunYearlyReportQuery(int year, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object
			SqlCommand command = new SqlCommand("SELECT datepart(mm,creation_datetime) AS month, SUM(total) as monthly_total, AVG(total) as monthly_average , COUNT(*) as monthly_count FROM Dockets WHERE  store_id = @storeID and DATEPART(yy, creation_datetime) = @year  GROUP BY datepart(mm,creation_datetime)", connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@year", year);
			SqlParameter param2 = new SqlParameter("@storeID", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunItemsReportQuery(DateTime startDate, DateTime endDate, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object

               string selectCommand = @"SELECT product_code, product_barcode, description, department, category, cost_ex, sale_ex,sale_inc, COUNT(*) as total_count, (sale_ex * COUNT(*)) as total_revenue
							     FROM Dockets as d
						          INNER JOIN DocketItems as i
						          on d.docket_id = i.docket_id
						          where store_id = @store_id and creation_datetime >= @start_date AND creation_datetime < @end_date
						          GROUP BY product_code, product_barcode, description, cost_ex, sale_ex, sale_inc, department, category
						          order by total_count desc";

			SqlCommand command = new SqlCommand(selectCommand, connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@start_date", startDate);
			SqlParameter param2 = new SqlParameter("@end_date", endDate);
			SqlParameter param3 = new SqlParameter("@store_id", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunAnonymousCustomersReportQuery(DateTime startDate, DateTime endDate, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object

			string selectCommand = @"SELECT  COUNT(*) as frequency, SUM(d.total) as total_revenue
							     FROM Dockets as d
							     where store_id = @store_id and d.creation_datetime >= @start_date AND d.creation_datetime < @end_date and d.customer_id IS NULL ";


			SqlCommand command = new SqlCommand(selectCommand, connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@start_date", startDate);
			SqlParameter param2 = new SqlParameter("@end_date", endDate);
			SqlParameter param3 = new SqlParameter("@store_id", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunCustomersReportQuery(DateTime startDate, DateTime endDate, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object

			string selectCommand = @"SELECT  COUNT(*) as frequency, SUM(d.total) as total_revenue, title, first_name,last_name,suburb,mobile, d.customer_id
							     FROM Dockets as d
							     INNER JOIN Customers as c
							     on d.customer_id = c.customer_id
							     where store_id = @store_id and d.creation_datetime >= @start_date AND d.creation_datetime < @end_date
							     GROUP BY d.customer_id, title, first_name,last_name,suburb,mobile
							     order by SUM(d.total) desc";

			SqlCommand command = new SqlCommand(selectCommand, connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@start_date", startDate);
			SqlParameter param2 = new SqlParameter("@end_date", endDate);
			SqlParameter param3 = new SqlParameter("@store_id", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunMonthlyNewCustomersQuery(int month, int year, int storeID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object

			string selectCommand = @"SELECT datepart(dd,creation_datetime) AS day, COUNT(*) as daily_count 
							     FROM Members 
							     WHERE  store_id = @store_id and DATEPART(mm, creation_datetime) = @month and DATEPART(yy, creation_datetime) = @year
							     GROUP BY datepart(dd,creation_datetime)";

			SqlCommand command = new SqlCommand(selectCommand, connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@month", month);
			SqlParameter param2 = new SqlParameter("@year", year);
			SqlParameter param3 = new SqlParameter("@store_id", storeID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunCustomerItemsQuery(int storeID, int customerID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object
               //TODO Check query accuracy

               string selectCommand = @"SELECT  product_code,description, department, category, cost_ex, sale_ex,sale_inc, COUNT(*) as total_count, (sale_ex * COUNT(*)) as total_revenue
							     FROM Dockets as d
							     INNER JOIN DocketItems as i
							     on d.docket_id = i.docket_id
							     where store_id = @store_id and d.customer_id = @customer_id
							     GROUP BY product_code, description, cost_ex, sale_ex, sale_inc, department, category
							     order by total_count desc";



			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@store_id", storeID);
			SqlParameter param2 = new SqlParameter("@customer_id", customerID);

			SqlCommand command = new SqlCommand(selectCommand, connection);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);



			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}

		public static DataTable RunRewardsSummaryQuery(DateTime startDate, DateTime endDate, int companyID)
		{
			// declare the SqlDataReader, which is used in
			// both the try block and the finally block

			// create a connection object
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

			// create a command object

			string selectCommand = @"SELECT *  
							     FROM Vouchers
							     WHERE  company_id = @company_id and d.creation_datetime >= @start_date AND d.creation_datetime < @end_date";

			SqlCommand command = new SqlCommand(selectCommand, connection);

			// 2. define parameters used in command object
			SqlParameter param1 = new SqlParameter("@start_date", startDate);
			SqlParameter param2 = new SqlParameter("@end_date", endDate);
			SqlParameter param3 = new SqlParameter("@company_id", companyID);

			// 3. add new parameter to command object
			command.Parameters.Add(param1);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);

			SqlDataAdapter ad = new SqlDataAdapter();
			ad.SelectCommand = command;

			DataSet newSet = new DataSet();
			ad.Fill(newSet);
			return newSet.Tables[0];
		}
	}
}