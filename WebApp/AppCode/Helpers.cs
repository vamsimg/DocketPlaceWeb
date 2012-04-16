using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocketPlace.Business.Framework;
using DocketPlace.Business;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;

namespace WebApp.AppCode
{
	public static class Helpers
	{

		public static Customer GetLoggedInCustomer()
		{
			if (HttpContext.Current.Session["customer_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=notloggedin");
			}

			int customer_id = Convert.ToInt32(HttpContext.Current.Session["customer_id"]);

			Customer loggedinCustomer = Customer.GetCustomer(customer_id);

			return loggedinCustomer;
		}



		/// <summary>
		/// Gets the current Admin who is logged in.
		/// </summary>
		/// <returns></returns>
		public static Admin GetLoggedInAdmin()
		{
			if (HttpContext.Current.Session["admin_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=notloggedin");
			}

			int admin_id = Convert.ToInt32(HttpContext.Current.Session["admin_id"]);

			Admin loggedinAdmin = Admin.GetAdmin(admin_id);

			return loggedinAdmin;
		}

		/// <summary>
		/// Gets the first permission found for an amdin. Unique constraint in DB should ensure that the first permission for an admin is the only permission.
		/// </summary>
		/// <param name="loggedInAdmin"></param>
		/// <param name="current_company"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public static Role GetPermission(Admin loggedInAdmin, Company current_company)
		{
			EntityList<Permission> permissions = current_company.PermissionsBycompany_;

			foreach (Permission permission in permissions)
			{
				if (permission.admin_.admin_id == loggedInAdmin.admin_id)
				{
					return permission.role_name;
				}
			}

			return null;
		}


		/// <summary>
		/// Checks to see if the currently logged in Admin is an authorized admin for the company.
		/// </summary>
		/// <param name="loggedInAdmin"></param>
		/// <returns></returns>
		public static bool IsAuthorizedAdmin(Admin loggedInAdmin, Company current_company)
		{
			bool is_ok = false;
			Role admin_role = GetPermission(loggedInAdmin, current_company);

			if (admin_role != null)
			{
				if (admin_role.role_name == "Admin" || admin_role.role_name == "Owner")
				{
					is_ok = true;
				}
			}

			is_ok = (is_ok || IsSuperUser(loggedInAdmin));
			return is_ok;

		}

		/// <summary>
		/// Checks to see if the currently logged in Admin is an authorized clerk for the company.
		/// </summary>
		/// <param name="loggedInAdmin"></param>
		/// <returns></returns>
		public static bool IsAuthorizedClerk(Admin loggedInAdmin, Company current_company)
		{
			bool is_ok = false;
			Role admin_role = GetPermission(loggedInAdmin, current_company);

			if (admin_role != null)
			{
				if (admin_role.role_name == "Clerk")
				{
					is_ok = true;
				}
			}

			return is_ok;
		}


		/// <summary>
		/// Checks to see if the currently logged in Admin is an authorized Owner for the company.
		/// </summary>
		/// <param name="loggedInAdmin"></param>
		/// <param name="current_company"></param>
		/// <returns></returns>
		public static bool IsAuthorizedOwner(Admin loggedInAdmin, Company current_company)
		{
			bool is_ok = false;
			Role admin_role = GetPermission(loggedInAdmin, current_company);

			if (admin_role != null)
			{
				if (admin_role.role_name == "Owner")
				{
					is_ok = true;
				}
			}

			return is_ok;
		}

		/// <summary>
		///  Checks to see if the currently logged in Admin is a SuperUser.
		/// </summary>
		/// <param name="loggedInAdmin"></param>
		/// <returns></returns>
		public static bool IsSuperUser(Admin loggedInAdmin)
		{
			if (loggedInAdmin.email == "vamsi.mg+coupon@gmail.com")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Check to see if the current store is owned by the company.
		/// </summary>
		/// <param name="current_store"></param>
		/// <param name="current_company"></param>
		/// <returns></returns>
		public static bool IsStoreAccessible(Store current_store, Company current_company)
		{
			if (current_store.company_id == current_company.company_id)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the company object for a currently selected company.
		/// </summary>
		/// <returns></returns>
		public static Company GetCurrentCompany()
		{

			if (HttpContext.Current.Session["company_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/manage/Company/MyCompanies.aspx");
			}

			int company_id = Convert.ToInt32(HttpContext.Current.Session["company_id"]);

			Company current_company = Company.GetCompany(company_id);

			if (current_company == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=companynotfound");
			}

			return current_company;
		}

		public static Store GetCurrentStore()
		{

			if (HttpContext.Current.Request.QueryString["store_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/manage/Stores/Stores.aspx");
			}

			int store_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["store_id"]);

			Store current_store = Store.GetStore(store_id);

			if (current_store == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=storenotfound");
			}

			return current_store;
		}

		public static UploadedAd GetCurrentUploadedAd()
		{

			if (HttpContext.Current.Request.QueryString["uploadedad_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/manage/UpploadedAds/UploadedAds.aspx");
			}

			int uploadedad_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["uploadedad_id"]);

			UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);

			if (current_ad == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=adnotfound");
			}

			return current_ad;
		}


		/// <summary>
		/// Gets the campaign object for a currently selected campaign.
		/// </summary>
		/// <returns></returns>
		public static Campaign GetCurrentCampaign()
		{

			if (HttpContext.Current.Request.QueryString["campaign_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/manage/Campaigns/Campaigns.aspx");

			}

			int campaign_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["campaign_id"]);

			Campaign current_campaign = Campaign.GetCampaign(campaign_id);

			if (current_campaign == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=campaignnotfound");
			}
			return current_campaign;
		}


		public static AdGroup GetCurrentAdGroup()
		{

			if (HttpContext.Current.Request.QueryString["adgroup_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/manage/Campaigns/Campaigns.aspx");

			}

			int adgroup_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["adgroup_id"]);

			AdGroup current_adgroup = AdGroup.GetAdGroup(adgroup_id);

			if (current_adgroup == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=adgroupnotfound");
			}
			return current_adgroup;
		}

		public static Docket GetCurrentDocket()
		{

			if (HttpContext.Current.Request.QueryString["docket_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=generic");
			}

			int docket_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["docket_id"]);

			Docket currentDocket = Docket.GetDocket(docket_id);

			if (currentDocket == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=docketnotfound");
			}

			return currentDocket;
		}

		public static Customer GetCurrentCustomer()
		{
			if (HttpContext.Current.Request.QueryString["customer_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=generic");
			}

			int customer_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["customer_id"]);

			Customer currentCustomer = Customer.GetCustomer(customer_id);

			if (currentCustomer == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=customernotfound");
			}

			return currentCustomer;
		}


		public static Invoice GetCurrentInvoice()
		{
			if (HttpContext.Current.Request.QueryString["invoice_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=generic");
			}

			int invoiceID = Convert.ToInt32(HttpContext.Current.Request.QueryString["invoice_id"]);

			Invoice currentInvoice = Invoice.GetInvoice(invoiceID);

			if (currentInvoice == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=invoicenotfound");
			}

			return currentInvoice;
		}


		public static CustomerList GetCurrentList()
		{
			if (HttpContext.Current.Request.QueryString["customerlist_id"] == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=generic");
			}

			int customerList_id = Convert.ToInt32(HttpContext.Current.Request.QueryString["customerlist_id"]);

			var currentList = CustomerList.GetCustomerList(customerList_id);

			if (currentList == null)
			{
				HttpContext.Current.Response.Redirect("/status.aspx?msg=listnotfound");
			}

			return currentList;
		}


		public static string GenerateImage(string inputbase64EncodedImage)
		{
			string file_location = "";


			Image img = BusinessHelper.DecodeAd(inputbase64EncodedImage);
			string directory = System.Environment.CurrentDirectory;

			string serverPath = HttpContext.Current.Server.MapPath("/manage/UploadedAds/temp/");

			string random_appendage = BusinessHelper.computeSHAhash(inputbase64EncodedImage, DateTime.Now);

			string tempfilename = serverPath + random_appendage + ".png";

			img.Save(tempfilename, ImageFormat.Png);

			file_location = "/manage/UploadedAds/temp/" + random_appendage + ".png";


			return file_location;
		}


		public static Image ValidateImage(byte[] upload_data)
		{
			Exception invalidImage = new Exception("Image appears to corrupt. Please download a new template and create the Ad again");
			Exception incorrectWidth = new Exception("Image is of the incorrect width. Width must be 500 pixels.");
			Exception tooTall = new Exception("Image is too tall. Maximum height is 1000 pixels.");
			Exception tooShort = new Exception("Image is too short. Minimum height is 150 pixels.");
			Exception tooBig = new Exception("Image size is too big. Make sure the image is an 8-bit black and white PNG and is 500 pixels wide and 500 pixels tall.");
			Exception wrongFormat = new Exception("Image is not a valid PNG.");


			Image image;

			try
			{
				MemoryStream ms = new MemoryStream(upload_data);
				image = (Image)System.Drawing.Image.FromStream(ms);
			}
			catch
			{
				throw (invalidImage);
			}


			if (!IsPNGImage(image))
				throw wrongFormat;
			else if (image.Width != 500)
				throw incorrectWidth;
			else if (image.Height > 1000)
				throw tooTall;
			else if (image.Height < 150)
				throw tooShort;
			else if (upload_data.Length > 200000)
				throw tooBig;

			return image;
		}

		static bool IsPNGImage(Image testImage)
		{
			try
			{

				// Two image formats can be compared using the Equals method
				// See http://msdn.microsoft.com/en-us/library/system.drawing.imaging.imageformat.aspx
				//
				return testImage.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png);
			}
			catch (OutOfMemoryException)
			{
				// Image.FromFile throws an OutOfMemoryException 
				// if the file does not have a valid image format or
				// GDI+ does not support the pixel format of the file.
				//
				return false;
			}
		}


		public static DataTable ListToDataTable<T>(List<T> list)
		{
			DataTable dt = new DataTable();

			foreach (PropertyInfo info in typeof(T).GetProperties())
			{
				dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
			}
			foreach (T t in list)
			{
				DataRow row = dt.NewRow();
				foreach (PropertyInfo info in typeof(T).GetProperties())
				{
					row[info.Name] = info.GetValue(t, null);
				}
				dt.Rows.Add(row);
			}
			return dt;
		}

		public static string GenerateFiveDigitRandom()
		{
			Random r = new Random();
			int num = r.Next(0, 99999);

			string output = num.ToString().Trim();
			return output;
		}


		/// <summary>
		/// Used for encoding receipt content before sending to server. This removes http issue with esc characters.
		/// </summary>
		/// <param name="toEncode"></param>
		/// <returns></returns>
		public static string EncodeToBase64(string toEncode)
		{
			byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

			string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

			return returnValue;
		}


		public static string DecodeFromBase64(string encodedData)
		{
			byte[] encodedDataAsBytes
			    = System.Convert.FromBase64String(encodedData);
			string returnValue =
			   System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
			return returnValue;
		}

		/// <summary>
		/// This is a hack to see if a date field is null in an object from the DAL.
		/// </summary>
		/// <param name="testDate"></param>
		/// <returns></returns>
		public static bool isDateSet(DateTime testDate)
		{
			DateTime test = new DateTime(0001, 01, 01);
			return (!(testDate == test));
		}


		/// <summary>
		/// Offsets are relative to Greenwich Mean Time. Server is US Pacific
		/// </summary>
		private static int serverDatetimeOffset = -8;

		/// <summary>
		/// Stores are Australian Eastern 
		/// </summary>
		private static int storeDatetimeOffset = 10;

		public static DateTime ConvertServerDateTimetoLocal(DateTime serverDatetime)
		{
			return serverDatetime.AddHours(storeDatetimeOffset - serverDatetimeOffset);
		}

		/// <summary>
		/// Function to get byte array from a file
		/// </summary>
		/// <param name="_FileName">File name to get byte array</param>
		/// <returns>Byte Array</returns>
		public static byte[] FileToByteArray(string _FileName)
		{
			byte[] _Buffer = null;

			try
			{
				// Open file for reading
				System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

				// attach filestream to binary reader
				System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

				// get total byte length of the file
				long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

				// read entire file into buffer
				_Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

				// close file reader
				_FileStream.Close();
				_FileStream.Dispose();
				_BinaryReader.Close();
			}
			catch (Exception _Exception)
			{
				// Error
				Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
			}

			return _Buffer;
		}


	}
}