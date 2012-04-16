using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Customers
{
	public partial class UpdateDetails : System.Web.UI.Page
	{
		private Customer loggedInCustomer;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInCustomer = Helpers.GetLoggedInCustomer();

			if (!IsPostBack)
			{
				if (loggedInCustomer.first_name != null)
				{
					FirstNameTextBox.Text = loggedInCustomer.first_name;
				}

				if (loggedInCustomer.last_name != null)
				{
					LastNameTextBox.Text = loggedInCustomer.last_name;
				}

				if (loggedInCustomer.postcode != null)
				{
					PostcodeTextBox.Text = loggedInCustomer.postcode;
				}


				CurrentEmailLabel.Text = loggedInCustomer.email;

				if (loggedInCustomer.mobile != null)
				{
					MobileTextBox.Text = loggedInCustomer.mobile;
				}
			}

			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Update Details";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);
		}


		protected void UpdateNameButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				loggedInCustomer.first_name = FirstNameTextBox.Text;
				loggedInCustomer.last_name = LastNameTextBox.Text;
				loggedInCustomer.postcode = PostcodeTextBox.Text;

				loggedInCustomer.Save();

				NameErrorLabel.Text = "Changes saved.";
			}
		}


		protected void UpdateEmailButton_Click(object sender, EventArgs e)
		{

			string sanitisedEmail = EmailTextBox.Text.ToLower().Trim();

			try
			{
				loggedInCustomer.email = sanitisedEmail;
				loggedInCustomer.Save();

				EmailErrorLabel.Text = "Updated email Successfully. Please sign out to complete the update.";
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					EmailErrorLabel.Text = "An account with this email address already exists";
				}
			}
		}


		protected void UpdateMobileButton_Click(object sender, EventArgs e)
		{
			string mobile = MobileTextBox.Text;

			Customer existingCustomer = Customer.GetCustomerByMobile(MobileTextBox.Text);
			if (existingCustomer != null)
			{
				MobileErrorLabel.Text = "An account with this mobile number already exists. Please send an email to help@docketplace.com.au";
				return;
			}

			loggedInCustomer.mobile = MobileTextBox.Text;
			loggedInCustomer.Save();

			MobileErrorLabel.Text = "Updated mobile number successfully";
		}


		protected void ChangePasswordButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					Customer loggedInCustomer = Helpers.GetLoggedInCustomer();
					loggedInCustomer.UpdatePassword(NewPasswordTextBox.Text);

					PasswordChangeErrorLabel.Text = "Password updated successfully";
				}
				catch (Exception ex)
				{
					PasswordChangeErrorLabel.Text = "Your password change was unsuccessful. Please email help@docketplace.com.au";
				}
			}
		}   
	}
}