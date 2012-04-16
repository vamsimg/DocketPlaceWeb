using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.AppCode
{
	/// <summary>
	/// Summary description for ErrorMessages
	/// </summary>
	public static class ErrorHelper
	{
		public static string generic = "An error has occurred. Please contact the help@docketplace.com.au";

		public static string notsuperuser = "Only DocketPlace administrators can access this area.";

		public static string notowner = "Only Owners for this company can access this area.";

		public static string notadmin = "Only Admins for this company can access this area.";

		public static string notlloggedin = "Please login to access this area.";

		public static string companynotfound = "Unable to find this company. Please contact the administrator.";

		public static string storenotfound = "Unable to find this store. Please contact the administrator.";

		public static string adnotfound = "Unable to find this ad. Please contact the administrator.";

		public static string adgroupnotfound = "Unable to find this Ad Group. Please contact the administrator.";

		public static string campaignnotfound = "Unable to find this Campaign. Please contact the administrator.";


	}

}