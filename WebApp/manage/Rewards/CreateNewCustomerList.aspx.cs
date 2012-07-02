using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Rewards
{
	public partial class CreateNewCustomerList : System.Web.UI.Page
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
			if (!(Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}


		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Companies/ViewCompany.aspx";

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
			Level3.Text = "Create Customer List";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		protected void CreateButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				try
				{
					var newList = CustomerList.CreateCustomerList();
					newList.company_id = currentCompany.company_id;
					newList.admin_id = loggedInAdmin.admin_id;

					newList.creation_datetime = DateTime.Now;

					int numCustomers = Convert.ToInt16(NumCustomersTextBox.Text);

					string selectStatement = "select TOP " + numCustomers.ToString() + @" * from Customers inner join Members 
									 on Customers.customer_id = Members.customer_id 
									 where company_id = " + currentCompany.company_id.ToString() + " ORDER BY ";

					string notes = "Top " + numCustomers.ToString() + " Customers by ";

					switch (ListTypeRadioButtonList.SelectedValue)
					{
						case "total_revenue":
							selectStatement += "total_revenue desc";
							notes += "total revenue";
							break;
						case "frequency":
							selectStatement += "frequency desc";
							notes += "visits to store";
							break;
					}
					newList.title = TitleTextBox.Text;
					newList.select_statement = selectStatement;
					newList.notes = notes;
					newList.Save();
					newList.Refresh();
					Response.Redirect("ViewCustomerList.aspx?customerlist_id=" + newList.customerlist_id.ToString(), true);
				}
				catch
				{
					CreateListErrorLabel.Text = ErrorHelper.generic;
					throw;
				}
			}
		}
	}
}