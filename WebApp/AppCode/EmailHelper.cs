using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace WebApp.AppCode
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
		/// Sends a welcome email to a new admin added by an owner .
		/// </summary>
		/// <param name="newEmail"></param>
		/// <param name="fullName"></param>
		public static void AdminAccountCreationEmail(string newEmail, string fullName, string owner, string role, string company)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);		       // put the from address here
			mail.To.Add(new MailAddress(newEmail));             // put to address here
			mail.Subject = "Welcome to DocketPlace";			  // put subject here	

			string serverPath = System.Web.HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "AdminAccountCreationEmail.txt");

			body = body.Replace("$full_name", fullName);
			body = body.Replace("$role", role);
			body = body.Replace("$owner", owner);
			body = body.Replace("$company", company);

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

		public static void CustomerAccountCreationEmail(string newEmail, string fullName, string password, string company_name)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);		       // put the from address here
			mail.To.Add(new MailAddress(newEmail));             // put to address here
			mail.Subject = "Welcome to DocketPlace Rewards on behalf of " + company_name;			  // put subject here	

			string serverPath = System.Web.HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "CustomerAccountCreationEmail.txt");

			body = body.Replace("$full_name", fullName);
			body = body.Replace("$company", company_name);
			body = body.Replace("$password", password);

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


		public static void StorePasswordEmail(string email, string new_password, string store_details)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);           // put the from address here
			mail.To.Add(new MailAddress(email));             // put to address here

			mail.Subject = "Store Password reset from DocketPlace";        // put subject here	
			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "StorePasswordResetEmail.txt");

			body = body.Replace("$store_details", store_details);
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


		public static void AdMatchRequestEmail(string recipient_email, string recipient_name, string retailer, string store, string advertiser, string admin_name)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);           // put the from address here
			mail.To.Add(new MailAddress(recipient_email));             // put to address here

			mail.Subject = "New Request to Print Ads in the " + store + " Store of " + retailer + " by " + advertiser;        // put subject here	
			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "AdMatchRequest.txt");

			body = body.Replace("$recipient_name", recipient_name);
			body = body.Replace("$retailer", retailer);
			body = body.Replace("$store", store);
			body = body.Replace("$advertiser", advertiser);
			body = body.Replace("$admin_name", admin_name);

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

		public static void AdMatchAcceptEmail(string recipient_email, string recipient_name, string retailer, string store, string advertiser, string admin_name)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);           // put the from address here
			mail.To.Add(new MailAddress(recipient_email));             // put to address here

			mail.Subject = "Accepted: Request to Print Ads in the " + store + " Store of " + retailer;        // put subject here	
			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "AdMatchAccept.txt");

			body = body.Replace("$recipient_name", recipient_name);
			body = body.Replace("$retailer", retailer);
			body = body.Replace("$store", store);
			body = body.Replace("$advertiser", advertiser);
			body = body.Replace("$admin_name", admin_name);

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

		public static void AdMatchRejectEmail(string recipient_email, string recipient_name, string retailer, string store, string advertiser, string admin_name)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);           // put the from address here
			mail.To.Add(new MailAddress(recipient_email));             // put to address here

			mail.Subject = "Declined: Request to Print Ads in the " + store + " Store of " + retailer;        // put subject here	
			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "AdMatchReject.txt");

			body = body.Replace("$recipient_name", recipient_name);
			body = body.Replace("$retailer", retailer);
			body = body.Replace("$store", store);
			body = body.Replace("$advertiser", advertiser);
			body = body.Replace("$admin_name", admin_name);

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

		public static void SignupEmail(string newEmail)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);		       // put the from address here
			mail.To.Add(new MailAddress(sender));             // put to address here
			mail.Subject = "New Sign Up";			  // put subject here	

			string serverPath = System.Web.HttpContext.Current.Server.MapPath("/email/");

			mail.Body = newEmail;							  // put body of email here
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

		public static void CustomerErrorEmail(string customerEmail, string merchantEmail, string errorMessage)
		{
			// create mail message object
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(sender);		       // put the from address here
			mail.To.Add(new MailAddress(merchantEmail));             // put to address here

			mail.Subject = "Error with Customer's account";			  // put subject here	


			string serverPath = HttpContext.Current.Server.MapPath("/email/");
			string body = File.ReadAllText(serverPath + "AccountErrorEmail.txt");

			body = body.Replace("$email", customerEmail);
			body = body.Replace("$errorMessage", errorMessage);				  // put body of email here
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