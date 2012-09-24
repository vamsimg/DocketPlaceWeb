using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DocketPlace.Business;

using System.Web.Script.Serialization;
using WebServicesApp;
using WebServicesApp.AppCode;

namespace WebServicesApp.services
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
                         LogHelper.WriteError("No Store " + storeID.ToString());
                    }
                    else
                    {
                         if (password != current_store.password)
                         {
                              newResponse.is_error = true;
                              newResponse.errorMessage = "IncorrectPassword";
                              LogHelper.WriteError("Bad Password" + password);
                         }
                         else
                         {
                              DateTime modifiedDatetime = DateTime.Now;

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
                                   catch (Exception ex)
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
          public UploaderResponse UploadDockets(int storeID, string password, string compressedDockets)
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
                              string uncompressedItems = ZipHelper.DecompressFromGzip(compressedDockets);
                              var jsonSerializer = new JavaScriptSerializer();
                              jsonSerializer.MaxJsonLength = Int32.MaxValue;

                              var dockets = jsonSerializer.Deserialize<List<AdProvider.LocalDocket>>(uncompressedItems);

                              foreach (var docket in dockets)
                              {
                                   try
                                   {
                                        insertNonRewardsDocket(docket, current_store);
                                   }
                                   catch (Exception ex)
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
          public UploaderResponse UploadDocket(int storeID, string password, AdProvider.LocalDocket localDocket)
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
                              try
                              {
                                   insertNonRewardsDocket(localDocket, current_store);
                              }
                              catch (Exception ex)
                              {
                                   LogHelper.WriteError(ex.ToString());
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

           /// <summary>
          /// Check the doddgy if statement
          /// </summary>
          /// <param name="localDocket"></param>
          /// <param name="currentStore"></param>
          /// <returns></returns>
          public static void insertNonRewardsDocket(AdProvider.LocalDocket localDocket, Store currentStore)
          {
               try
               {
                    Docket existingDocket = Docket.GetDocketByLocalIDAndStore(localDocket.local_id, currentStore.store_id);
                    if (existingDocket != null && existingDocket.DocketItemsBydocket_.Count() == 0)
                    {
                         foreach (AdProvider.LocalDocketItem item in localDocket.itemList)
                         {
                              DocketItem newItem = existingDocket.CreateDocketItem();
                              newItem.product_code = item.product_code;
                              newItem.product_barcode = item.product_barcode;
                              newItem.department = item.department;
                              newItem.category = item.category;
                              newItem.description = item.description;

                              newItem.cost_ex = item.cost_ex;
                              newItem.sale_ex = item.sale_ex;
                              newItem.sale_inc = item.sale_inc;
                              newItem.quantity = item.quantity;
                              newItem.Save();
                         }
                    }
                    else
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


                         foreach (AdProvider.LocalDocketItem item in localDocket.itemList)
                         {
                              DocketItem newItem = newDocket.CreateDocketItem();
                              newItem.product_code = item.product_code;
                              newItem.product_barcode = item.product_barcode;
                              newItem.department = item.department;
                              newItem.category = item.category;
                              newItem.description = item.description;

                              newItem.cost_ex = item.cost_ex;
                              newItem.sale_ex = item.sale_ex;
                              newItem.sale_inc = item.sale_inc;
                              newItem.quantity = item.quantity;
                              newItem.Save();
                         }
                    }
               }
               catch (Exception ex)
               {
                    throw ex;
               }
          }
     }
}