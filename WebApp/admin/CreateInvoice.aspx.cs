using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business.Framework;
using WebApp.AppCode;
using DocketPlace.Business;

namespace WebApp.admin
{
     public partial class CreateInvoice : System.Web.UI.Page
     {
          private Admin loggedInAdmin;

          protected void Page_Load(object sender, EventArgs e)
          {
               loggedInAdmin = Helpers.GetLoggedInAdmin();

               CheckPermission();
          }

          private void CheckPermission()
          {
               if (!Helpers.IsSuperUser(loggedInAdmin))
               {
                    Response.Redirect("/status/errormessage.aspx?error=" + ErrorHelper.notsuperuser);
               }
          }


          protected void CompaniesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
          {
               BillingItemsGridView.DataBind();
               InvoicesGridView.DataBind();
          }


          protected void CreateItemButton_Click(object sender, EventArgs e)
          {
               BillingItem newItem = BillingItem.CreateBillingItem();

               newItem.company_id = Convert.ToInt32(CompaniesDropDownList.SelectedValue);
               newItem.description = DescriptionTextBox.Text;
               newItem.quantity = Convert.ToInt32(QuantityTextBox.Text);
               newItem.unit_cost = Convert.ToDecimal(UnitCostTextBox.Text);

               if (IsCreditCheckBox.Checked)
               {
                    newItem.total_amount = -1 * newItem.quantity * newItem.unit_cost;
               }
               else
               {
                    newItem.total_amount = newItem.quantity * newItem.unit_cost;
               }

               newItem.creation_datetime = DateTime.Now;

               newItem.Save();

               BillingItemsGridView.DataBind();
          }


          protected void CreateInvoiceButton_Click(object sender, EventArgs e)
          {
               int company_id = Convert.ToInt32(CompaniesDropDownList.SelectedValue);
               EntityList<BillingItem> unattachedItems = BillingItem.GetUnattachedBillingItems(company_id);

               Invoice newInvoice = Invoice.CreateInvoice();
               newInvoice.company_id = company_id;
               newInvoice.terms = "Payment due in 7 days";
               newInvoice.notes = NotesTextBox.Text;
               newInvoice.creation_datetime = DateTime.Now;
               newInvoice.start_datetime = Convert.ToDateTime(StartDateTextBox.Text);
               newInvoice.end_datetime = Convert.ToDateTime(EndDateTextBox.Text);
               newInvoice.total_amount = 0;

               newInvoice.Save();
               newInvoice.Refresh();

               foreach (BillingItem item in unattachedItems)
               {
                    item.invoice_id = newInvoice.invoice_id;
                    item.Save();
               }

               newInvoice.Refresh();
               newInvoice.total_amount = newInvoice.calculatedTotal;

               newInvoice.Save();

               BillingItemsGridView.DataBind();
               InvoicesGridView.DataBind();
          }
     }
}