using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.services
{
	/// <summary>
	/// Summary description for POSItemHandler
	/// </summary>
	[WebService(Namespace = "http://docketplace.com.au/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class POSItemHandler : System.Web.Services.WebService
	{

		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}

		public class LocalItem
		{
			public string product_code;
			public string product_barcode;
			public string description;
			public decimal cost_price;
			public decimal sale_price;
			public double quantity;
			public bool is_static;
			public DateTime modified_datetime;
		}

		public class LocalStocktakeTransaction
		{
			public string product_code;
			public string product_barcode;
			public string description;
			public double quantity;
			public string person;
			public DateTime stocktake_datetime;
		}

		public class ItemResponse
		{
			public bool is_error;
			public string errorMessage;
			public List<LocalItem> localItems;
			public List<LocalStocktakeTransaction> localStocktakeTransactions;
		}




		/// <summary>
		/// The POS Client app uses this to upload an item list into the database.
		/// </summary>
		/// <param name="newUpdate"></param>
		/// <returns></returns>
		[WebMethod]
		public ItemResponse UpdateOrReplaceItems(int storeID, string password, List<LocalItem> itemsToUpdate, bool replaceAll)
		{
			var newResponse = new ItemResponse();

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
						DateTime modified_datetime = DateTime.Now;

						if (replaceAll)
						{
							current_store.ItemsBystore_.DeleteAll();

							foreach (var item in itemsToUpdate)
							{
								CreateNewItem(current_store, item, modified_datetime);

								newResponse.is_error = false;
							}
						}
						else
						{
							foreach (var item in itemsToUpdate)
							{
								Item foundItem = Item.GetItem(item.product_code, storeID);

								if (foundItem == null)
								{
									CreateNewItem(current_store, item, modified_datetime);
								}
								newResponse.is_error = false;
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

		private static void CreateNewItem(Store current_store, LocalItem item, DateTime modifiedDateTime)
		{
			Item newItem = Item.CreateItem();

			newItem.store_id = current_store.store_id;
			newItem.product_code = item.product_code;
			newItem.product_barcode = item.product_barcode;
			newItem.description = item.description;
			newItem.cost_price = item.cost_price;
			newItem.sale_price = item.sale_price;
			newItem.quantity = item.quantity;
			newItem.is_static = item.is_static;
			newItem.modified_datetime = modifiedDateTime;
			newItem.Save();
		}



		/// <summary>
		/// The mobile app uses this to create the stocktake transaction list.
		/// </summary>
		/// <param name="newUpdate"></param>
		/// <returns></returns>
		[WebMethod]
		public ItemResponse UploadStockTakeTransactions(int storeID, string password, List<LocalStocktakeTransaction> transactions)
		{
			var newResponse = new ItemResponse();

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
						foreach (var item in transactions)
						{
							var newTransaction = StocktakeTransaction.CreateStocktakeTransaction();
							newTransaction.store_id = storeID;
							newTransaction.product_code = item.product_code;
							newTransaction.product_barcode = item.product_barcode;
							newTransaction.description = item.description;
							newTransaction.quantity = item.quantity;
							//Use the local datetiem from the mobile app so that client on the POS machine is probably in the same timezone.
							newTransaction.stocktake_datetime = item.stocktake_datetime;
							newTransaction.person = item.person;
							newTransaction.Save();
							newResponse.is_error = false;
						}
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


		/// <summary>
		/// POS client app use this to get the stocktake transactions.
		/// </summary>
		/// <returns></returns>
		[WebMethod]
		public ItemResponse GetStocktakeTransactions(int storeID, string password)
		{
			var newResponse = new ItemResponse();

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
						var transactionList = new List<LocalStocktakeTransaction>();

						foreach (var transaction in current_store.StocktakeTransactionsBystore_)
						{
							var localTransaction = new LocalStocktakeTransaction();
							localTransaction.product_code = transaction.product_code;
							localTransaction.product_barcode = transaction.product_barcode;
							localTransaction.description = transaction.description;
							localTransaction.quantity = transaction.quantity;
							localTransaction.stocktake_datetime = transaction.stocktake_datetime;
							localTransaction.person = transaction.person;
							transactionList.Add(localTransaction);
						}

						newResponse.is_error = false;
						newResponse.localStocktakeTransactions = transactionList;
						current_store.StocktakeTransactionsBystore_.DeleteAll();
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

			//// create a connection object
			//string connString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

			//// create a command object

			//string deleteCommand = @"delete StocktakeTransactions store_id = @store_id";

			//using (SqlConnection connection =	 new SqlConnection(connString))
			//{
			//     SqlCommand command = new SqlCommand(updateCommand, connection);	

			//     //add new parameter to command object
			//     command.Parameters.AddWithValue("@store_id", storeID);


			//     // Open the connection in a try/catch block. 
			//     // Create and execute the DataReader, writing the result
			//     // set to the console window.
			//     try
			//     {
			//          connection.Open();
			//          int rowsAffected = command.ExecuteNonQuery();
			//          connection.Close();
			//     }
			//     catch (Exception ex)
			//     {
			//          Console.WriteLine(ex.Message);
			//     }
			//     Console.ReadLine();
			//}



		}

		///// <summary>
		///// Mobile client app use this to get the details for the item list.
		///// </summary>
		///// <returns></returns>
		//[WebMethod]
		//public ItemResponse GetItemsForStocktake(int storeID, string password)
		//{
		//     var newResponse = new ItemResponse();

		//     try
		//     {
		//          Store current_store = Store.GetStore(storeID);


		//          if (current_store == null)
		//          {
		//               newResponse.is_error = true;
		//               newResponse.errorMessage = "NoStore";

		//          }
		//          else
		//          {
		//               if (password != current_store.password)
		//               {
		//                    newResponse.is_error = true;
		//                    newResponse.errorMessage = "IncorrectPassword";
		//               }
		//               else
		//               {

		//                    var newList = new List<LocalItem>();

		//                    foreach (var item in current_store.ItemsBystore_)
		//                    {
		//                         LocalItem newItem = new LocalItem();
		//                         newItem.product_code = item.product_code;
		//                         newItem.product_barcode = item.product_barcode;
		//                         newItem.description = item.description;
		//                         newItem.sale_price = item.sale_price;
		//                         newItem.quantity = item.quantity;
		//                         newItem.is_static = item.is_static;
		//                         newItem.modified_datetime = item.modified_datetime;
		//                         newList.Add(newItem);
		//                    }

		//                    newResponse.is_error = false;
		//                    newResponse.localItems = newList;
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


		///// <summary>
		///// 
		///// </summary>
		///// <param name="storeID"></param>
		///// <param name="password"></param>
		///// <returns></returns>
		//[WebMethod]
		//public ItemResponse GetItemsByBarcode(int storeID, string password, string productBarcode)
		//{
		//     var newResponse = new ItemResponse();

		//     try
		//     {
		//          Store current_store = Store.GetStore(storeID);


		//          if (current_store == null)
		//          {
		//               newResponse.is_error = true;
		//               newResponse.errorMessage = "NoStore";

		//          }
		//          else
		//          {
		//               if (password != current_store.password)
		//               {
		//                    newResponse.is_error = true;
		//                    newResponse.errorMessage = "IncorrectPassword";
		//               }
		//               else
		//               {

		//                    var newList = new List<LocalItem>();

		//                    foreach (var item in Item.GetItemsByStoreAndBarcode(storeID, productBarcode))
		//                    {
		//                         LocalItem newItem = new LocalItem();
		//                         newItem.product_code = item.product_code;
		//                         newItem.product_barcode = item.product_barcode;
		//                         newItem.description = item.description;
		//                         newItem.sale_price = item.sale_price;
		//                         newItem.quantity = item.quantity;
		//                         newItem.is_static = item.is_static;
		//                         newItem.modified_datetime = item.modified_datetime;
		//                         newList.Add(newItem);
		//                    }

		//                    newResponse.is_error = false;
		//                    newResponse.localItems = newList;
		//               }
		//          }
		//     }
		//     catch (Exception ex)
		//     {
		//          newResponse.is_error = true;
		//          newResponse.errorMessage = "GenericError";
		//          LogHelper.WriteError(ex.ToString());
		//     }
		//     return newResponse;
		//}
	}
}
