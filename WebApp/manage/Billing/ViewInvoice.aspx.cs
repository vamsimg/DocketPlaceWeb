using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Billing
{
	public partial class ViewInvoice : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private Invoice currentInvoice;


		void Page_PreInit(object sender, EventArgs e)
		{
			if (Request.QueryString["print"] == "true")
			{
				MasterPageFile = "~/print.master";
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			currentInvoice = Helpers.GetCurrentInvoice();
			CheckPermission();

			PopulateInvoice();

			if (Request.QueryString["print"] == "true")
			{
				PrintHyperLink.Visible = false;
			}
			else
			{
				PopuplateBreadcrumbs();
			}
		}


		/// <summary>
		/// Check that admin is authorized for this company and the invoice belongs to this company.
		/// </summary>
		private void CheckPermission()
		{
			if (!Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
			else if (currentInvoice.company_id != currentCompany.company_id)
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
			Level2.Text = "Billing";
			Level2.NavigateUrl = "/manage/Billing/CurrentBillingItems.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "View Invoice";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		private void PopulateInvoice()
		{
			PrintHyperLink.NavigateUrl = "/manage/Billing/ViewInvoice.aspx?invoice_id=" + currentInvoice.invoice_id.ToString() + "&print=true";
			PrintHyperLink.Target = "_blank";


			InvoiceIDLiteral.Text = currentInvoice.invoice_id.ToString();
			CustomerLiteral.Text = currentCompany.name;
			InvoiceDateLiteral.Text = currentInvoice.creation_datetime.ToString("dd-MMM-yyyy");
			BillingPeriodLiteral.Text = currentInvoice.start_datetime.ToString("dd-MMM-yyyy") + " to " + currentInvoice.end_datetime.ToString("dd-MMM-yyyy");

			if (Helpers.isDateSet(currentInvoice.paid_datetime))
			{
				PaymentDateLiteral.Text = currentInvoice.paid_datetime.ToString("dd-MMM-yyyy");
			}

			TermsLiteral.Text = currentInvoice.terms;

			SubtotalLiteral.Text = "$" + currentInvoice.subTotal.ToString("#0.00");
			GSTLiteral.Text = "$" + currentInvoice.gst.ToString("#0.00");
			TotalLiteral.Text = "$" + currentInvoice.calculatedTotal.ToString("#0.00");
		}
	}
}