/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 24/06/2011 11:25:48 PM.

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
	/// This object represents the properties and methods of a Docket.
	/// </summary>
	public partial class Docket : EntityBase
	{

		public static Docket GetDocketByLocalIDAndStore(int local_id, int store_id)
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@local_id", local_id));
			param.Add(new SqlParameter("@store_id", store_id));
			

			string where = "where local_id = @local_id and store_id = @store_id";

			EntityList<Docket> dockets = Docket.GetDockets(where, param);

			if (dockets.Count > 0)
			{
				return dockets[0];
			}
			else
			{
				return null;
			}
		}

		public static EntityList<Docket> GetDocketsByDatesAndStore(DateTime startDate, DateTime endDate, int storeID)
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@startDatetime", startDate));
			param.Add(new SqlParameter("@endDatetime", endDate));
			param.Add(new SqlParameter("@store_id", storeID));

			string where = "where store_id = @store_id and creation_datetime >= @startDatetime and creation_datetime <= @endDatetime";

			EntityList<Docket> dockets = Docket.GetDockets(where, param);

			return dockets;
		}		
				
	}
}
