using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Web.Script.Serialization;
using System.IO;
using System.Globalization;
using System.Web;
using DocketPlace.Business.Framework;

namespace WebApp.services
{
     // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerMobileHandler" in code, svc and config file together.


     [ServiceContract]
     [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
     public class CustomerMobileHandler
     {
          /// <summary>
          /// Note datetimes here are set to strings due to json limitations. In POS handler they are standard datetimes.
          /// </summary>

          public class LocalStore
          {
               public string company_id;
               public string name;
               public string barcode;
               public string base64Icon;
               public string base64Banner;
               public string info;
               public string current_points;
               public string needed_points;
          }

          public class LocalReceipt
          {
               public string receipt_id;
               public string company_id;
               public string content;
               public string total;
               public string points;
               public string creation_datetime;              
          }

          public class CustomerResponse
          {
               public bool is_error;
               public string errorMessage;
               public string stores;
               public string receipts;               
          }

          [WebGet(UriTemplate = "/TestConnection/{email}/{password}/{client_type}", ResponseFormat = WebMessageFormat.Json)]
          public CustomerResponse TestConnection(string email, string password, string client_type)
          {
               var newResponse = new CustomerResponse();

               try
               {
                    Customer foundCustomer = Customer.GetCustomerByEmail(email.ToLower().Trim());

                    if (foundCustomer == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoCustomer";

                    }
                    else
                    {
                         string password_hash = BusinessHelper.computeSHAhash(password, foundCustomer.creation_datetime);

                         if (password_hash != foundCustomer.password_hash)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                         else
                         {
                              newResponse.is_error = false;
                         }
                    }
                    LogHelper.WriteError(client_type);
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = ex.ToString();
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;
          }

          [WebGet(UriTemplate = "/NewPassword/{email}/{password}/{client_type}", ResponseFormat = WebMessageFormat.Json)]
          public CustomerResponse SendNewPassword(string email, string password, string client_type)
          {
               var newResponse = new CustomerResponse();

               try
               {
                    Customer foundCustomer = Customer.GetCustomerByEmail(email.ToLower().Trim());

                    if (foundCustomer == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoCustomer";

                    }
                    else
                    {
                         string new_password = foundCustomer.ResetPassword();
                         EmailHelper.PasswordResetEmail(foundCustomer.email, new_password);
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = ex.ToString();
                    LogHelper.WriteError(ex.ToString());
               }
               LogHelper.WriteError(client_type);
               return newResponse;
          }

          [WebGet(UriTemplate = "/Stores/{email}/{password}/{client_type}", ResponseFormat = WebMessageFormat.Json)]
          public CustomerResponse GetStoresForCustomer(string email, string password, string client_type)
          {
               var newResponse = new CustomerResponse();
               string iconPath = System.Web.HttpContext.Current.Server.MapPath("/images/mrswatson1.png");
               byte[] iconBytes = File.ReadAllBytes(iconPath);
               string base64Icon = System.Convert.ToBase64String(iconBytes);

               try
               {
                    Customer foundCustomer = Customer.GetCustomerByEmail(email.ToLower().Trim());

                    if (foundCustomer == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoCustomer";

                    }
                    else
                    {
                         string password_hash = BusinessHelper.computeSHAhash(password, foundCustomer.creation_datetime);

                         if (password_hash != foundCustomer.password_hash)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                         else
                         {
                              var stores = new List<LocalStore>();
                              foreach(var item in foundCustomer.MembersBycustomer_)
                              {
                                   var store = new LocalStore();
                                   store.name = item.company_.name;
                                   store.company_id = item.company_id.ToString();
                                   store.barcode = item.local_barcode_id;
                                   store.current_points = item.reward_points.ToString();
                                   store.needed_points = (item.company_.RewardSettingsBycompany_[0].points_threshold).ToString();
                                   store.base64Icon = base64Icon;
                                   store.base64Banner = base64Icon;
                                   store.info = "Hello World";
                                   stores.Add(store);
                              }

                              var jsonSerializer = new JavaScriptSerializer();
                              jsonSerializer.MaxJsonLength = Int32.MaxValue;

                              string rawData = jsonSerializer.Serialize(stores);

                              newResponse.stores = rawData;

                              
                              newResponse.is_error = false;

                              //MakeAccess(null, current_store, newList.Count, "DownloadItems", HttpUtility.UrlDecode(client_type), null);
                         }
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = ex.ToString();
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;
               LogHelper.WriteError(client_type);
          }

          [WebGet(UriTemplate = "/Receipts/{email}/{password}/{date}/{client_type}", ResponseFormat = WebMessageFormat.Json)]
          public CustomerResponse GetLatestReceipts(string email, string password, string date ,string client_type)
          {
               CultureInfo provider = CultureInfo.InvariantCulture;

               DateTime lastReceiptDate = DateTime.ParseExact(HttpUtility.UrlDecode(date), "yyyy-MM-dd_HH-mm-ss", provider);

               
               var newResponse = new CustomerResponse();

               try
               {
                    Customer foundCustomer = Customer.GetCustomerByEmail(email.ToLower().Trim());

                    if (foundCustomer == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoCustomer";

                    }
                    else
                    {
                         string password_hash = BusinessHelper.computeSHAhash(password, foundCustomer.creation_datetime);

                         if (password_hash != foundCustomer.password_hash)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                         else
                         {                              
                              var receipts = new List<LocalReceipt>();
                              foreach (var item in foundCustomer.DocketsBycustomer_.Where(d => d.creation_datetime > lastReceiptDate))
                              {
                                   var receipt = new LocalReceipt();
                                   receipt.receipt_id = item.docket_id.ToString();
                                   receipt.company_id = item.store_.company_id.ToString();

                                   if (String.IsNullOrEmpty(item.raw_content))
                                   {
                                        receipt.content = GenerateItemContent(item.DocketItemsBydocket_);
                                   }
                                   else
                                   {
                                        receipt.content = item.raw_content;
                                   }

                                   receipt.total = item.total.ToString("#0.00");
                                   receipt.points = item.reward_points.ToString();
                                   receipt.creation_datetime = item.creation_datetime.ToString("yyyy-MM-dd HH:mm:ss");
                                   receipts.Add(receipt);
                              }

                              var jsonSerializer = new JavaScriptSerializer();
                              jsonSerializer.MaxJsonLength = Int32.MaxValue;

                              string rawData = jsonSerializer.Serialize(receipts);

                              newResponse.receipts = rawData;


                              newResponse.is_error = false;

                              //MakeAccess(null, current_store, newList.Count, "DownloadItems", HttpUtility.UrlDecode(client_type), null);
                         }
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = ex.ToString();
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;
               
          }

          [WebGet(UriTemplate = "/Everything/{email}/{password}/{date}/{client_type}", ResponseFormat = WebMessageFormat.Json)]
          public CustomerResponse GetEverything(string email, string password, string date, string client_type)
          {

               var newResponse = new CustomerResponse();
               string iconPath = System.Web.HttpContext.Current.Server.MapPath("/images/mrswatson1.png");
               byte[] iconBytes = File.ReadAllBytes(iconPath);
               string base64Icon = System.Convert.ToBase64String(iconBytes);

               string bannerPath = System.Web.HttpContext.Current.Server.MapPath("/images/banner.png");
               byte[] bannerBytes = File.ReadAllBytes(bannerPath);
               string base64Banner = System.Convert.ToBase64String(bannerBytes);

               CultureInfo provider = CultureInfo.InvariantCulture;

               DateTime lastReceiptDate = DateTime.ParseExact(HttpUtility.UrlDecode(date), "yyyy-MM-dd_HH-mm-ss", provider);

               try
               {
                    Customer foundCustomer = Customer.GetCustomerByEmail(email.ToLower().Trim());

                    if (foundCustomer == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoCustomer";

                    }
                    else
                    {
                         string password_hash = BusinessHelper.computeSHAhash(password, foundCustomer.creation_datetime);

                         if (password_hash != foundCustomer.password_hash)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                         else
                         {
                              var receipts = new List<LocalReceipt>();
                              foreach (var item in foundCustomer.DocketsBycustomer_.Where(d => d.creation_datetime > lastReceiptDate))
                              {
                                   var receipt = new LocalReceipt();
                                   receipt.receipt_id = item.docket_id.ToString();
                                   receipt.company_id = item.store_.company_id.ToString();
                                   if(String.IsNullOrEmpty(item.raw_content))
                                   {
                                        receipt.content = GenerateItemContent(item.DocketItemsBydocket_);
                                   }
                                   else
                                   {
                                        receipt.content = item.raw_content;
                                   }
                                   
                                   receipt.total = item.total.ToString("#0.00");
                                   receipt.points = item.reward_points.ToString();
                                   receipt.creation_datetime = item.creation_datetime.ToString("yyyy-MM-dd HH:mm:ss");
                                   receipts.Add(receipt);
                              }

                              var jsonSerializer = new JavaScriptSerializer();
                              jsonSerializer.MaxJsonLength = Int32.MaxValue;

                              string rawData = jsonSerializer.Serialize(receipts);

                              newResponse.receipts = rawData;

                              //Stores

                              var stores = new List<LocalStore>();
                              foreach (var item in foundCustomer.MembersBycustomer_)
                              {
                                   var store = new LocalStore();
                                   store.name = item.company_.name;
                                   store.company_id = item.company_id.ToString();
                                   store.barcode = item.local_barcode_id;
                                   store.current_points = item.reward_points.ToString();
                                   store.needed_points = (item.company_.RewardSettingsBycompany_[0].points_threshold).ToString();
                                   store.base64Icon = base64Icon;
                                   store.base64Banner = base64Banner;
                                   store.info = "Hello World";
                                   stores.Add(store);
                              }
                                                           
                              string rawStoreData = jsonSerializer.Serialize(stores);

                              newResponse.stores = rawStoreData;


                              newResponse.is_error = false;

                              //MakeAccess(null, current_store, newList.Count, "DownloadItems", HttpUtility.UrlDecode(client_type), null);
                         }
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = ex.ToString();
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;

          }


          private string GenerateItemContent(EntityList<DocketItem> items)
          {
               string content = "";
               foreach (var item in items)
               {
                    content += item.description + " * " + item.quantity.ToString() + " @" + item.unit_cost.ToString("#0.00") + " = $" + (item.quantity * (double)item.unit_cost).ToString("#0.00") +"\n\n";
               }
               return content;
          }



          //[WebGet(UriTemplate = "/Messages/{email}/{password}/{date}/{client_type}", ResponseFormat = WebMessageFormat.Json)]
          //public CustomerResponse GetLatestMessages(string email, string password, string date, string client_type)
          //{
          //     CultureInfo provider = CultureInfo.InvariantCulture;

          //     DateTime lastReceiptDate = DateTime.ParseExact(HttpUtility.UrlDecode(date), "yyyy-MM-dd_HH-mm-ss", provider);


          //     var newResponse = new CustomerResponse();

          //     try
          //     {
          //          Customer foundCustomer = Customer.GetCustomerByEmail(email.ToLower().Trim());

          //          if (foundCustomer == null)
          //          {
          //               newResponse.is_error = true;
          //               newResponse.errorMessage = "NoCustomer";

          //          }
          //          else
          //          {
          //               string password_hash = BusinessHelper.computeSHAhash(password, foundCustomer.creation_datetime);

          //               if (password_hash != foundCustomer.password_hash)
          //               {
          //                    newResponse.is_error = true;
          //                    newResponse.errorMessage = "IncorrectPassword";
          //               }
          //               else
          //               {
          //                    var receipts = new List<LocalReceipt>();
          //                    foreach (var item in foundCustomer.DocketsBycustomer_.Where(d => d.creation_datetime > lastReceiptDate))
          //                    {
          //                         var receipt = new LocalReceipt();
          //                         receipt.receipt_id = item.docket_id.ToString();
          //                         receipt.company_id = item.store_.company_id.ToString();
          //                         receipt.content = item.raw_content;
          //                         receipt.total = item.total.ToString("#0.00");
          //                         receipt.points = item.reward_points.ToString();
          //                         receipt.creation_datetime = item.creation_datetime.ToString("yyyy-MM-dd HH:mm:ss");
          //                         receipts.Add(receipt);
          //                    }

          //                    var jsonSerializer = new JavaScriptSerializer();
          //                    jsonSerializer.MaxJsonLength = Int32.MaxValue;

          //                    string rawData = jsonSerializer.Serialize(receipts);

          //                    newResponse.receipts = rawData;


          //                    newResponse.is_error = false;

          //                    //MakeAccess(null, current_store, newList.Count, "DownloadItems", HttpUtility.UrlDecode(client_type), null);
          //               }
          //          }
          //     }
          //     catch (Exception ex)
          //     {
          //          newResponse.is_error = true;
          //          newResponse.errorMessage = ex.ToString();
          //          LogHelper.WriteError(ex.ToString());
          //     }
          //     return newResponse;

          //}

     }
}
