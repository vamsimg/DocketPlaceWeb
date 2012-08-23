using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocketPlace.Business;

namespace WebApp.AppCode
{
	/// <summary>
	/// Summary description for ErrorHelper
	/// </summary>
	public static class LogHelper
	{
		public static LogEntry LogFailure(int owner_id, int errorcode, Exception ex)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();

			new_logentry.ip_address = HttpContext.Current.Request.UserHostAddress;

			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = 2;


			new_logentry.owner_id = owner_id;
			new_logentry.description = "Error";

			new_logentry.Save();
			new_logentry.Refresh();

			LogFault(ex, new_logentry, errorcode);

			Exception objErr = ex;

			string err = "Error in: " + HttpContext.Current.Request.Url.ToString() +
						   ". Error Message:" + objErr.Message.ToString();
			// Log the error
			WriteError(err);

			return new_logentry;
		}

		private static void LogFault(Exception ex, LogEntry new_logentry, int errorcode)
		{
			Fault new_fault = Fault.CreateFaultBylogentry_(new_logentry);
			new_fault.requested_page = HttpContext.Current.Request.Url.ToString();
			new_fault.creation_datetime = new_logentry.creation_datetime;
			new_fault.faultcode_id = errorcode;

			new_fault.description = ex.Message;

			new_fault.Save();
		}



		public static LogEntry LogChange(int logcode, string description, int owner_id)
		{
			LogEntry new_logentry = LogEntry.CreateLogEntry();

			new_logentry.ip_address = HttpContext.Current.Request.UserHostAddress;

			new_logentry.creation_datetime = DateTime.Now;
			new_logentry.logcode_id = logcode;


			new_logentry.owner_id = owner_id;
			new_logentry.description = description;

			new_logentry.Save();
			new_logentry.Refresh();
			return new_logentry;
		}


		/// Handles error by accepting the error message 
		/// Displays the page on which the error occured
		public static void WriteError(string errorMessage)
		{
			try
			{
				string path = "/logs/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
				if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
				{
					System.IO.File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (System.IO.StreamWriter w = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
				{
					w.WriteLine("\r\nLog Entry : ");
					w.WriteLine("{0}", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
					string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
							    ". Error Message:" + errorMessage;
					w.WriteLine(err);
					w.WriteLine("__________________________");
					w.Flush();
					w.Close();
				}
			}
			catch (Exception ex)
			{
                    throw;
			}
		}

		public static void WriteStatus(string message)
		{
			try
			{
				string path = "/logs/status_" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
				if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
				{
					System.IO.File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (System.IO.StreamWriter w = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
				{
					message = DateTime.Now.ToString() + " , " + message;
					w.WriteLine(message);
					w.Flush();
					w.Close();
				}
			}
			catch (Exception ex)
			{
                    throw;
			}
		}

	}

}