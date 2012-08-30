using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DocketPlace.Business;
using WebApp.AppCode;
using DocketPlace.Business.Framework;

namespace WebApp
{
	/// <summary>
	/// Summary description for AdProvider
	/// </summary>
	[WebService(Namespace = "http://docketplace.com.au/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class AdProvider : System.Web.Services.WebService
	{
		public class AdRequest
		{
			public int store_id;
			public string password;
			public string IP;
			public LocalDocket currentDocket;
		}

		public class AdResponse
		{
			public string status;
			public bool is_error;
			public string header;
			public List<AdImage> adList;
			public int placedad_id;
		}

		public class AdImage
		{
			public string imageData;
			public string footer;
		}

		private static AdImage createNewAdImage(string newImageData, string newFooter)
		{
			AdImage newImage = new AdImage();
			newImage.imageData = newImageData;
			newImage.footer = newFooter;
			return newImage;
		}

		public class LocalDocket
		{
			public int local_id;
			public string receipt_content;
			public decimal total;
			public List<LocalDocketItem> itemList;
			public LocalCustomer localCustomer;
			public DateTime creation_datetime;
		}

		public class LocalDocketItem
		{
			public string product_code;
			public string product_barcode;
			public string description;
               public string department;
               public string category;          
               public decimal cost_ex;
               public decimal sale_ex;
               public decimal sale_inc;
               public double quantity;

		}

		public class LocalCustomer
		{
			public int customer_id;
			public string mobile;
			public string phone;
			public string email;
			public string title;
			public string first_name;
			public string last_name;
			public string suburb;
			public string postcode;
			/// <summary>
			/// Used only for RM Rewards where the retailer has a paid loyalty program. The grade will usually be A.
			/// </summary>
			public string grade;
			public string barcode_id;
		}

		public class VoucherCheck
		{
			public int store_id;
			public string password;
			public int voucher_id;
			public string voucher_code;
			public bool markAsUsed;
		}

		public class VoucherResponse
		{
			public string status;
			public bool is_error;
			public LocalCustomer owner;
		}

		public class CustomerUpdate
		{
			public int store_id;
			public string password;
			public List<LocalCustomer> customerList;
		}

		public class CustomerUpdateResponse
		{
			public string status;
			public bool is_error;
			public List<string> responses;
		}




		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World56";
		}

		[WebMethod]
		public AdResponse RequestAd(AdRequest new_request)
		{
			AdResponse new_response = new AdResponse();
			new_response.adList = new List<AdImage>();

			try
			{
				Store current_store = Store.GetStore(new_request.store_id);

				if (current_store == null)
				{
					new_response.is_error = true;
					new_response.status = "NoStore";

				}
				else if (new_request.password != current_store.password)
				{
					new_response.is_error = true;
					new_response.status = "IncorrectPassword";
				}
				else
				{
					new_response.header = "";

					if (new_request.currentDocket != null && current_store.company_.is_rewards)
					{
						RewardsHelper.RewardResults newResults = RewardsHelper.InsertRewardsDocket(new_request.currentDocket, current_store, true);

						//Display how many points for this docket and what the current balance is.
						if (newResults.newDocket.customer_ != null)
						{
							string header = "Rewards\r\n";

							header += "Thank you " + newResults.newDocket.customer_.full_name + "\r\n\r\n";
							header += "Reward points for this docket: " + newResults.newDocket.reward_points.ToString() + "\r\n\r\n";
							header += "Your Current Balance: " + newResults.rewardsBalance.ToString() + "\r\n\r\n";

							if (current_store.company_.RewardSettingsBycompany_[0].enable_vouchers == true)
							{
								header += "Next Voucher at: " + newResults.rewardsThreshold.ToString() + "\r\n\r\n";
							}

							new_response.header = header;
						}
						else
						{
                                   RewardSetting currentSetting = current_store.company_.RewardSettingsBycompany_[0];
							int possiblePoints = Convert.ToInt32(Math.Ceiling(new_request.currentDocket.total * currentSetting.points_per_dollar));

							string possibleRewards = "This receipt could have been worth \n{0} points.\n\n";
							string output = "";

							if (currentSetting.enable_vouchers)
							{
								possibleRewards += "Join our Rewards program to receive a \n${1} voucher for every {2} points in \npurchases.You also get exclusive offers to save you even more.\n\nAsk in-store for more details";
								output = String.Format(possibleRewards, possiblePoints.ToString(), currentSetting.voucher_amount.ToString("#0"), currentSetting.points_threshold.ToString());
							}
							else
							{
								possibleRewards += "Join our Rewards program to receive a \nspecial gift when you accumulate enough \npoints through purchases.\n\nAsk in-store for more details\n\n";
								output = String.Format(possibleRewards, possiblePoints.ToString());
							}

							new_response.header = output;
						}

						if (newResults.newVoucher != null)
						{
							string serverPath = System.Web.HttpContext.Current.Server.MapPath("/");
							System.Drawing.Image dummyAd = System.Drawing.Image.FromFile(serverPath + "/images/voucher.png");


							string voucherFooter = "Your voucher details are as follows:\r\n\r\n";
							voucherFooter += "Voucher Amount: $" + newResults.newVoucher.dollar_value.ToString("#0.00") + "\r\n\r\n";
							voucherFooter += "Voucher Number: " + newResults.newVoucher.voucher_id.ToString() + "\r\n\r\n";
							voucherFooter += "Voucher Code: " + newResults.newVoucher.code + "\r\n\r\n";
							voucherFooter += "Expiry Date: " + newResults.newVoucher.expiry_datetime.ToString("dd-MM-yyyy") + "\r\n\r\n\r\n";

							AdImage voucherImage = createNewAdImage(BusinessHelper.EncodeAd(dummyAd), voucherFooter);
							new_response.adList.Add(voucherImage);

							new_response.is_error = false;

							AdImage triggeredImage = ProcessTriggers(current_store, newResults.newDocket);
							if (triggeredImage != null)
							{
								new_response.adList.Add(triggeredImage);
							}


						}
						else
						{
							new_response = GetNewAd(new_request, new_response, current_store, newResults.newDocket);
						}
					}
					else if (new_request.currentDocket != null)
					{
						Docket newDocket = null;

						newDocket = RewardsHelper.InsertNonRewardsDocket(new_request.currentDocket, current_store, true);

						new_response = GetNewAd(new_request, new_response, current_store, newDocket);

					}
					else
					{
						new_response = GetNewAd(new_request, new_response, current_store, null);
					}

				}
			}
			catch (Exception ex)
			{
				new_response.is_error = true;
				new_response.status = "GenericError";
				LogHelper.WriteError(ex.ToString());
			}

			return new_response;
		}

		/// <summary>
		/// Bodgy code needs refactoring.
		/// </summary>
		/// <param name="newRequest"></param>
		/// <param name="new_response"></param>
		/// <param name="current_store"></param>
		/// <param name="newDocket"></param>
		/// <returns></returns>
		private AdResponse GetNewAd(AdRequest newRequest, AdResponse new_response, Store current_store, Docket newDocket)
		{
			//Check Triggers

			AdImage triggeredImage = null;

			if (newDocket != null)
			{
				triggeredImage = ProcessTriggers(current_store, newDocket);
			}


			if (triggeredImage != null)
			{
				new_response.adList.Add(triggeredImage);
			}
			else
			{
				//Get all active adrequests
				EntityList<RequestedAd> adList = RequestedAd.GetCurrentAdsForStore(current_store.store_id);

				string footer = "";

				if (adList.Count == 0)
				{
					if (current_store.default_uploadedad_ != null)
					{
						AdImage defaultImage = createNewAdImage(current_store.default_uploadedad_.data, footer);
						new_response.adList.Add(defaultImage);
					}
					else
					{
						string serverPath = System.Web.HttpContext.Current.Server.MapPath("/");
						System.Drawing.Image dummyAd = System.Drawing.Image.FromFile(serverPath + "/images/DummyAd.png");
						AdImage defaultImage = createNewAdImage(current_store.default_uploadedad_.data, footer);
						new_response.adList.Add(defaultImage);
					}
					new_response.is_error = false;
					new_response.status = "DefaultAd";
				}
				else
				{
					Random random = new Random();

					int random_index = random.Next(adList.Count);

					RequestedAd selected_ad = adList[random_index];
					selected_ad.num_printed++;
					selected_ad.daily_quota--;
					selected_ad.Save();

					footer = selected_ad.uploadedad_.footer;


					PlacedAd current_placement = PlacedAd.CreatePlacedAd();
					current_placement.uploadedad_id = selected_ad.uploadedad_id;
					current_placement.admatch_id = selected_ad.admatch_id;
					current_placement.placement_datetime = DateTime.Now;

					//Set the the type of ad, ie: wether its the parent company or another retailer or advertiser.
					if (selected_ad.admatch_.adgroup_.campaign_.company_id == current_store.company_id)
					{
						current_placement.owner_type = "Owner";
					}
					else if (selected_ad.admatch_.adgroup_.campaign_.company_.is_retailer)
					{
						current_placement.owner_type = "Retailer";
					}
					else
					{
						current_placement.owner_type = "Advertiser";
					}

					current_placement.Save();
					current_placement.Refresh();

					if (newDocket != null)
					{
						newDocket.placedad_id = current_placement.placedad_id;
						newDocket.Save();
					}

					AdImage newAdImage = createNewAdImage(selected_ad.uploadedad_.data, footer);
					new_response.adList.Add(newAdImage);

					new_response.placedad_id = current_placement.placedad_id;
					new_response.is_error = false;
				}
			}

			return new_response;
		}

		private static AdImage ProcessTriggers(Store current_store, Docket newDocket)
		{
			AdImage triggeredImage = null;
			foreach (Trigger item in current_store.TriggersBystore_.OrderBy(t => t.priority))
			{
				switch (item.type)
				{
					case "Member":
						if (item.value == "true" && newDocket.customer_ != null)
						{
							triggeredImage = createNewAdImage(item.uploadedad_.data, item.uploadedad_.footer);

							PlacedAd current_placement = PlacedAd.CreatePlacedAd();

							current_placement.uploadedad_id = item.uploadedad_.uploadedad_id;
							current_placement.trigger_id = item.trigger_id;
							current_placement.placement_datetime = DateTime.Now;

							current_placement.owner_type = "Owner";

							current_placement.Save();
							current_placement.Refresh();

							newDocket.placedad_id = current_placement.placedad_id;
							newDocket.Save();

							return triggeredImage;
						}
						else if (item.value == "false" && newDocket.customer_ == null)
						{
							return triggeredImage = createNewAdImage(item.uploadedad_.data, item.uploadedad_.footer);
						}
						break;
					case "Points":
						if (newDocket.customer_ != null)
						{
							Member currentMember = newDocket.customer_.MembersBycustomer_.Where(m => m.company_id == current_store.company_.company_id).First();
							if (currentMember.reward_points >= Convert.ToInt32(item.value))
							{
								return triggeredImage = createNewAdImage(item.uploadedad_.data, item.uploadedad_.footer);
							}
						}
						break;
					case "Purchase":
						if (newDocket != null)
						{
							if (newDocket.total >= Convert.ToDecimal(item.value))
							{
								return triggeredImage = createNewAdImage(item.uploadedad_.data, item.uploadedad_.footer);
							}
						}
						break;
				}
			}
			return null;
		}




		[WebMethod]
		public AdResponse TestConnection(AdRequest new_request)
		{
			AdResponse new_response = new AdResponse();
			new_response.adList = new List<AdImage>();
			try
			{
				Store current_store = Store.GetStore(new_request.store_id);

				if (current_store == null)
				{
					new_response.is_error = true;
					new_response.status = "NoStore";
				}
				else
				{
					if (new_request.password != current_store.password)
					{
						new_response.is_error = true;
						new_response.status = "IncorrectPassword";
					}
					else
					{
						string serverPath = System.Web.HttpContext.Current.Server.MapPath("/");
						System.Drawing.Image SolidConnection = System.Drawing.Image.FromFile(serverPath + "/images/SolidConnection.png");

						AdImage solidConn = createNewAdImage(BusinessHelper.EncodeAd(SolidConnection), "");
						new_response.header = "";
						new_response.adList.Add(solidConn);
						new_response.is_error = false;
					}
				}
			}
			catch (Exception ex)
			{
				new_response.is_error = true;
				new_response.status = "GenericError";
				LogHelper.WriteError(ex.ToString());
			}
			return new_response;
		}

		[WebMethod]
		public AdResponse InsertUnsentDocket(AdRequest new_request)
		{
			AdResponse new_response = new AdResponse();

			try
			{
				Store current_store = Store.GetStore(new_request.store_id);

				if (current_store == null)
				{
					new_response.is_error = true;
					new_response.status = "NoStore";
				}
				else
				{
					if (new_request.password != current_store.password)
					{
						new_response.is_error = true;
						new_response.status = "IncorrectPassword";
					}
					else
					{
						if (current_store.company_.is_rewards)
						{
							RewardsHelper.RewardResults newResults = RewardsHelper.InsertRewardsDocket(new_request.currentDocket, current_store, false);
						}
						else
						{
							Docket newDocket = null;
							newDocket = RewardsHelper.InsertNonRewardsDocket(new_request.currentDocket, current_store, true);
						}
						new_response.status = "Docket Insert Sucess";
						new_response.is_error = false;
					}
				}
			}
			catch (Exception ex)
			{
				new_response.is_error = true;
				new_response.status = "GenericError";
				LogHelper.WriteError(ex.ToString());
			}
			return new_response;
		}

		[WebMethod]
		public VoucherResponse ValidateVoucher(VoucherCheck new_request)
		{
			VoucherResponse new_response = new VoucherResponse();

			try
			{
				Store current_store = Store.GetStore(new_request.store_id);


				if (current_store == null)
				{
					new_response.is_error = true;
					new_response.status = "NoStore";

				}
				else
				{
					if (new_request.password != current_store.password)
					{
						new_response.is_error = true;
						new_response.status = "IncorrectPassword";
					}
					else
					{
						Voucher currentVoucher = Voucher.GetVoucher(new_request.voucher_id);
						if (currentVoucher == null)
						{
							new_response.is_error = true;
							new_response.status = "Voucher Not Found";
						}
						else if (!String.Equals(currentVoucher.code.Trim(), new_request.voucher_code))
						{
							new_response.is_error = true;
							new_response.status = "Incorrect voucher code";

						}
						else if (Helpers.isDateSet(currentVoucher.used_datetime) == true)
						{
							new_response.is_error = true;
							new_response.status = "Voucher already used on " + currentVoucher.used_datetime.ToString("dd--MMM-yyyy");
						}
						else if (new_request.markAsUsed == true)
						{
							currentVoucher.used_datetime = DateTime.Now;
							currentVoucher.Save();


							LocalCustomer owner = new LocalCustomer();
							owner.first_name = currentVoucher.customer_.first_name;
							owner.last_name = currentVoucher.customer_.last_name;
							new_response.owner = owner;


							new_response.is_error = false;
							new_response.status = "Voucher used successfully";
						}
					}
				}
			}
			catch (Exception ex)
			{
				new_response.is_error = true;
				new_response.status = "GenericError";
				LogHelper.WriteError(ex.ToString());
			}

			return new_response;
		}

		[WebMethod]
		public CustomerUpdateResponse UpdateCustomers(CustomerUpdate newUpdate)
		{
			CustomerUpdateResponse newResponse = new CustomerUpdateResponse();
			newResponse.responses = new List<string>();
			try
			{
				Store current_store = Store.GetStore(newUpdate.store_id);


				if (current_store == null)
				{
					newResponse.is_error = true;
					newResponse.status = "NoStore";

				}
				else
				{
					if (newUpdate.password != current_store.password)
					{
						newResponse.is_error = true;
						newResponse.status = "IncorrectPassword";
					}
					else
					{
						LogHelper.WriteStatus("Customer sync for company: " + current_store.company_.company_id.ToString() + "and store: " + current_store.store_id.ToString() + " started" + " # records: " + newUpdate.customerList.Count.ToString());
						foreach (LocalCustomer item in newUpdate.customerList)
						{
							newResponse.responses.Add(RewardsHelper.UpdateCustomerInfo(item, current_store));
						}
						LogHelper.WriteStatus("Customer sync for company: " + current_store.company_.company_id.ToString() + "and store: " + current_store.store_id.ToString() + " finished");
					}
				}
			}
			catch (Exception ex)
			{
				newResponse.is_error = true;
				newResponse.status = "GenericError";
				LogHelper.WriteError(ex.ToString());
			}
			return newResponse;
		}

	}
}
