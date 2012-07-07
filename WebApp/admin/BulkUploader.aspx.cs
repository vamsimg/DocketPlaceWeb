using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Text.RegularExpressions;

namespace WebApp.admin
{
     public partial class BulkUploader : System.Web.UI.Page
     {

          private int companyId = 18;
          private int storeID = 26;

          private Admin loggedInAdmin;

          protected void Page_Load(object sender, EventArgs e)
          {
               loggedInAdmin = Helpers.GetLoggedInAdmin();

               if (!Helpers.IsSuperUser(loggedInAdmin))
               {
                    Response.Redirect("/status/errormessage.aspx?error=" + ErrorHelper.notsuperuser);
               }
          }


          protected void Button1_Click(object sender, EventArgs e)
	     {
		byte[] data = FileUpload1.FileBytes;

		string s = System.Text.ASCIIEncoding.ASCII.GetString(data);
	
		List<string> strings = Regex.Split(s, "\r\n").ToList();

		foreach(string item in strings)
		{
			string[] fields = item.Split('\t');

			Customer newCustomer = Customer.CreateCustomer();

			newCustomer.title = fields[3];
			newCustomer.first_name = fields[4];
			newCustomer.last_name = fields[5];
			newCustomer.suburb = fields [6];
			newCustomer.postcode = fields[7];
			newCustomer.phone = fields[8];
               string sanitisedMobile = fields[9].Trim().Replace(" ", "");
               if (sanitisedMobile.StartsWith("4"))
               {
                    sanitisedMobile = "0" + sanitisedMobile;
               }
               newCustomer.mobile = sanitisedMobile;
			newCustomer.email = fields[10].ToLower().Trim();
			newCustomer.email_broken = false;	
			

			newCustomer.verification_code = Helpers.GenerateFiveDigitRandom();
			newCustomer.is_active = true;


			newCustomer.creation_datetime = DateTime.Now;

			string newPassword = Helpers.GenerateFiveDigitRandom();
			newCustomer.password_hash = BusinessHelper.computeSHAhash(newPassword, newCustomer.creation_datetime);

			newCustomer.Save();
			newCustomer.Refresh();

			

			Member newMember = newCustomer.CreateMember();

			newMember.company_id = companyId;
			newMember.store_id = storeID;

			newMember.local_customer_id = fields[0];
			newMember.local_barcode_id = fields[1];
			newMember.reward_points = Convert.ToInt32(fields[2]);
               newMember.frequency = 0;
               newMember.total_revenue = 0;
               newMember.grade = "Default";
			newMember.creation_datetime = newCustomer.creation_datetime;

			newMember.Save();	 
	
			PointsLog newEntry = newCustomer.CreatePointsLog();	
			newEntry.creation_datetime = DateTime.Now;
			newEntry.company_id = newMember.company_id;
			newEntry.reward_points = newMember.reward_points;
			newEntry.description = "Points from previous Healthy Life store.";
	
			newEntry.Save();

		}

		LogHelper.WriteStatus("Bulk Upload for company: " + companyId.ToString());
	     }
     }
}