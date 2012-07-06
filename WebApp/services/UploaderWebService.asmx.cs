using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Web.Script.Serialization;

namespace WebApp.services
{
     /// <summary>
     /// Summary description for UploaderWebService
     /// </summary>
     [WebService(Namespace = "http://docketplace.com.au/")]
     [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
     [System.ComponentModel.ToolboxItem(false)]
     // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     // [System.Web.Script.Services.ScriptService]
     public class UploaderWebService : System.Web.Services.WebService
     {

          [WebMethod]
          public string HelloWorld()
          {
               return "Hello World";
          }


          //public class LocalDocket
          //{
          //     public int local_id;
          //     public string receipt_content;
          //     public decimal total;
          //     public List<LocalDocketItem> itemList;
          //     public LocalCustomer localCustomer;
          //     public DateTime creation_datetime;
          //}

          //public class LocalDocketItem
          //{
          //     public string product_code;
          //     public string product_barcode;
          //     public string description;
          //     public decimal unit_cost;
          //     public double quantity;

          //}

          //public class LocalCustomer
          //{
          //     public string customer_id;
          //     public string mobile;
          //     public string phone;
          //     public string email;
          //     public string title;
          //     public string first_name;
          //     public string last_name;
          //     public string suburb;
          //     public string postcode;               
          //     public string barcode_id;
          //}



          public class UploaderResponse
          {
               public bool is_error;
               public string errorMessage;   
               
               //Dummy items
               public List<AdProvider.LocalCustomer> customers;
               public List<AdProvider.LocalDocket> dockets;
               public List<AdProvider.LocalDocketItem> items;
          }

          [WebMethod]
          public UploaderResponse TestConnection(int storeID, string password)
          {
               var newResponse = new UploaderResponse();
               newResponse.is_error = false;
               try
               {
                    Store current_store = Store.GetStore(storeID);

                    if (current_store == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoStore";
                    }
                    else
                    {
                         if (password != current_store.password)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = "GenericError";
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;
          }


          /// <returns></returns>
          [WebMethod]
          public UploaderResponse UploadCustomers(int storeID, string password, string compressedCustomers)
          {
               var newResponse = new UploaderResponse();

               try
               {
                    Store current_store = Store.GetStore(storeID);

                    if (current_store == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoStore";
                    }
                    else
                    {
                         if (password != current_store.password)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                         else
                         {
                              DateTime modifiedDatetime = DateTime.UtcNow;

                              string uncompressedItems = ZipHelper.DecompressFromGzip(compressedCustomers);
                              var jsonSerializer = new JavaScriptSerializer();
                              jsonSerializer.MaxJsonLength = Int32.MaxValue;

                              var customers = jsonSerializer.Deserialize<List<AdProvider.LocalCustomer>>(uncompressedItems);

                              foreach (var customer in customers)
                              {
                                   try
                                   {
                                        RewardsHelper.UpdateCustomerInfo(customer, current_store);
                                   }
                                   catch(Exception ex)
                                   {
                                        LogHelper.WriteError(ex.ToString());
                                   }
                              }
                         }
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = "GenericError";
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;
          }


          /// <returns></returns>
          [WebMethod]
          public UploaderResponse UploadDockets(int storeID, string password, WebApp.AdProvider.LocalDocket currentDocket)
          {
               var newResponse = new UploaderResponse();

               try
               {
                    Store current_store = Store.GetStore(storeID);

                    if (current_store == null)
                    {
                         newResponse.is_error = true;
                         newResponse.errorMessage = "NoStore";
                    }
                    else
                    {
                         if (password != current_store.password)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                         }
                         else
                         {
                              RewardsHelper.InsertNonRewardsDocket(currentDocket, current_store, false);                                  
                         }
                    }
               }
               catch (Exception ex)
               {
                    newResponse.is_error = true;
                    newResponse.errorMessage = "GenericError";
                    LogHelper.WriteError(ex.ToString());
               }
               return newResponse;
          }         
     }
}
