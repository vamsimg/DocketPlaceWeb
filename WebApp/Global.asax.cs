using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebApp.AppCode;
using System.Web.Routing;
using Microsoft.ServiceModel.Activation;
using System.ServiceModel.Activation;

namespace WebApp
{
	public class Global : System.Web.HttpApplication
	{

		void Application_Start(object sender, EventArgs e)
		{
			
		}

		void Application_End(object sender, EventArgs e)
		{
			//  Code that runs on application shutdown

		}

          protected void Application_BeginRequest(Object sender, EventArgs e)
          {
               if (!Request.IsSecureConnection)
               {
                   Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
               }
          }

		void Application_Error(object sender, EventArgs e)
		{
			// Code that runs when an unhandled error occurs
			
			Exception objErr = Server.GetLastError().GetBaseException();
			string err = "Error in: " + Request.Url.ToString() +
						   "\r\n Error Message:" + objErr.Message.ToString() + "\r\n Inner Exception:" + objErr.StackTrace;
			// Log the error
			LogHelper.WriteError(err);
		}

		void Session_Start(object sender, EventArgs e)
		{
			// Code that runs when a new session is started

		}

		void Session_End(object sender, EventArgs e)
		{
			// Code that runs when a session ends. 
			// Note: The Session_End event is raised only when the sessionstate mode
			// is set to InProc in the Web.config file. If session mode is set to StateServer 
			// or SQLServer, the event is not raised.

		}

	}
}
