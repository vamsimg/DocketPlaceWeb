/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 28/12/2009 6:20:07 PM.

     The NuSoft Framework is an open source project developed
     by NuSoft Solutions (http://www.nusoftsolutions.com).
     The latest version of the framework templates and detailed license
     is available at http://www.codeplex.com/NuSoftFramework.

     This file will NOT be overwritten when regenerating your code.
</generated>
------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using DocketPlace.Business.Framework;

namespace DocketPlace.Business
{
	/// <summary>
	/// This object represents the properties and methods of a RequestedAd.
	/// </summary>
	public partial class RequestedAd : EntityBase
	{
		//public static EntityListReadOnly<RequestedAd> GetCurrentAdsForStore(int store_id)
		//{
		//     string commandText = 	";

		//     SqlParameter p = new SqlParameter("@store_id", store_id);

		//     List<SqlParameter> parameters = new List<SqlParameter>();
		//     parameters.Add(p);

		//     return EntityBaseReadOnly.GetListReadOnly<RequestedAd>(commandText, parameters);
		//}

		public static EntityList<RequestedAd> GetCurrentAdsForStore(int store_id)
		{
			SqlParameter p = new SqlParameter("@store_id", store_id);
			SqlParameter q = new SqlParameter("@currentDate", DateTime.Now);	

			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(p);
			parameters.Add(q);

               string where = "inner join AdMatches as m on m.admatch_id = [RequestedAds].admatch_id where m.store_id = @store_id and @currentDate between m.start_datetime and m.end_datetime and [RequestedAds].is_active = 1";

			EntityList<RequestedAd> currentAds = RequestedAd.GetRequestedAds(where, parameters);


			return currentAds;
		}

	}

	
}
