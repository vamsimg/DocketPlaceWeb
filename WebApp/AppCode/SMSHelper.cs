using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace WebApp.AppCode
{
	/// <summary>
	/// Summary description for SMSHelper
	/// </summary>
	public static class SMSHelper
	{
		#region Essendex

		private static string userName = "vamsi@docketplace.com.au";
		private static string password = "NDX7999";
		private static string accountReference = "EX0089065";

		public static decimal SMScost = 0.1M;

		public static string sanitiseMobile(string dirtyMobile)
		{
			return dirtyMobile.Trim().Replace(" ", "");
		}

		public static bool isMobileValid(string mobile)
		{
			return (mobile.Length == 10 && mobile.StartsWith("04"));
		}

		public static string SendSingleSMS(string mobile, string message)
		{
			var sender = new EsendexSend.SendServiceSoapClient();

			var serviceHeader = new EsendexSend.MessengerHeader();

			serviceHeader.Username = userName;
			serviceHeader.Password = password;
			serviceHeader.Account = accountReference; // your account reference
			try
			{

				return sender.SendMessage(serviceHeader, mobile, message, EsendexSend.MessageType.Text);
			}
			catch (Exception ex)
			{
				LogHelper.WriteError("SMS Error" + ex.Message);
				throw ex;
			}
		}

		public static string SendBulkSMS(List<string> mobiles, string message, int outgoingSMS_id)
		{
			var sender = new EsendexSend.SendServiceSoapClient();

			var serviceHeader = new EsendexSend.MessengerHeader();

			serviceHeader.Username = userName;
			serviceHeader.Password = password;
			serviceHeader.Account = accountReference; // your account reference

			string body = message + "Unsub:Reply NO";

			try
			{
				//A list of GUIDS are returned.
				string[] responses = sender.SendMessageMultipleRecipients(serviceHeader, mobiles.ToArray(), body, EsendexSend.MessageType.Text);

				StringBuilder builder = new StringBuilder();

				Console.WriteLine(builder);

				foreach (string response in responses)
				{
					builder.Append(response).Append(",");
				}
				return builder.ToString().TrimEnd(',');

			}
			catch (Exception ex)
			{
				LogHelper.WriteError("SMS Error" + ex.Message);
				throw ex;
			}
		}

		/// <summary>
		/// True if theres enough messages available.
		/// </summary>
		/// <param name="messageCount"></param>
		/// <returns></returns>
		public static bool QuotaAvailable(int messageCount)
		{
			var account = new EsendexAccount.AccountServiceSoapClient();

			var serviceHeader = new EsendexAccount.MessengerHeader();

			serviceHeader.Username = userName;
			serviceHeader.Password = password;
			serviceHeader.Account = accountReference; // your account reference

			try
			{
				bool result = (account.GetMessageLimit(serviceHeader) >= messageCount);
				if (result == false)
				{
					EmailHelper.SendGenericEmail(userName, "Insufficient SMS", "");
				}
				return result;
			}
			catch (Exception ex)
			{
				LogHelper.WriteError("SMS Error" + ex.Message);
				throw ex;
			}
		}


		public static EsendexSend.MessageStatusCode GetMessageResponse(string messageID)
		{
			var sender = new EsendexSend.SendServiceSoapClient();

			var serviceHeader = new EsendexSend.MessengerHeader();

			serviceHeader.Username = userName;
			serviceHeader.Password = password;
			serviceHeader.Account = accountReference; // your account reference
			try
			{

				return sender.GetMessageStatus(serviceHeader, messageID);
			}
			catch (Exception ex)
			{
				LogHelper.WriteError("SMS Error" + ex.Message);
				throw ex;
			}
		}

		public static EsendexInbox.message[] GetReceivedSMSForDate(DateTime date)
		{
			var inbox = new EsendexInbox.InboxServiceSoapClient();

			var serviceHeader = new EsendexInbox.MessengerHeader();

			serviceHeader.Username = userName;
			serviceHeader.Password = password;
			serviceHeader.Account = accountReference; // your account reference

			try
			{
				return inbox.GetMessagesForDay(serviceHeader, date.Year, date.Month, date.Day);
			}
			catch (Exception ex)
			{
				LogHelper.WriteError("SMS Error" + ex.Message);
				throw ex;
			}

		}

		#endregion

		///Deprecated
		#region SL INteractive

		//private static string userName = "vamsinator";
		//private static string password = "518761";

		//public static decimal SMScost = 0.09M;




		//private static string ParseResponse(string input)
		//{
		//     string[] splitResponse = input.Split(':');

		//     string start = splitResponse[0];

		//     string response = "Error sending SMS.";

		//     switch (start)
		//     {
		//          case "COMPLETE":
		//               string numSent = splitResponse[1];
		//               response = numSent + " SMS successfully sent.";
		//               break;
		//          case "E":
		//               string errorCode =  splitResponse[1];
		//               switch (errorCode)
		//               {
		//                    case "NO_USER":
		//                         response = "An error has occurred. Please contact the website administrator.";
		//                         LogHelper.WriteError(input);
		//                         break;
		//                    case "NO_PWORD":
		//                         response = "An error has occurred. Please contact the website administrator.";
		//                         LogHelper.WriteError(input);
		//                         break;
		//                    case "NO_MSG":
		//                         response = "An error has occurred. Please contact the website administrator.";
		//                         LogHelper.WriteError(input);
		//                         break;
		//                    case "NO_TO":
		//                         response = "An error has occurred. Please contact the website administrator.";
		//                         LogHelper.WriteError(input);
		//                         break;
		//                    case "I_UNAME_PWORD":
		//                         response = "An error has occurred. Please contact the website administrator.";
		//                         LogHelper.WriteError(input);
		//                         break;
		//                    case "CREDIT":
		//                         response = "Insufficient SMS credits available. No messages sent. Please contact the website administrator.";
		//                         string creditsLeft = splitResponse[2];
		//                         LogHelper.WriteError(input);
		//                         EmailHelper.SendGenericEmail("vamsi@docketplace.com.au", "SMS credits ran out. Credits left: " + creditsLeft, "");
		//                         break;
		//                    case "PHONE_NO":
		//                         response = "Invalid mobile number. Check that all the mobile numbers start with 04 and is 10 digits long. You may have a landline number instead.";
		//                         LogHelper.WriteError(input);
		//                         break;
		//                    default:
		//                         response = "An error has occurred. Please contact the website administrator.";
		//                         LogHelper.WriteError(input);
		//                         break;							
		//               }
		//               break;
		//     }

		//     return response;
		//}


		//public static string SendSMS(List<string> mobiles, string message, string callerID)
		//{
		//     string moddedMobiles = "";

		//     foreach (string item in mobiles)
		//     {
		//          moddedMobiles += "61" + item.Substring(1) + ",";
		//     }

		//     string url = "http://www.slinteractive.com.au/api/send_sms.php?uname=" + userName + "&pword=" + password + "&msg=" + message + "&to=" + moddedMobiles + "&sid=" + callerID;

		//     WebRequest wrGETURL;
		//     wrGETURL = WebRequest.Create(url);

		//     Stream objStream;
		//     objStream = wrGETURL.GetResponse().GetResponseStream();

		//     StreamReader objReader = new StreamReader(objStream);

		//     string response = "";
		//     string lineReader = "";
		//     while (lineReader != null)
		//     {			
		//          lineReader = objReader.ReadLine();
		//          response += lineReader;

		//     }
		//     return ParseResponse(response.Remove(0,1));
		//}

		//public static string SendBulkSMS(List<string> mobiles, string message, string callerID)
		//{
		//     List<string> cleansedMobiles = new List<string>();

		//     foreach (string item in mobiles)
		//     {
		//          cleansedMobiles.Add ("61" + item.Substring(1));
		//     }

		//     string fileLocation = CreateCSVFile(cleansedMobiles);

		//     string url = "http://www.slinteractive.com.au/api/send_csv.php";

		//     var SMSdetails = new NameValueCollection();

		//     SMSdetails.Add("uname", userName);
		//     SMSdetails.Add("pword", password);
		//     SMSdetails.Add("msg", message);
		//     SMSdetails.Add("sid", callerID);
		//     SMSdetails.Add("pid", "1");

		//     string response = HttpUploadFile(url, fileLocation, "csv_file", SMSdetails);
		//     File.Delete(fileLocation);			
		//     return ParseResponse(response.Trim());
		//}


		//public static string HttpUploadFile(string url, string file, string paramName, NameValueCollection nvc)
		//{		
		//     string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
		//     byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

		//     HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
		//     wr.ContentType = "multipart/form-data; boundary=" + boundary;
		//     wr.Method = "POST";
		//     wr.KeepAlive = true;
		//     wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

		//     Stream rs = wr.GetRequestStream();

		//     string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
		//     foreach (string key in nvc.Keys)
		//     {
		//          rs.Write(boundarybytes, 0, boundarybytes.Length);
		//          string formitem = string.Format(formdataTemplate, key, nvc[key]);
		//          byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
		//          rs.Write(formitembytes, 0, formitembytes.Length);
		//     }
		//     rs.Write(boundarybytes, 0, boundarybytes.Length);

		//     string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n\r\n";
		//     string header = string.Format(headerTemplate, paramName, file);
		//     byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
		//     rs.Write(headerbytes, 0, headerbytes.Length);

		//     FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
		//     byte[] buffer = new byte[4096];
		//     int bytesRead = 0;
		//     while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
		//     {
		//          rs.Write(buffer, 0, bytesRead);
		//     }
		//     fileStream.Close();

		//     byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
		//     rs.Write(trailer, 0, trailer.Length);
		//     rs.Close();

		//     WebResponse wresp = null;
		//     string response = "";
		//     try
		//     {
		//          wresp = wr.GetResponse();
		//          Stream stream2 = wresp.GetResponseStream();
		//          StreamReader reader2 = new StreamReader(stream2);
		//          response =  reader2.ReadToEnd();
		//     }
		//     catch (Exception ex)
		//     {

		//          if (wresp != null)
		//          {
		//               wresp.Close();
		//               wresp = null;
		//          }
		//     }
		//     finally
		//     {
		//          wr = null;
		//     }
		//     return response;
		//}

		//private static string MakeSMSCall(string url, string file, NameValueCollection nvc)
		//{

		//     string boundary = "----------------------------" +
		//     DateTime.Now.Ticks.ToString("x");


		//     HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url);
		//     httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
		//     httpWebRequest2.Method = "POST";
		//     httpWebRequest2.KeepAlive = true;
		//     httpWebRequest2.Credentials =
		//     System.Net.CredentialCache.DefaultCredentials;


		//     Stream memStream = new System.IO.MemoryStream();

		//     byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +	boundary + "\r\n");


		//     string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

		//     foreach(string key in nvc.Keys)
		//     {
		//          string formitem = string.Format(formdataTemplate, key, nvc[key]);
		//          byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
		//          memStream.Write(formitembytes, 0, formitembytes.Length);
		//     }


		//     memStream.Write(boundarybytes,0,boundarybytes.Length);

		//     string headerTemplate = "Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";

		//     string header = string.Format(headerTemplate,"file",file);

		//     byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

		//     memStream.Write(headerbytes,0,headerbytes.Length);


		//     FileStream fileStream = new FileStream(file, FileMode.Open,	FileAccess.Read);
		//     byte[] buffer = new byte[1024];

		//     int bytesRead = 0;

		//     while ( (bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 )
		//     {
		//          memStream.Write(buffer, 0, bytesRead);
		//     }

		//     memStream.Write(boundarybytes,0,boundarybytes.Length);
		//     fileStream.Close();


		//     httpWebRequest2.ContentLength = memStream.Length;

		//     Stream requestStream = httpWebRequest2.GetRequestStream();

		//     memStream.Position = 0;
		//     byte[] tempBuffer = new byte[memStream.Length];
		//     memStream.Read(tempBuffer,0,tempBuffer.Length);
		//     memStream.Close();
		//     requestStream.Write(tempBuffer,0,tempBuffer.Length );
		//     requestStream.Close();


		//     WebResponse webResponse2 = httpWebRequest2.GetResponse();

		//     Stream stream2 = webResponse2.GetResponseStream();
		//     StreamReader reader2 = new StreamReader(stream2);


		//     string response = reader2.ReadToEnd();

		//     webResponse2.Close();
		//     httpWebRequest2 = null;
		//     webResponse2 = null;

		//     return response;
		//}

		//private static string CreateCSVFile(List<string> cleansedMobiles)
		//{
		//     try
		//     {
		//          string serverPath = System.Web.HttpContext.Current.Server.MapPath("/");
		//          string path =  serverPath + "/temp/" + Helpers.GenerateFiveDigitRandom() + ".csv";
		//          if (!System.IO.File.Exists(path))
		//          {
		//               System.IO.File.Create(path).Close();
		//          }
		//          using (System.IO.StreamWriter w = System.IO.File.AppendText(path))
		//          {
		//               foreach (string mobile in cleansedMobiles)
		//               {
		//                    w.WriteLine(mobile);					
		//               }

		//               w.Flush();
		//               w.Close();
		//          }
		//          return path;
		//     }
		//     catch 
		//     {
		//          throw;
		//     }
		//}

		#endregion


	}
}