using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;
using System.Data;
using System.Text;
using MailChimp.Types;

namespace WebApp.manage.Reports
{
     public partial class CategoryAnalysis : System.Web.UI.Page
     {
          private Admin loggedInAdmin;
          private Company currentCompany;

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
               CheckPermission();

              


               if (!IsPostBack)
               {
                    
                    //Register the script to escape angle brackets in category body. Usually <N/A>
                    
                    string keyValue = "this.form." + DepartmentsDropDownList.ClientID + ".value = encodeMyHtml(this.form." + DepartmentsDropDownList.ClientID + ".value);";
                    UpdateButton.Attributes.Add("onclick", keyValue);

                    DateTime startDate = DateTime.Now;
                    StartDateTextBox.Text = startDate.ToShortDateString();
                    EndDateTextBox.Text = startDate.ToShortDateString();

                    PopulateStores();
               }

               if (IsPostBack)
               {
                    EnableMailchimp();
                    EnableSMS();
               }
               PopuplateBreadcrumbs();
          }

          private void CheckPermission()
          {
               if (!Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany))
               {
                    Response.Redirect("/status.aspx?error=notadmin");
               }

              
          }

          private void EnableMailchimp()
          {
               if (!String.IsNullOrEmpty(currentCompany.mailchimp_apikey) && !String.IsNullOrEmpty(currentCompany.mc_masterlist_id))
               {
                    MailchimpPanel.Visible = true;
                    MailchimpPanel.Enabled = true;
               }
          }

          private void EnableSMS()
          {
               if (Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany) && currentCompany.smsEnabled)
               {
                    SMSPanel.Visible = true;
                    SMSPanel.Enabled = true;
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
               Level2.Text = "Reports";
               Level2.NavigateUrl = "/manage/Reports/ReportsHome.aspx";

               Literal arrows2 = new Literal();
               arrows2.Text = " >> ";

               breadCrumbPanel.Controls.Add(arrows2);
               breadCrumbPanel.Controls.Add(Level2);

               HyperLink Level3 = new HyperLink();
               Level3.Text = "Category Analysis";

               Literal arrows3 = new Literal();
               arrows3.Text = " >> ";

               breadCrumbPanel.Controls.Add(arrows3);
               breadCrumbPanel.Controls.Add(Level3);
          }

          /// <summary>
          /// Check for hacking
          /// </summary>
          private void PopulateStores()
          {
               StoresDropDownList.DataSource = currentCompany.StoresBycompany_;
               StoresDropDownList.DataBind();

               var departments = new List<string>();

               foreach(var store in currentCompany.StoresBycompany_)
               {
                    departments.AddRange(store.departments.Split('|').ToList());
               }

               DepartmentsDropDownList.DataSource = departments;
               DepartmentsDropDownList.DataBind();
          }         

          protected void UpdateButton_Click(object sender, EventArgs e)
          {

               var results = GetRawData();
               var totalCosts = (from r in results.AsEnumerable()
                                 select ((decimal)r.Field<double>("quantity") * r.Field<decimal>("cost_ex"))).Sum();

               var memberCosts = (from r in results.AsEnumerable()
                                  where r.Field<int?>("customer_id") != null
                                  select ((decimal)r.Field<double>("quantity") * r.Field<decimal>("cost_ex"))).Sum();

               var nonMemberCosts = (from r in results.AsEnumerable()
                                     where r.Field<int?>("customer_id") == null
                                     select ((decimal)r.Field<double>("quantity") * r.Field<decimal>("cost_ex"))).Sum();

               var memberSales = (from r in results.AsEnumerable()
                                  where r.Field<int?>("customer_id") != null
                                  select ((decimal)r.Field<double>("quantity") * r.Field<decimal>("sale_ex"))).Sum();

               var nonMemberSales = (from r in results.AsEnumerable()
                                     where r.Field<int?>("customer_id") == null
                                     select ((decimal)r.Field<double>("quantity") * r.Field<decimal>("sale_ex"))).Sum();


               var totalSales = (from r in results.AsEnumerable()
                                 select ((decimal)r.Field<double>("quantity") * r.Field<decimal>("sale_ex"))).Sum();


               var totalProfit = totalSales - totalCosts;

               var memberProfit = memberSales - memberCosts;
               var nonMemberProfit = nonMemberSales - nonMemberCosts;
               
               DataTable salesSummary = new DataTable();

               salesSummary.Columns.Add("type");
               salesSummary.Columns.Add("costs");
               salesSummary.Columns.Add("sales");
               salesSummary.Columns.Add("profit");


               salesSummary.Rows.Add("Member", memberCosts.ToString("#0"), memberSales.ToString("#0"), memberProfit.ToString("#0"));
               salesSummary.Rows.Add("NonMember", nonMemberCosts.ToString("#0"), nonMemberSales.ToString("#0"), nonMemberProfit.ToString("#0"));
               salesSummary.Rows.Add("Total", totalCosts.ToString("#0"), totalSales.ToString("#0"), totalProfit.ToString("#0"));

               SalesSummaryGridView.DataSource = salesSummary;
               SalesSummaryGridView.DataBind();

               var itemList = from r in results.AsEnumerable()
                              group r by new { barcode = r.Field<string>("product_barcode"), description = r.Field<string>("description") } into uniqueItems                              
                              select new
                              {
                                   barcode = uniqueItems.Key.barcode,
                                   description = uniqueItems.Key.description,
                                   quantity = uniqueItems.Sum(row => row.Field<double>("quantity")),
                                   costs = uniqueItems.Sum(row => (decimal)row.Field<double>("quantity") * row.Field<decimal>("cost_ex")),
                                   sales = uniqueItems.Sum(row => (decimal)row.Field<double>("quantity") * row.Field<decimal>("sale_ex")),
                                   profits = uniqueItems.Sum(row => (decimal)row.Field<double>("quantity") * (row.Field<decimal>("sale_ex") - row.Field<decimal>("cost_ex")))
                              };


               ItemsGridView.DataSource = itemList.OrderByDescending(r => r.quantity);
               ItemsGridView.DataBind();



               var customerList =  from r in results.AsEnumerable()
                                   where r.Field<int?>("customer_id") != null
                                   group r by new 
                                   { customer_id = r.Field<int?>("customer_id"), 
                                     title = r.Field<string>("title"), 
                                     first_name = r.Field<string>("first_name"),
                                     last_name = r.Field<string>("last_name"),
                                     mobile = r.Field<string>("mobile"),
                                     email = r.Field<string>("email"),
                                     suburb = r.Field<string>("suburb"),
                                     postcode = r.Field<string>("postcode")

                                   
                                   
                                   } into uniqueCustomers
                              
                                   select new
                                   {
                                        customer_id = uniqueCustomers.Key.customer_id,
                                        title = uniqueCustomers.Key.title,
                                        first_name = uniqueCustomers.Key.first_name,
                                        last_name = uniqueCustomers.Key.last_name,
                                        mobile = uniqueCustomers.Key.mobile,
                                        email = uniqueCustomers.Key.email,
                                        suburb = uniqueCustomers.Key.suburb,
                                        postcode = uniqueCustomers.Key.postcode,

                                        profits = uniqueCustomers.Sum(row => (decimal)row.Field<double>("quantity") * (row.Field<decimal>("sale_ex") - row.Field<decimal>("cost_ex")))
                                   };

               CustomersGridView.DataSource = customerList.OrderByDescending(r=>r.profits);
               CustomersGridView.DataBind();
          }

          private DataTable GetRawData()
          {
               int storeID = Convert.ToInt32(Server.HtmlDecode(StoresDropDownList.SelectedValue));
               string department = DepartmentsDropDownList.SelectedValue;

               var startDate = Convert.ToDateTime(StartDateTextBox.Text);
               var endDate = Convert.ToDateTime(EndDateTextBox.Text);


               var results = ReportsHelper.getCategoryAnalysisRawData(department, storeID, startDate, endDate);

               return results;
          }


          //SMS tools

          protected void SendSMSButton_Click(object sender, EventArgs e)
          {
               if (IsValid)
               {
                    string message = MessageTextBox.Text;

                    if (message.Length == 0)
                    {
                         SendSMSLiteral.Text = "Message is empty";
                    }
                    else
                    {
                         try
                         {
                              List<string> validMobiles = CalculateSMSCosts();

                              if (validMobiles.Count == 0)
                              {
                                   SendSMSLiteral.Text = "No valid mobile numbers selected.";
                              }
                              else
                              {
                                   var mobiles = new StringBuilder();

                                   foreach (string item in validMobiles)
                                   {
                                        mobiles.Append(item).Append(",");
                                   }

                                   if (!SMSHelper.QuotaAvailable(validMobiles.Count))
                                   {
                                        SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
                                   }
                                   else
                                   {
                                        OutgoingSMS newMessage = OutgoingSMS.CreateOutgoingSMSByadmin_(loggedInAdmin);

                                        newMessage.count = validMobiles.Count;
                                        newMessage.message_text = message;
                                        newMessage.notes = NotesTextBox.Text;
                                        newMessage.sent_datetime = DateTime.Now;
                                        newMessage.receipient_list = mobiles.ToString().TrimEnd(',');
                                        newMessage.verification_sms = Helpers.GenerateFiveDigitRandom();
                                        

                                        newMessage.Save();
                                        newMessage.Refresh();

                                        try
                                        {
                                             newMessage.response_list = SMSHelper.SendBulkSMS(validMobiles, message, newMessage.outgoingSMS_id);
                                             newMessage.Save();

                                             BillingItem newItem = BillingItem.CreateBillingItemBycompany_(currentCompany);

                                             newItem.description = validMobiles.Count.ToString() + " Bulk SMS sent for customers in Department:  " + DepartmentsDropDownList.SelectedValue + " between " + StartDateTextBox.Text + " and " + EndDateTextBox.Text;
                                             newItem.quantity = validMobiles.Count;
                                             newItem.unit_cost = SMSHelper.SMScost;
                                             newItem.total_amount = newItem.quantity * newItem.unit_cost;

                                             newItem.creation_datetime = DateTime.Now;

                                             newItem.Save();
                                             newItem.Refresh();

                                             newMessage.billingitem_id = newItem.billingitem_id;
                                             newMessage.Save();

                                             SendSMSLiteral.Text = validMobiles.Count.ToString() + " messages sent successfully.";

                                             EmailHelper.SendGenericEmail("vamsi@docketplace.com.au", "Bulk SMS sent by " + currentCompany.name, "");
                                        }
                                        catch
                                        {
                                             newMessage.Delete();
                                             SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
                                        }

                                   }
                              }
                         }
                         catch
                         {
                              SendSMSLiteral.Text = "An error has occurred please contact the website administrator";
                              throw;
                         }
                    }
               }
          }


          protected void CalculateCostButton_Click(object sender, EventArgs e)
          {
               List<string> validMobiles = CalculateSMSCosts();
          }

          private List<string> CalculateSMSCosts()
          {
               int invalidMobilesCount = 0;

               List<string> validMobiles = new List<string>();

               string invalidMobiles = "";

               int noSMSCount = 0;

               var customerList =  from r in GetRawData().AsEnumerable()
                                   where r.Field<int?>("customer_id") != null
                                   group r by new 
                                   { customer_id = r.Field<int?>("customer_id"), 
                                     title = r.Field<string>("title"), 
                                     first_name = r.Field<string>("first_name"),
                                     last_name = r.Field<string>("last_name"),
                                     mobile = r.Field<string>("mobile"),                                    
                                     
                                     no_sms = r.Field<bool>("no_sms"),
                                     mobile_broken = r.Field<bool>("mobile_broken")                                  
                                   
                                   } into uniqueCustomers
                              
                                   select new
                                   {
                                        customer_id = uniqueCustomers.Key.customer_id,
                                        title = uniqueCustomers.Key.title,
                                        first_name = uniqueCustomers.Key.first_name,
                                        last_name = uniqueCustomers.Key.last_name,
                                        mobile = uniqueCustomers.Key.mobile,
                                        no_sms = uniqueCustomers.Key.no_sms,
                                        mobile_broken = uniqueCustomers.Key.mobile_broken,                                        
                                   };


               //TODO Explain this better
               foreach (var customer in customerList)
               {
                    if (!String.IsNullOrEmpty(customer.mobile))
                    {                         
                         string cleanMobile = SMSHelper.sanitiseMobile(customer.mobile);

                         if (!SMSHelper.isMobileValid(cleanMobile) || customer.mobile_broken)
                         {
                              invalidMobilesCount++;
                              invalidMobiles += "," + customer.mobile;

                         }
                         else if (customer.no_sms)
                         {
                              noSMSCount++;

                         }
                         else
                         {
                              validMobiles.Add(cleanMobile);
                         }                     
                    }
               }

               decimal totalCost = (decimal)(validMobiles.Count * SMSHelper.SMScost);

               SMSCostLiteral.Text = validMobiles.Count.ToString() + " valid mobile numbers @ $" + SMSHelper.SMScost.ToString("#0.00") + "  each = $" + totalCost.ToString("#0.00");

               if (invalidMobilesCount > 0)
               {
                    SMSCostLiteral.Text += "<br /> Also " + invalidMobilesCount.ToString() + " invalid number(s) which have not been included.";
               }

               if (noSMSCount > 0)
               {
                    SMSCostLiteral.Text += "<br /> Also " + noSMSCount.ToString() + " unsubscribed number(s) which have not been included.";
               }

               LogHelper.WriteError("Dirty Mobiles: " + invalidMobiles);
               return validMobiles;
          }


          protected void PreviewSMSButton_Click(object sender, EventArgs e)
          {
               if (IsValid)
               {
                    PreviewSMSLiteral.Text = MessageTextBox.Text + "Unsub:Reply NO";
               }
          }


          //Mailchimp tools
          protected void MailchimpButtonButton_Click(object sender, EventArgs e)
          {
               MailChimp.MCApi chimp = new MailChimp.MCApi(currentCompany.mailchimp_apikey, true);

             
               try
               {
                    string groupTitle = "DEP: " + DepartmentsDropDownList.SelectedValue + " " + StartDateTextBox.Text + " - " + EndDateTextBox.Text;

                    List<string> groupsTemp = new List<string>();
                    groupsTemp.Add(groupTitle);
                    int groupingID = chimp.ListInterestGroupingAdd(currentCompany.mc_masterlist_id, groupTitle, List.GroupingType.Hidden, groupsTemp);
                                        

                    // Setup Subscribe Options that will be used for all records add or updated during the BatchListSubscribe
                    var options = new List.SubscribeOptions { DoubleOptIn = false, EmailType = List.EmailType.Html, UpdateExisting = true, ReplaceInterests = true };


                    var newGroupings = new List<List.Grouping>() 
                    {
                         new List.Grouping(groupTitle, new string[] {groupTitle}),                  
                    };



                    var batch = new List<List.Merges>();
                         
                    

                    var customerList =  from r in GetRawData().AsEnumerable()
                                   where r.Field<int?>("customer_id") != null
                                   group r by new 
                                   { customer_id = r.Field<int?>("customer_id"), 
                                     
                                     first_name = r.Field<string>("first_name"),
                                     last_name = r.Field<string>("last_name"),                                    
                                   
                                     email = r.Field<string>("email"),
                                     no_email = r.Field<bool>("no_email"),
                                     email_broken = r.Field<bool>("email_broken")                                  
                                   
                                   } into uniqueCustomers
                              
                                   select new
                                   {
                                        customer_id = uniqueCustomers.Key.customer_id,                                        
                                        first_name = uniqueCustomers.Key.first_name,
                                        last_name = uniqueCustomers.Key.last_name,                                        
                                        email = uniqueCustomers.Key.email,
                                        no_email = uniqueCustomers.Key.no_email,
                                        email_broken = uniqueCustomers.Key.email_broken,                                        
                                   };


                    foreach (var customer in customerList)
                    {
                         if (!String.IsNullOrEmpty(customer.email))
                         {
                              if (!(customer.no_email || customer.email_broken))
                              {
                                   string first_name = "";
                                   string last_name = "";

                                   if (!String.IsNullOrEmpty(customer.first_name))
                                   {
                                        first_name = customer.first_name;
                                   }

                                   if (!String.IsNullOrEmpty(customer.last_name))
                                   {
                                        last_name = customer.last_name;
                                   }

                                   batch.Add(new List.Merges(customer.email, List.EmailType.Html, newGroupings)
				          {
				                  {"FNAME", first_name},
				                  {"LNAME", last_name},							
				          }
                                   );
                              }
                         }
                    }

                    var returned = chimp.ListBatchSubscribe(currentCompany.mc_masterlist_id, batch, options);

                    MailchimpErrorLabel.Text += "Email records Added " + returned.AddCount + "</br>";
                    MailchimpErrorLabel.Text += "Email records Updated " + returned.UpdateCount + "</br>";
                    MailchimpErrorLabel.Text += "Email records Errors " + returned.ErrorCount + "</br>";
                    MailchimpErrorLabel.Text += "Group Title: " + groupTitle + "</br>";
                    
                    LogHelper.WriteError("Mailchimp group created for " + currentCompany.name);
                    
                    foreach (var error in returned.Errors)
                    {
                         LogHelper.WriteError("ERROR: " + error.Email + " " + error.Message + "\r\n");
                    }

               }
               catch (MailChimp.Types.MCException ex)
               {
                    MailchimpErrorLabel.Text = ex.Message;
               }
               catch (Exception ex)
               {
                    LogHelper.WriteError(ex.ToString());
                    MailchimpErrorLabel.Text = "Mailchimp group not created successfully. Please email help@docketplace.com.au.";
               }
          }

     }
}