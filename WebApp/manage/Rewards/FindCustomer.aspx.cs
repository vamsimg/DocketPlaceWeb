using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using DocketPlace.Business.Framework;
using System.Data;

namespace WebApp.manage.Rewards
{
	public partial class FindCustomer : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			PopuplateBreadcrumbs();
		}



		private void CheckPermission()
		{
			if (!(Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany) || Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
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
			Level2.Text = "Customers & Rewards";
			Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Find a Customer";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		protected void SearchButton_Click(object sender, EventArgs e)
		{
			string searchTerm = "";
			string field = "";
			bool emptySearch = false;
			bool IDgiven = false;
			bool barcodeGiven = false;

			if (LocalCustomerIDTextBox.Text != "")
			{
				int customerID = Convert.ToInt32(LocalCustomerIDTextBox.Text);
				searchTerm = customerID.ToString();
				field = "customer_id";
				IDgiven = true;
			}
			else if (BarcodeTextBox.Text != "")
			{
				string barcodeID = BarcodeTextBox.Text;
				searchTerm = BarcodeTextBox.Text;
				field = "local_barcode_id";
				barcodeGiven = true;
			}
			else if (DocketPlaceIDTextBox.Text != "")
			{
				string barcodeID = BarcodeTextBox.Text;
				searchTerm = BarcodeTextBox.Text;
				field = "customer_id";
			}
			else if (FirstNameTextBox.Text != "")
			{
				searchTerm = FirstNameTextBox.Text;
				field = "first_name";
			}
			else if (LastNameTextBox.Text != "")
			{
				searchTerm = LastNameTextBox.Text;
				field = "last_name";
			}
			else if (MobileTextBox.Text != "")
			{
				searchTerm = MobileTextBox.Text;
				field = "mobile";
			}
			else if (PhoneTextBox.Text != "")
			{
				searchTerm = PhoneTextBox.Text;
				field = "phone";
			}
			else if (EmailTextBox.Text != "")
			{
				searchTerm = EmailTextBox.Text.Trim().ToLower();
				field = "email";
			}
			else
			{
				emptySearch = true;
			}

			if (emptySearch == true)
			{
				SearchErrorLabel.Text = "Enter a search above.";
			}
			else
			{

				EntityList<Customer> foundCustomers = new EntityList<Customer>();
				if (IDgiven)
				{
					EntityList<Member> foundMembers = Member.GetMembersByCompanyAndLocalID(searchTerm, currentCompany.company_id);

					foreach (Member item in foundMembers)
					{
						foundCustomers.Add(item.customer_);
					}
				}
				else if (barcodeGiven)
				{
					EntityList<Member> foundMembers = Member.GetMembersByCompanyAndBarcodeID(searchTerm, currentCompany.company_id);

					foreach (Member item in foundMembers)
					{
						foundCustomers.Add(item.customer_);
					}
				}
				else
				{
					string sanitisedSearchTerm = searchTerm.ToLower().Trim();
					foundCustomers = Customer.GetCustomersBySearch(sanitisedSearchTerm, field);
				}
				if (foundCustomers.Count == 0)
				{
					SearchErrorLabel.Text = "No customers found.";
				}
				else
				{
					DataTable newTable = new DataTable();

					newTable.Columns.Add("customer_id");
					newTable.Columns.Add("local_customer_id");
					newTable.Columns.Add("barcode_id");
					newTable.Columns.Add("title");
					newTable.Columns.Add("first_name");
					newTable.Columns.Add("last_name");
					newTable.Columns.Add("postcode");
					newTable.Columns.Add("mobile");
					newTable.Columns.Add("email");
					newTable.Columns.Add("phone");

					foreach (Customer item in foundCustomers)
					{
						IEnumerable<Member> currentMembers = item.MembersBycustomer_.Where(m => m.company_id == currentCompany.company_id);

						foreach (Member currentMember in currentMembers)
						{
							newTable.Rows.Add(currentMember.customer_id, currentMember.local_customer_id, currentMember.local_barcode_id, item.title, item.first_name, item.last_name, item.postcode, item.mobile, item.email, item.phone);
						}
					}


					CustomersGridView.DataSource = newTable;
					CustomersGridView.DataBind();
				}
			}





		}
	}
}