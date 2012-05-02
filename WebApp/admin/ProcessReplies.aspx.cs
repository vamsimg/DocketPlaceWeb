using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;

namespace WebApp.admin
{
     public partial class ProcessReplies : System.Web.UI.Page
     {
          private Admin loggedInAdmin;

          protected void Page_Load(object sender, EventArgs e)
          {
               loggedInAdmin = Helpers.GetLoggedInAdmin();

               if (!Helpers.IsSuperUser(loggedInAdmin))
               {
                    Response.Redirect("/status/errormessage.aspx?error=" + ErrorHelper.notsuperuser);
               }
          }
          
          protected void CheckButton_Click(object sender, EventArgs e)
          {
               DateTime selectedDate = Convert.ToDateTime(StartDateTextBox.Text);
               
               try
               {
                    EsendexInbox.message[] todaySMS = SMSHelper.GetReceivedSMSForDate(selectedDate);
                    var recentOutgoingSMS = OutgoingSMS.GetOutgoingSMSByDates(selectedDate.AddDays(-2), selectedDate.AddDays(1));

                    List<string> weirdMessages = new List<string>();

                    foreach (EsendexInbox.message item in todaySMS)
                    {
                         string senderMobile = item.originator;
                         string message = item.body.ToLower();

                         string processedMobile = "0" + senderMobile.Substring(senderMobile.Length - 9);

                         Customer replier = Customer.GetCustomerByMobile(processedMobile);
                         if (replier == null)
                         {
                              AddToWeirdMessages(weirdMessages, item);
                         }
                         else if (message.Contains("no") || message.Contains("stop"))
                         {
                              try
                              {
                                   //TODO its possible that an unsubscribe will result in the customer going no_sms for 2 different companies. Unlikely in 2011 but something to recode in future.
                                   foreach (Member memberOf in replier.MembersBycustomer_)
                                   {
                                        memberOf.no_sms = true;
                                        memberOf.Save();
                                   }

                                   foreach (OutgoingSMS sentMessage in recentOutgoingSMS)
                                   {
                                        if (sentMessage.receipient_list.Contains(processedMobile))
                                        {
                                             sentMessage.unsubscribe_list += processedMobile + ",";
                                             sentMessage.Save();
                                        }
                                   }
                              }
                              catch
                              {
                                   AddToWeirdMessages(weirdMessages, item);
                              }

                         }
                         else
                         {
                              AddToWeirdMessages(weirdMessages, item);
                         }
                    }
               }
               catch (Exception ex)
               {
                    LogHelper.WriteError(ex.Message);
               }
          }

          private static void AddToWeirdMessages(List<string> nonUnSubMessages, EsendexInbox.message item)
          {
               nonUnSubMessages.Add(item.receivedat.ToString() + "\t" + item.originator + "\t" + item.body);
          }    
     }
}