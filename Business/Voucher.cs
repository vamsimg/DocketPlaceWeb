/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 24/06/2011 11:27:03 PM.

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
	/// This object represents the properties and methods of a Voucher.
	/// </summary>
	public partial class Voucher : EntityBase
	{
		public static EntityList<Voucher> GetVouchersByDatesAndCompany(DateTime startDate, DateTime endDate, int companyID)
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@startDatetime", startDate));
			param.Add(new SqlParameter("@endDatetime", endDate));
			param.Add(new SqlParameter("@company_id", companyID));

			string where = "where company_id = @company_id and creation_datetime >= @startDatetime and creation_datetime <= @endDatetime";

			EntityList<Voucher> vouchers = Voucher.GetVouchers(where, param);

			return vouchers;
		}
	}
}
