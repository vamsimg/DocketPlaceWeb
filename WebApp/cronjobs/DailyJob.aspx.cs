using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.AppCode;
using DocketPlace.Business;
using System.Text;

namespace WebApp.cronjobs
{
	public partial class DailyJob : System.Web.UI.Page
	{
		/// <summary>
		/// The schedule task manager makes a get request to this page with the key below. This is to prevent any malicious attempts to trigger unnecessary emails.
		/// It's run at 3am each day Sydney time. 8am California time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			Page.Server.ScriptTimeout = 3600;

			string matchKey = "RTmUkTPJGqsdgsDcb5Ud";
			if (Request.QueryString["key"] != null)
			{
				string receivedKey = Request.QueryString["key"];

				if (receivedKey == matchKey)
				{
					DateTime localToday = DateTime.Now.Date;



					//ProcessBrokenMobiles(localToday);

					ProcessSMSReplies(localToday);

					ProcessEmailCampaigns(localToday);

				}
			}
		}

		private void ProcessEmailCampaigns(DateTime today)
		{
			LogHelper.WriteStatus("Mailchimp data processed for " + today.AddDays(-2).ToShortDateString() + " and " + today.AddDays(-1).ToShortDateString());

			var companies = Company.GetCompanies();


			var filter = new MailChimp.Types.Campaign.Filter();

			filter.SendtimeStart = (MailChimp.Types.Opt<DateTime>)today.AddDays(-2);
			filter.SendtimeEnd = (MailChimp.Types.Opt<DateTime>)today.AddDays(-1);


			foreach (Company currentCompany in companies)
			{
				if (!String.IsNullOrEmpty(currentCompany.mailchimp_apikey) && !String.IsNullOrEmpty(currentCompany.mc_masterlist_id))
				{
					MailChimp.MCApi chimp = new MailChimp.MCApi(currentCompany.mailchimp_apikey, true);

					var campaigns = chimp.Campaigns(filter);

					foreach (var campaign in campaigns.Data)
					{
						try
						{
							var unsubscribes = chimp.CampaignUnsubscribes(campaign.ID);


							foreach (var unsubscribeEmail in unsubscribes.Data)
							{
								try
								{
									var targetCustomer = Customer.GetCustomerByEmail(unsubscribeEmail.Email);
									if (targetCustomer != null)
									{
										Member currentMember = targetCustomer.MembersBycustomer_.Where(m => m.company_id == currentCompany.company_id).First();
										currentMember.no_email = true;
										currentMember.Save();
									}
								}
								catch (Exception ex)
								{
									LogHelper.WriteError(ex.Message);
								}
							}

						}
						catch (Exception ex)
						{
							LogHelper.WriteError(ex.Message);
						}

						try
						{
							var bounces = chimp.CampaignBounceMessages(campaign.ID);
							foreach (var bouncedEmail in bounces.Data)
							{
								try
								{
									var targetCustomer = Customer.GetCustomerByEmail(bouncedEmail.Email);
									if (targetCustomer != null)
									{
										targetCustomer.email_broken = true;
										targetCustomer.Save();
									}
								}
								catch (Exception ex)
								{
									LogHelper.WriteError(ex.Message);
								}
							}
						}
						catch (Exception ex)
						{
							LogHelper.WriteError(ex.Message);
						}

					}
				}
			}
		}



		/// <summary>
		/// //Get messages that have have failed to deliver today. Outgoing messages are timestamped with server time.
		/// </summary>
		private static void ProcessBrokenMobiles(DateTime today)
		{
			LogHelper.WriteStatus("Broken mobiles processed for " + today.AddDays(-1).ToShortDateString() + " and " + today.ToShortDateString());
			foreach (OutgoingSMS item in OutgoingSMS.GetOutgoingSMSByDates(today.AddDays(-1), today.AddDays(0)))
			{
				var brokenMobiles = new List<String>();
				try
				{
					if (!String.IsNullOrEmpty(item.response_list))
					{
						string[] mobiles = item.receipient_list.Split(',');
						string[] messageIDs = item.response_list.Split(',');


						int i = 0;
						foreach (string messageID in messageIDs)
						{
							if (!String.IsNullOrEmpty(messageID))
							{
								try
								{
									var response = SMSHelper.GetMessageResponse(messageID);
									if (response == EsendexSend.MessageStatusCode.Failed)
									{
										brokenMobiles.Add(mobiles[i]);
									}
									i++;
								}
								catch (Exception ex)
								{
									LogHelper.WriteError(ex.Message);
								}
							}
						}

					}
				}
				catch (Exception ex)
				{
					LogHelper.WriteError(ex.Message);
				}

				try
				{
					var builder = new StringBuilder();

					foreach (string mobile in brokenMobiles)
					{
						if (!String.IsNullOrEmpty(mobile))
						{
							var brokenCustomer = Customer.GetCustomerByMobile(mobile);
							if (brokenCustomer != null)
							{
								if (!brokenCustomer.mobile_broken)
								{
									brokenCustomer.mobile_broken = true;
									brokenCustomer.Save();
									builder.Append(mobile + ",");
								}
							}
						}
					}
					item.broken_list = builder.ToString();
					item.Save();
				}
				catch (Exception ex)
				{
					LogHelper.WriteError(ex.Message);
				}
			}
			LogHelper.WriteStatus("Broken mobiles finished for " + today.AddDays(-1).ToShortDateString() + " and " + today.ToShortDateString());
		}


		private void ProcessSMSReplies(DateTime today)
		{
			LogHelper.WriteStatus("Inbox replies processed for " + today.ToShortDateString());
			try
			{
				EsendexInbox.message[] todaySMS = SMSHelper.GetReceivedSMSForDate(today);
				var recentOutgoingSMS = OutgoingSMS.GetOutgoingSMSByDates(today.AddDays(-3), today.AddDays(1));

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

				if (weirdMessages.Count > 0)
				{
					ProcessWeirdMessages(weirdMessages);
				}

			}
			catch (Exception ex)
			{
				LogHelper.WriteError(ex.Message);
			}
		}

		private void ProcessWeirdMessages(List<string> weirdMessages)
		{
			string body = "";

			foreach (string item in weirdMessages)
			{
				body += item + "\n";
			}

			EmailHelper.SendGenericEmail(EmailHelper.sender, "Weird SMS", body);
		}

		private static void AddToWeirdMessages(List<string> nonUnSubMessages, EsendexInbox.message item)
		{
			nonUnSubMessages.Add(item.receivedat.ToString() + "\t" + item.originator + "\t" + item.body);
		}    
	}
}