/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 28/12/2009 6:19:28 PM.

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
using System.Linq;

using DocketPlace.Business.Framework;

namespace DocketPlace.Business
{
	/// <summary>
	/// This object represents the properties and methods of a Invoice.
	/// </summary>
	public partial class Invoice : EntityBase
	{
		public decimal subTotal
		{
			get
			{
				return this.BillingItemsByinvoice_.Sum(i => i.total_amount);
			}
		}

		public decimal gst
		{
			get
			{
				return this.subTotal * (decimal)0.1;
			}
		}

		public decimal calculatedTotal
		{
			get
			{
				return this.subTotal * (decimal)1.1;
			}
		}		
	}
}
