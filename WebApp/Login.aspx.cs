using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using System.Web.Security;

namespace WebApp
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
               if (Session["customer_id"] != null)
               {
                    Response.Redirect("/manage/SelectCompany.aspx");
               }
			
			string error = Request.QueryString["error"];

			if (error == "noemail")
			{
				LoginErrorLabel.Text = "No email address was entered. Please enter an email address.";
			}
			else if (error == "nopassword")
			{
				LoginErrorLabel.Text = "No password was entered. Please enter a password.";
			}
			else if (error == "nouser")
			{
				LoginErrorLabel.Text = "Your email address was not found. Please check the spelling or create a new account.";
			}
			else if (error == "wrongpassword")
			{
				LoginErrorLabel.Text = "Incorrect  Email & Password Combination. Check that CAPS-LOCK is off.";
			}

			PopuplateBreadcrumbs();

		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Merchant Login";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}


		protected void LoginButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				string sanitisedEmail = EmailTextBox.Text.ToLower().Trim();
				string password = PasswordTextBox.Text;

				//Get the user details so we can check if the user exists or if the password is incorrect.

				Admin current_user = Admin.GetAdminsByEmail(sanitisedEmail);

				if (current_user != null)
				{
					string password_hash = BusinessHelper.computeSHAhash(password, current_user.creation_datetime);

					if (current_user.email == sanitisedEmail && current_user.password_hash == password_hash)
					{
						Session["admin_id"] = current_user.admin_id;
						Session["admin_email"] = current_user.email;
						FormsAuthentication.SetAuthCookie(sanitisedEmail, false);
						FormsAuthentication.RedirectFromLoginPage(current_user.email, false);

						Response.Redirect("/manage/SelectCompany.aspx");

					}
					else
					{
						//Last option is that password was incorrect.
						LoginErrorLabel.Text = "Incorrect  Email & Password Combination. Check that CAPS-LOCK is off. ";
					}
				}
				else
				{
					//Username not found
					LoginErrorLabel.Text = "Your email address was not found. Please check the spelling or create a new account.";
				}
			}
		}
	}
}