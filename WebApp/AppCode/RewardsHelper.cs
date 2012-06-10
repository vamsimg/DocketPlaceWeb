using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocketPlace.Business;

namespace WebApp.AppCode
{

	/// <summary>
	/// Summary description for RewardsHelper
	/// </summary>
	public class RewardsHelper
	{
		public class RewardResults
		{
			public Docket newDocket;
			public Voucher newVoucher;
			public int rewardsBalance;
			public int rewardsThreshold;
		}

		/// <summary>
		/// Check the doddgy if statement
		/// </summary>
		/// <param name="localDocket"></param>
		/// <param name="currentStore"></param>
		/// <returns></returns>
		public static Docket InsertNonRewardsDocket(AdProvider.LocalDocket localDocket, Store currentStore, bool sendEmail)
		{
			Docket newDocket = Docket.CreateDocket();
			
			newDocket.code = Helpers.GenerateFiveDigitRandom();
			newDocket.store_id = currentStore.store_id;
			newDocket.creation_datetime = localDocket.creation_datetime;
			newDocket.raw_content = Helpers.DecodeFromBase64(localDocket.receipt_content);
			newDocket.total = localDocket.total;
			newDocket.reward_points = 0;
			newDocket.Save();
			newDocket.Refresh();

			if (currentStore.company_.are_lineitems_stored)
			{
				foreach (AdProvider.LocalDocketItem item in localDocket.itemList)
				{
					DocketItem newItem = newDocket.CreateDocketItem();
					newItem.product_code = item.product_code;
					newItem.product_barcode = item.product_barcode;
					newItem.description = item.description;
					newItem.unit_cost = item.unit_cost;
					newItem.quantity = item.quantity;
					newItem.Save();
				}
			}

			if (localDocket.localCustomer != null)
			{
				Member memberRecord = ProcessCustomerInfo(localDocket.localCustomer, currentStore);

				memberRecord.frequency++;
				memberRecord.total_revenue += localDocket.total;

				memberRecord.Save();

				newDocket.customer_id = memberRecord.customer_id;
				newDocket.Save();
				newDocket.Refresh();

				if (newDocket.customer_.email != "" && newDocket.customer_.email_broken == false && sendEmail)
				{
					EmailHelper.ReceiptEmail(newDocket.customer_.email, Helpers.DecodeFromBase64(localDocket.receipt_content), currentStore.company_.name);
				}
			}

			return newDocket;
		}


		/// <summary>
		/// Send the docket details to the web app, usually generateVoucher should be true so the customer receives a gift Voucher immediately, 
		/// otherwise may be false if the POS terminal was offline and the method is called the next morning to synch the past dockets. 
		/// </summary>
		/// <param name="localDocket"></param>
		/// <param name="currentStore"></param>
		/// <param name="generateVoucher"></param>
		/// <returns></returns>
		public static RewardResults InsertRewardsDocket(AdProvider.LocalDocket localDocket, Store currentStore, bool generateVoucher)
		{
			RewardResults newResult = new RewardResults();
			bool isReprint = false;

			try
			{
				Docket existingDocket = Docket.GetDocketByLocalIDAndStore(localDocket.local_id, currentStore.store_id);
				if (existingDocket == null)
				{

					Docket newDocket = Docket.CreateDocket();
					newDocket.local_id = localDocket.local_id;
					newDocket.code = Helpers.GenerateFiveDigitRandom();
					newDocket.store_id = currentStore.store_id;
					newDocket.creation_datetime = localDocket.creation_datetime;
					newDocket.raw_content = Helpers.DecodeFromBase64(localDocket.receipt_content);
					newDocket.total = localDocket.total;
					newDocket.reward_points = 0;
					newDocket.Save();
					newDocket.Refresh();

					newResult.newDocket = newDocket;

					if (currentStore.company_.are_lineitems_stored)
					{
						foreach (AdProvider.LocalDocketItem item in localDocket.itemList)
						{
							DocketItem newItem = newDocket.CreateDocketItem();
							newItem.product_code = item.product_code;
							newItem.product_barcode = item.product_barcode;
							newItem.description = item.description;
							newItem.unit_cost = item.unit_cost;
							newItem.quantity = item.quantity;
							newItem.Save();
						}
					}
				}
				else
				{
					newResult.newDocket = existingDocket;
					isReprint = true;
				}


				if (localDocket.localCustomer != null && currentStore.company_.is_rewards)
				{
					Member memberRecord = ProcessCustomerInfo(localDocket.localCustomer, currentStore);
					memberRecord.frequency++;
					memberRecord.total_revenue += localDocket.total;
					memberRecord.Save();


					newResult.newDocket.customer_id = memberRecord.customer_id;
					newResult.newDocket.Save();
					newResult.newDocket.Refresh();


					//Unique constraint should ensure only one.
					RewardSetting currentSettings = memberRecord.company_.RewardSettingsBycompany_[0];

					//If the docket is a reprint then do not add new points or create voucher. There should already be one in the system.
					if (!isReprint)
					{
						bool voucherAvailable = UpdateRewardPoints(newResult.newDocket, memberRecord, currentSettings);

						if (voucherAvailable == true && generateVoucher)
						{
							newResult.newVoucher = GenerateNewVoucher(memberRecord, currentSettings);
						}
					}

					newResult.rewardsBalance = memberRecord.reward_points;
					newResult.rewardsThreshold = currentSettings.points_threshold;

					//Send the customer an email receipt if email is available.
					if (newResult.newDocket.customer_.email != "" && newResult.newDocket.customer_.email_broken == false)
					{
						try
						{
							EmailHelper.ReceiptEmail(newResult.newDocket.customer_.email, Helpers.DecodeFromBase64(localDocket.receipt_content), currentStore.company_.name);
						}
						catch (Exception ex)
						{
							LogHelper.WriteError(ex.ToString());
						}
					}
				}
			}
			catch
			{
				throw;
			}

			return newResult;
		}

		private static Voucher GenerateNewVoucher(Member existingMember, RewardSetting currentSettings)
		{
			Voucher newVoucher = Voucher.CreateVoucherBycustomer_(existingMember.customer_);

			newVoucher.company_id = existingMember.company_id;
			newVoucher.code = Helpers.GenerateFiveDigitRandom().Trim();


			int numVouchers = existingMember.reward_points / currentSettings.points_threshold;
			int pointsRemaining = existingMember.reward_points % currentSettings.points_threshold;

			newVoucher.dollar_value = currentSettings.voucher_amount * numVouchers;

			newVoucher.creation_datetime = DateTime.Now;

			DateTime localDateTime = Helpers.ConvertServerDateTimetoLocal(DateTime.Now);

			DateTime expiryDateTime = new DateTime(localDateTime.Year, localDateTime.Month, localDateTime.Day).AddDays(currentSettings.expiry_days + 2);

			newVoucher.expiry_datetime = expiryDateTime;

			newVoucher.Save();

			existingMember.reward_points = pointsRemaining;
			existingMember.Save();

			PointsLog newLogItem = existingMember.customer_.CreatePointsLog();
			newLogItem.reward_points = -1 * numVouchers * currentSettings.points_threshold;
			newLogItem.voucher_id = newVoucher.voucher_id;
			newLogItem.company_id = existingMember.company_id;
			newLogItem.creation_datetime = newVoucher.creation_datetime;
			newLogItem.Save();

			//Adjust the new voucher's expiry datetime for the local setting.
			newVoucher.expiry_datetime = Helpers.ConvertServerDateTimetoLocal(newVoucher.expiry_datetime);

			return newVoucher;
		}

		private static bool UpdateRewardPoints(Docket currentDocket, Member existingMember, RewardSetting currentSettings)
		{

			int newPoints = Convert.ToInt32(Math.Ceiling(currentDocket.total * currentSettings.points_per_dollar));

			currentDocket.reward_points = newPoints;
			currentDocket.Save();

			existingMember.reward_points += newPoints;
			existingMember.Save();

			PointsLog newLogItem = existingMember.customer_.CreatePointsLog();
			newLogItem.reward_points = newPoints;
			newLogItem.docket_id = currentDocket.docket_id;
			newLogItem.company_id = existingMember.company_id;
			newLogItem.creation_datetime = DateTime.Now;
			newLogItem.Save();

			//Indicate if  a new voucher can be created if they have accumulated enough points. Check if vouchers are enaubled in reward settings.

			if (currentSettings.enable_vouchers)
			{
				return existingMember.reward_points > currentSettings.points_threshold;
			}
			else
			{
				return false;
			}
		}



		/// <summary>
		/// Check to see if a customer is a member of the store and if not create a member ship record. Returns the Member object.
		/// </summary>
		/// <param name="localCustomer"></param>
		/// <param name="currentStore"></param>
		/// <returns></returns>
		private static Member ProcessCustomerInfo(AdProvider.LocalCustomer localCustomer, Store currentStore)
		{
			Member existingMember = Member.GetMemberByStoreAndLocalID(localCustomer.customer_id.ToString(), currentStore.store_id);

			if (existingMember == null)
			{
				string sanitisedEmail = localCustomer.email.ToLower().Trim();
				string sanitisedMobile = localCustomer.mobile.Trim().Replace(" ", "");
				Customer existingCustomer = Customer.GetCustomerByEmail(sanitisedEmail);

				if (existingCustomer == null)
				{
					existingCustomer = Customer.GetCustomerByMobile(sanitisedMobile);

					if (existingCustomer == null)
					{
						existingCustomer = Customer.GetCustomerByPhone(localCustomer.phone);

						if (existingCustomer == null)
						{
							Customer newCustomer = Customer.CreateCustomer();
							newCustomer.title = localCustomer.title;
							newCustomer.first_name = localCustomer.first_name;
							newCustomer.last_name = localCustomer.last_name;

							newCustomer.email = sanitisedEmail;
							newCustomer.email_broken = false;
							newCustomer.mobile = sanitisedMobile;
							newCustomer.mobile_broken = false;
							newCustomer.phone = localCustomer.phone;
							newCustomer.suburb = localCustomer.suburb;
							newCustomer.postcode = localCustomer.postcode;
							newCustomer.verification_code = Helpers.GenerateFiveDigitRandom();
							newCustomer.is_active = true;


							newCustomer.creation_datetime = DateTime.Now;

							string newPassword = Helpers.GenerateFiveDigitRandom();
							newCustomer.password_hash = BusinessHelper.computeSHAhash(newPassword, newCustomer.creation_datetime);


							newCustomer.Save();
							newCustomer.Refresh();


							Member newMember = CreateMemberRecord(currentStore, newCustomer, localCustomer);

							if (newCustomer.email != "")
							{
								//Send receipt by email.						
								EmailHelper.CustomerAccountCreationEmail(newCustomer.email, newCustomer.full_name, newPassword, currentStore.company_.name);
							}
							return newMember;
						}
						else
						{
							return CreateMemberRecord(currentStore, existingCustomer, localCustomer);
						}
					}
					else
					{
						return CreateMemberRecord(currentStore, existingCustomer, localCustomer);
					}
				}
				else
				{
					return CreateMemberRecord(currentStore, existingCustomer, localCustomer);
				}
			}
			else
			{
                    //Barcode has changed.
                    if (existingMember.local_barcode_id != localCustomer.barcode_id)
                    {
                         existingMember.local_barcode_id = localCustomer.barcode_id;
                         existingMember.Save();
                    }

				return existingMember;
			}
		}

		public static Member CreateMemberRecord(Store currentStore, Customer existingCustomer, AdProvider.LocalCustomer localCustomer)
		{
			//Check if the customer is already a member of the company
			List<Member> existingMembers = Member.GetMembersBycustomer_(existingCustomer).ToList();

			Member existingMember = existingMembers.Find(element => element.company_id == currentStore.company_id);

			if (existingMember != null)
			{
				//If the customer has used the QR code tool to email themselves a receipt and the store has subsequently added them to their local database then we need to update their local customer ID.
				if (existingMember.local_customer_id == "None")
				{
					existingMember.local_customer_id = localCustomer.customer_id;
					existingMember.Save();
				}
				return existingMember;
			}
			else
			{
				Member newMember = Member.CreateMemberBycustomer_(existingCustomer);
				newMember.company_id = currentStore.company_id;
				newMember.store_id = currentStore.store_id;

				//The customer has scanned a QR code for an email receipt. They aren't in the local RM database.
				if (localCustomer != null)
				{
					newMember.local_customer_id = localCustomer.customer_id;
					newMember.local_barcode_id = localCustomer.barcode_id;
					newMember.grade = localCustomer.grade;
				}
				newMember.creation_datetime = DateTime.Now;
				newMember.reward_points = 0;
				newMember.frequency = 0;
				newMember.total_revenue = 0;
				newMember.Save();
				return newMember;
			}
		}

		/// <summary>
		/// Used only when the store owner manually clicks the sync customer button list. This takes new details and updates the client records.
		/// If the customer is a member of another company then the details are not updated.
		/// </summary>
		/// <param name="localCustomer"></param>
		/// <param name="currentStore"></param>
		/// <returns></returns>	
		public static string UpdateCustomerInfo(AdProvider.LocalCustomer localCustomer, Store currentStore)
		{
			Member existingMember = Member.GetMemberByStoreAndLocalID(localCustomer.customer_id.ToString(), currentStore.store_id);

			try
			{
				//Couldnt't find a member , so go looking in the database to see if there's an existing customer record for this 
				//guy. If there is then created a member record, if there isnt then create a member record.
				//If the customer is a member of another company then don't update details and overwrite.
				if (existingMember == null)
				{
					string sanitisedEmail = localCustomer.email.ToLower().Trim();
					string sanitisedMobile = localCustomer.mobile.Trim().Replace(" ", "");
					Customer existingCustomer = Customer.GetCustomerByEmail(sanitisedEmail);

					if (existingCustomer == null)
					{
						existingCustomer = Customer.GetCustomerByMobile(sanitisedMobile);

						if (existingCustomer == null)
						{
							existingCustomer = Customer.GetCustomerByPhone(localCustomer.phone);

							if (existingCustomer == null)
							{
								Customer newCustomer = Customer.CreateCustomer();
								newCustomer.title = localCustomer.title;
								newCustomer.first_name = localCustomer.first_name;
								newCustomer.last_name = localCustomer.last_name;

								newCustomer.email = sanitisedEmail;
								newCustomer.email_broken = false;
								newCustomer.mobile = sanitisedMobile;
								newCustomer.mobile_broken = false;
								newCustomer.phone = localCustomer.phone;
								newCustomer.suburb = localCustomer.suburb;
								newCustomer.postcode = localCustomer.postcode;
								newCustomer.verification_code = Helpers.GenerateFiveDigitRandom();
								newCustomer.is_active = true;


								newCustomer.creation_datetime = DateTime.Now;

								string newPassword = Helpers.GenerateFiveDigitRandom();
								newCustomer.password_hash = BusinessHelper.computeSHAhash(newPassword, newCustomer.creation_datetime);


								newCustomer.Save();
								newCustomer.Refresh();


								Member newMember = CreateMemberRecord(currentStore, newCustomer, localCustomer);

								if (newCustomer.email != "")
								{
									//Send new account email.						
									//EmailHelper.CustomerAccountCreationEmail(newCustomer.email, newCustomer.full_name, newPassword, currentStore.company_.name);
								}
								return "Customer number: " + localCustomer.customer_id + " created.";
							}
							else
							{
								return MakeActualCustomerUpdate(localCustomer, currentStore, existingCustomer);
							}
						}
						else
						{
							return MakeActualCustomerUpdate(localCustomer, currentStore, existingCustomer);
						}
					}
					else
					{

						return MakeActualCustomerUpdate(localCustomer, currentStore, existingCustomer);
					}
				}
				else
				{
					string details = UpdateCustomerDetails(localCustomer, existingMember.customer_);
					return "Customer number: " + localCustomer.customer_id + details + " updated.";
				}
			}
			catch (Exception ex)
			{
				LogHelper.WriteError(ex.ToString() + " " + "Store ID: " + currentStore.store_id + " Customer ID: " + localCustomer.customer_id);
				return "Error updating Customer number: " + localCustomer.customer_id;
			}
		}

		private static string MakeActualCustomerUpdate(AdProvider.LocalCustomer localCustomer, Store currentStore, Customer existingCustomer)
		{
			if (existingCustomer.MembersBycustomer_.Count == 0)
			{
				string details = UpdateCustomerDetails(localCustomer, existingCustomer);
				CreateMemberRecord(currentStore, existingCustomer, localCustomer);
				return "Customer number: " + localCustomer.customer_id + details + " updated.";
			}
			else
			{
				CreateMemberRecord(currentStore, existingCustomer, localCustomer);
				return "Customer number: " + localCustomer.customer_id + " not updated as they are also a customer of another company.";
			}

		}

		private static string UpdateCustomerDetails(AdProvider.LocalCustomer localCustomer, Customer existingCustomer)
		{
			string sectionsUpdated = "";
			if (existingCustomer.title != localCustomer.title)
			//if (String.IsNullOrEmpty(existingCustomer.title) && !String.IsNullOrEmpty(localCustomer.title))
			{
				existingCustomer.title = localCustomer.title;
				sectionsUpdated += " Title";
			}

			if (existingCustomer.first_name != localCustomer.last_name)
			{
				existingCustomer.first_name = localCustomer.first_name;
				sectionsUpdated += " First Name";
			}

			if (existingCustomer.last_name != localCustomer.last_name)
			{
				existingCustomer.last_name = localCustomer.last_name;
				sectionsUpdated += " Last Name";
			}

			string sanitisedEmail = localCustomer.email.ToLower().Trim();
			if (existingCustomer.email != sanitisedEmail)
			{
				existingCustomer.email = sanitisedEmail;
				existingCustomer.email_broken = false;
				sectionsUpdated += " email";
			}

			if (existingCustomer.phone != localCustomer.phone)
			{
				existingCustomer.phone = localCustomer.phone;
				sectionsUpdated += " Phone";
			}

			string sanitisedMobile = localCustomer.mobile.Trim().Replace(" ", "");

			if (existingCustomer.mobile != sanitisedMobile)
			{
				existingCustomer.mobile = sanitisedMobile;
				existingCustomer.mobile_broken = false;
				sectionsUpdated += " Mobile";

			}

			if (existingCustomer.suburb != localCustomer.suburb)
			{
				existingCustomer.suburb = localCustomer.suburb;
				sectionsUpdated += " suburb";
			}

			if (existingCustomer.postcode != localCustomer.postcode)
			{
				existingCustomer.postcode = localCustomer.postcode;
				sectionsUpdated += " postcode";
			}

			existingCustomer.Save();

			return sectionsUpdated;
		}


	}
}