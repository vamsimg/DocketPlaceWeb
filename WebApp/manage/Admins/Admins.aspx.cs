using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;
using DocketPlace.Business.Framework;
using System.Data;

namespace WebApp.manage.Admins
{
	public partial class Admins : System.Web.UI.Page
	{
		private Admin loggedInAdmin;

		private Company current_company;

		protected void Page_Load(object sender, EventArgs e)
		{
			AdminListErrorLabel.Text = "";

			loggedInAdmin = Helpers.GetLoggedInAdmin();

			current_company = Helpers.GetCurrentCompany();

			if (!Helpers.IsAuthorizedOwner(loggedInAdmin, current_company))
			{
				Response.Redirect("/status.aspx?msg=notauthorized");
			}


			if (!IsPostBack)
			{
				RefreshAdminsGridview();
			}

			PopuplateBreadcrumbs();
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Company/ViewCompany.aspx";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);

			HyperLink Level2 = new HyperLink();
			Level2.Text = "Admins";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);
		}


		private void RefreshAdminsGridview()
		{
			Role admin_role = Role.GetRole("Archived");

			current_company.Refresh();
			EntityList<Permission> company_permissions = current_company.GetActivePermissions();

			// Create the output table.
			DataTable adminList = new DataTable();

			adminList.Columns.Add("PermissionID");
			adminList.Columns.Add("Role");
			adminList.Columns.Add("First Name");
			adminList.Columns.Add("Last Name");
			adminList.Columns.Add("Email");
			adminList.Columns.Add("Phone");
			adminList.Columns.Add("Position");


			foreach (Permission item in company_permissions)
			{
				DataRow new_row = adminList.NewRow();

				new_row["PermissionID"] = item.permission_id;
				new_row["Role"] = item.role_name.role_name;
				new_row["First Name"] = item.admin_.first_name;
				new_row["Last Name"] = item.admin_.last_name;
				new_row["Email"] = item.admin_.email;
				new_row["Phone"] = item.admin_.phone;
				new_row["Position"] = item.company_position;

				adminList.Rows.Add(new_row);
			}

			AdminsGridView.DataSource = adminList;
			AdminsGridView.DataBind();
		}

		protected void AddExistingAdminButton_Click(object sender, EventArgs e)
		{

			if (IsValid)
			{
				string sanitisedEmail = ExistingEmailTextBox.Text.ToLower().Trim();
				Admin new_admin = Admin.GetAdminsByEmail(sanitisedEmail);


				if (new_admin == null)
				{
					AdminListErrorLabel.Text = "Administrator not found. Please create a new account for them below.";
				}
				else
				{

					var existing_permissions = current_company.PermissionsBycompany_.Where(p => p.admin_id == new_admin.admin_id);

					if (existing_permissions.Count() == 0)
					{
						string role = AdminTypeRadioButtonList.SelectedValue;
						AddAdminToCompany(new_admin, ExistingPositionTextBox.Text, role);
						EmailHelper.AdminAccountCreationEmail(new_admin.email, new_admin.full_name, loggedInAdmin.full_name, role, current_company.name);
						RefreshAdminsGridview();
					}
					else if (existing_permissions.ToList()[0].role_name.role_name == "Archived")
					{
						existing_permissions.ToList()[0].Delete();

						string role = AdminTypeRadioButtonList.SelectedValue;
						AddAdminToCompany(new_admin, ExistingPositionTextBox.Text, role);
						EmailHelper.AdminAccountCreationEmail(new_admin.email, new_admin.full_name, loggedInAdmin.full_name, role, current_company.name);
						RefreshAdminsGridview();
					}
					else
					{
						AdminListErrorLabel.Text = "Administrator is already attached to this company.";
					}

				}
			}
		}



		protected void CreateAdminButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				string sanitisedEmail = EmailTextBox.Text.ToLower().Trim();
				Admin admin = Admin.GetAdminsByEmail(sanitisedEmail);
				if (admin != null)
				{
					AdminListErrorLabel.Text = "A user with this email already exists. Please check it again or contact customer service.";
				}
				else
				{
					try
					{
						Admin new_admin = Admin.CreateAdmin();

						new_admin.first_name = FirstNameTextBox.Text;
						new_admin.last_name = LastNameTextBox.Text;
						new_admin.email = sanitisedEmail;
						new_admin.phone = PhoneTextBox.Text;
						new_admin.is_active = true;
						new_admin.creation_datetime = DateTime.Now;

						new_admin.Save();
						new_admin.Refresh();

						string role = AdminTypeRadioButtonList.SelectedValue;

						AddAdminToCompany(new_admin, PositionTextBox.Text, role);

						AdminListErrorLabel.Text = "New administrator added successfully. An email has been sent to them detailing how to access DocketPlace.";
						EmailHelper.AdminAccountCreationEmail(new_admin.email, new_admin.full_name, loggedInAdmin.full_name, role, current_company.name);
					}
					catch (Exception ex)
					{
						AdminListErrorLabel.Text = ErrorHelper.generic;
						throw ex;
					}
					finally
					{
						RefreshAdminsGridview();
					}
				}
			}
		}

		private void AddAdminToCompany(Admin new_admin, string company_position, string role)
		{

			Permission new_permission = Permission.CreatePermission();

			try
			{
				Role admin_role = Role.GetRole(role);
				new_permission.admin_id = new_admin.admin_id;
				new_permission.role_name = admin_role;
				new_permission.company_id = current_company.company_id;
				new_permission.company_position = company_position;

				new_permission.authoriser_id = loggedInAdmin.admin_id;

				new_permission.creation_datetime = DateTime.Now;

				new_permission.Save();

				AdminListErrorLabel.Text = "New administrator has been successfully added. An email has been sent informing them.";
			}
			catch (Exception ex)
			{
				AdminListErrorLabel.Text = ErrorHelper.generic;
			}
		}


		protected void DeletePermissionImageButton_Command(object sender, CommandEventArgs e)
		{
			ImageButton ib = (ImageButton)sender;
			int permission_id = Convert.ToInt32(ib.CommandArgument);

			try
			{
				Permission current_permission = Permission.GetPermission(permission_id);

				Role archive_role = Role.GetRole("Archived");

				current_permission.role_name = archive_role;
				current_permission.Save();
				RefreshAdminsGridview();
			}
			catch
			{
				AdminListErrorLabel.Text = ErrorHelper.generic;
			}
		}   
	}
}