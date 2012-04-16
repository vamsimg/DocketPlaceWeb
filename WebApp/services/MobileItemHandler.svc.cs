using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using DocketPlace.Business;
using WebApp.AppCode;
using System.ServiceModel.Activation;

namespace WebApp.services
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MobileItemHandler" in code, svc and config file together.
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class MobileItemHandler
	{	
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

		
		[WebInvoke(UriTemplate = "/StocktakeTransactions/{id}/{password}", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json) ]
		public ItemResponse UploadStockTakeTransactions(string id, string password, List<LocalStocktakeTransaction> transactions)
		{
			var newResponse = new ItemResponse();


			try
			{
				int storeID = int.Parse(id);

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

		
		[WebGet(UriTemplate = "/Items/{id}/{password}",  ResponseFormat = WebMessageFormat.Json)]
		public ItemResponse GetItemsForStocktake(string id, string password)
		{
			var newResponse = new ItemResponse();

			try
			{
				int storeID = int.Parse(id);

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

						var newList = new List<LocalItem>();

						foreach (var item in current_store.ItemsBystore_)
						{
						LocalItem newItem = new LocalItem();
				

							newItem.product_code = item.product_code;
							newItem.product_barcode = item.product_barcode;
							newItem.description = item.description;
							newItem.cost_price = item.cost_price;
							newItem.sale_price = item.sale_price;
							newItem.quantity = item.quantity;
							newItem.is_static = item.is_static;
							newItem.modified_datetime = item.modified_datetime;
							newList.Add(newItem);
						}
						newResponse.is_error = false;
						newResponse.localItems = newList;
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

		
		[WebGet(UriTemplate = "/Items/{id}/{password}/{productBarcode}", ResponseFormat = WebMessageFormat.Json)]
		public ItemResponse GetItemsByBarcode(string id, string password, string productBarcode)
		{
			var newResponse = new ItemResponse();

			try
			{
				int storeID = int.Parse(id);
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

						var newList = new List<LocalItem>();

						foreach (var item in Item.GetItemsByStoreAndBarcode(storeID, productBarcode))
						{
							LocalItem newItem = new LocalItem();
							newItem.product_code = item.product_code;
							newItem.product_barcode = item.product_barcode;
							newItem.description = item.description;
							newItem.cost_price = item.cost_price;
							newItem.sale_price = item.sale_price;
							newItem.quantity = item.quantity;
							newItem.is_static = item.is_static;
							newItem.modified_datetime = item.modified_datetime;
							newList.Add(newItem);
						}

						newResponse.is_error = false;
						newResponse.localItems = newList;
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
	}
}
