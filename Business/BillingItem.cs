/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 28/12/2009 6:18:55 PM.

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
	/// This object represents the properties and methods of a BillingItem.
	/// </summary>
	public partial class BillingItem : EntityBase
	{		
		public static EntityList<BillingItem> GetUnattachedBillingItems(int company_id)
		{
			SqlParameter parameter = new SqlParameter("@company_id", company_id);

			string where = "where company_id = @company_id and invoice_id IS NULL";

			EntityList<BillingItem> billingItems = BillingItem.GetBillingItems(where, parameter);
			return billingItems;	
		}

	}
}
