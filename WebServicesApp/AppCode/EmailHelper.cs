using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace WebServicesApp.AppCode
{
	public static class EmailHelper
	{
		public const string sender = "help@docketplace.com.au";


		// For testing use only
		//private const string domain = "localhost:1072";
		//


		public static void SendGenericEmail(string recipient, string subject, string body)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);		       // put the from address here
			mail.To.Add(new MailAddress(recipient));             // put to address here
			mail.Subject = subject;						  // put subject here			
			mail.Body = body;							  // put body of email here
			SmtpClient client = new SmtpClient();
			try
			{
				client.Send(mail);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		


		/// <summary>
		/// Sends a new password to a user.
		/// </summary>
		/// <param name="email"></param>
		/// <param name="new_password"></param>
		public static void PasswordResetEmail(string email, string new_password)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);           // put the from address here
			mail.To.Add(new MailAddress(email));             // put to address here

			mail.Subject = "Password reset from DocketPlace";        // put subject here	
			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "PasswordResetEmail.txt");

			body = body.Replace("$new_password", new_password);
			mail.Body = body;



			SmtpClient client = new SmtpClient();
			try
			{
				client.Send(mail);
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public static void ReceiptEmail(string customerEmail, string content, string companyName)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);		       // put the from address here
			mail.To.Add(new MailAddress(customerEmail));             // put to address here

			mail.Subject = "E-receipt from " + companyName;			  // put subject here	


			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "ReceiptEmail.htm");

               

			body = body.Replace("$content", HttpUtility.HtmlEncode(content));
			mail.IsBodyHtml = true;
			mail.Body = body;


			SmtpClient client = new SmtpClient();
			try
			{
				client.Send(mail);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}