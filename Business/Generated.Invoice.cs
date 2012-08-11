/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 11/08/2012 4:51:03 PM.

     The NuSoft Framework is an open source project developed
     by NuSoft Solutions (http://www.nusoftsolutions.com).
     The latest version of the framework templates and detailed license
     is available at http://www.codeplex.com/NuSoftFramework.

     This file will be overwritten when regenerating your code.
</generated>
------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using DocketPlace.Business.Framework;


namespace DocketPlace.Business
{
	/// <summary>
	/// This object represents the properties and methods of a Invoice.
	/// </summary>
	[Serializable()]
	[DebuggerDisplay("invoice_id: {invoice_id}")]
	public partial class Invoice
	{
		#region Public Properties
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _invoice_id = int.MinValue;
		/// <summary>
		/// invoice_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(true, true, false)]
		public int invoice_id
		{
			[DebuggerStepThrough()]
			get { return this._invoice_id; }
			protected set 
			{
				if (this._invoice_id != value) 
				{
					this._invoice_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("invoice_id");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _company_id = int.MinValue;
		/// <summary>
		/// company_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public int company_id
		{
			[DebuggerStepThrough()]
			get { return this._company_id; }
			set 
			{
				if (this._company_id != value) 
				{
					this._company_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("company_id");
					this._company_ = null;
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _terms = String.Empty;
		/// <summary>
		/// terms
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public string terms
		{
			[DebuggerStepThrough()]
			get { return this._terms; }
			set 
			{
				if (this._terms != value) 
				{
					this._terms = value;
					this.IsDirty = true;	
					OnPropertyChanged("terms");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _notes = String.Empty;
		/// <summary>
		/// notes
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public string notes
		{
			[DebuggerStepThrough()]
			get { return this._notes; }
			set 
			{
				if (this._notes != value) 
				{
					this._notes = value;
					this.IsDirty = true;	
					OnPropertyChanged("notes");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _payment_method = String.Empty;
		/// <summary>
		/// payment_method
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public string payment_method
		{
			[DebuggerStepThrough()]
			get { return this._payment_method; }
			set 
			{
				if (this._payment_method != value) 
				{
					this._payment_method = value;
					this.IsDirty = true;	
					OnPropertyChanged("payment_method");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal _total_amount = decimal.MinValue;
		/// <summary>
		/// total_amount
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public decimal total_amount
		{
			[DebuggerStepThrough()]
			get { return this._total_amount; }
			set 
			{
				if (this._total_amount != value) 
				{
					this._total_amount = value;
					this.IsDirty = true;	
					OnPropertyChanged("total_amount");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _is_credit = false;
		/// <summary>
		/// is_credit
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public bool is_credit
		{
			[DebuggerStepThrough()]
			get { return this._is_credit; }
			set 
			{
				if (this._is_credit != value) 
				{
					this._is_credit = value;
					this.IsDirty = true;	
					OnPropertyChanged("is_credit");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _is_paid = false;
		/// <summary>
		/// is_paid
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public bool is_paid
		{
			[DebuggerStepThrough()]
			get { return this._is_paid; }
			set 
			{
				if (this._is_paid != value) 
				{
					this._is_paid = value;
					this.IsDirty = true;	
					OnPropertyChanged("is_paid");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime _start_datetime = DateTime.MinValue;
		/// <summary>
		/// start_datetime
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public DateTime start_datetime
		{
			[DebuggerStepThrough()]
			get { return this._start_datetime; }
			set 
			{
				if (this._start_datetime != value) 
				{
					this._start_datetime = value;
					this.IsDirty = true;	
					OnPropertyChanged("start_datetime");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime _end_datetime = DateTime.MinValue;
		/// <summary>
		/// end_datetime
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public DateTime end_datetime
		{
			[DebuggerStepThrough()]
			get { return this._end_datetime; }
			set 
			{
				if (this._end_datetime != value) 
				{
					this._end_datetime = value;
					this.IsDirty = true;	
					OnPropertyChanged("end_datetime");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime _creation_datetime = DateTime.MinValue;
		/// <summary>
		/// creation_datetime
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public DateTime creation_datetime
		{
			[DebuggerStepThrough()]
			get { return this._creation_datetime; }
			set 
			{
				if (this._creation_datetime != value) 
				{
					this._creation_datetime = value;
					this.IsDirty = true;	
					OnPropertyChanged("creation_datetime");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime _paid_datetime = DateTime.MinValue;
		/// <summary>
		/// paid_datetime
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public DateTime paid_datetime
		{
			[DebuggerStepThrough()]
			get { return this._paid_datetime; }
			set 
			{
				if (this._paid_datetime != value) 
				{
					this._paid_datetime = value;
					this.IsDirty = true;	
					OnPropertyChanged("paid_datetime");
				}
			}
		}
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Company _company_ = null;
		/// <summary>
		/// The parent Company object
		/// </summary>
		public Company company_
		{
			get 
			{
				if (_company_ == null) 
				{
					_company_ = GetParentEntity(Company.GetCompany(this.company_id)) as Company;
				}
				return _company_;
			}
			set
			{
				if(_company_ != value) 
				{
					_company_ = value;
					
					if (value != null) 
					{
						this.company_id = value.company_id;
					}
					else 
					{
						this.company_id = int.MinValue;
					}
				}
			}
		}
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntityList<BillingItem> _billingItemsByinvoice_ = null;
		/// <summary>
		/// A collection of BillingItem children objects
		/// </summary>
		public EntityList<BillingItem> BillingItemsByinvoice_
		{
			get 
			{
				if (_billingItemsByinvoice_ == null) {
					_billingItemsByinvoice_ = DocketPlace.Business.BillingItem.GetBillingItemsByinvoice_(this);
				}
				return _billingItemsByinvoice_;
			}
		}	
		#endregion
		
		#region Non-Public Properties
		/// <summary>
		/// Gets the SQL statement for an insert
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected override string InsertSPName
		{
			get 
			{
				return typeof(Invoice).Name + "Insert";
			}
		}
		
		/// <summary>
		/// Gets the SQL statement for an update by key
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected override string UpdateSPName
		{
			get
			{
				return typeof(Invoice).Name + "Update";
			}
		}
		
		/// <summary>
		/// Gets the SQL statement for a delete by key
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected override string DeleteSPName
		{
			get
			{
				return typeof(Invoice).Name + "Delete";
			}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// The default protected constructor
		/// </summary>
		protected Invoice() { }
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Creates a BillingItem for this Invoice object
		/// </summary>
		public BillingItem CreateBillingItem()
		{
			return DocketPlace.Business.BillingItem.CreateBillingItemByinvoice_(this);
		}
		

        /// <summary>
        /// Refreshes the entity with data from the data source. Child entity objects and entity list objects will be preserved (ie. they will not be replaced with new objects so that references to them are retained, such as bound data controls).
        /// </summary>
        public override void Refresh()
		{
			this.Replace(GetInvoice(this.invoice_id));
		}

		#endregion
		
		#region Non-Public Methods
		/// <summary>
		/// This is called before an entity is saved to ensure that any parent entities keys are set properly
		/// </summary>
		protected override void EnsureParentProperties()
		{
			if (_company_ != null)
			{	
				this.company_id = this.company_.company_id;
			}
			
		}
		#endregion
		
		#region Static Properties
		/// <summary>
		/// A list of all fields for this entity in the database. It does not include the 
		/// select keyword, or the table information - just the fields. This can be used
		/// for new dynamic methods.
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static string SelectFieldList 
		{
			get 
			{
				return @"
	[Invoices].[invoice_id],
	[Invoices].[company_id],
	[Invoices].[terms],
	[Invoices].[notes],
	[Invoices].[payment_method],
	[Invoices].[total_amount],
	[Invoices].[is_credit],
	[Invoices].[is_paid],
	[Invoices].[start_datetime],
	[Invoices].[end_datetime],
	[Invoices].[creation_datetime],
	[Invoices].[paid_datetime]
";
			}
		}
		
		
		/// <summary>
        /// Table Name
        /// </summary>
        public new static string TableName
        {
            get
            {
                return "Invoices";
            }
        }

		#endregion
		
		#region Static Methods
		/// <summary>
		/// Insert a Invoice into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <param name="terms">terms</param>
		/// <param name="notes">notes</param>
		/// <param name="payment_method">payment_method</param>
		/// <param name="total_amount">total_amount</param>
		/// <param name="is_credit">is_credit</param>
		/// <param name="is_paid">is_paid</param>
		/// <param name="start_datetime">start_datetime</param>
		/// <param name="end_datetime">end_datetime</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="paid_datetime">paid_datetime</param>
		public static void InsertInvoice(int @company_id, string @terms, string @notes, string @payment_method, decimal @total_amount, bool @is_credit, bool @is_paid, DateTime @start_datetime, DateTime @end_datetime, DateTime @creation_datetime, DateTime @paid_datetime)
		{
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.BeginTransaction();
            		InsertInvoice(@company_id, @terms, @notes, @payment_method, @total_amount, @is_credit, @is_paid, @start_datetime, @end_datetime, @creation_datetime, @paid_datetime, helper);
                    helper.Commit();
                }
                catch
                {
                    helper.Rollback();
                    throw;
                }
            }
		}

		/// <summary>
		/// Insert a Invoice into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <param name="terms">terms</param>
		/// <param name="notes">notes</param>
		/// <param name="payment_method">payment_method</param>
		/// <param name="total_amount">total_amount</param>
		/// <param name="is_credit">is_credit</param>
		/// <param name="is_paid">is_paid</param>
		/// <param name="start_datetime">start_datetime</param>
		/// <param name="end_datetime">end_datetime</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="paid_datetime">paid_datetime</param>
		/// <param name="helper">helper</param>
		internal static void InsertInvoice(int @company_id, string @terms, string @notes, string @payment_method, decimal @total_amount, bool @is_credit, bool @is_paid, DateTime @start_datetime, DateTime @end_datetime, DateTime @creation_datetime, DateTime @paid_datetime, SqlHelper @helper)
		{
			string commandText = "InvoiceInsert";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@terms", EntityBase.GetDatabaseValue(@terms)));
			parameters.Add(new SqlParameter("@notes", EntityBase.GetDatabaseValue(@notes)));
			parameters.Add(new SqlParameter("@payment_method", EntityBase.GetDatabaseValue(@payment_method)));
			parameters.Add(new SqlParameter("@total_amount", EntityBase.GetDatabaseValue(@total_amount)));
			parameters.Add(new SqlParameter("@is_credit", @is_credit));
			parameters.Add(new SqlParameter("@is_paid", @is_paid));
			parameters.Add(new SqlParameter("@start_datetime", EntityBase.GetDatabaseValue(@start_datetime)));
			parameters.Add(new SqlParameter("@end_datetime", EntityBase.GetDatabaseValue(@end_datetime)));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			parameters.Add(new SqlParameter("@paid_datetime", EntityBase.GetDatabaseValue(@paid_datetime)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Updates a Invoice into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="invoice_id">invoice_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="terms">terms</param>
		/// <param name="notes">notes</param>
		/// <param name="payment_method">payment_method</param>
		/// <param name="total_amount">total_amount</param>
		/// <param name="is_credit">is_credit</param>
		/// <param name="is_paid">is_paid</param>
		/// <param name="start_datetime">start_datetime</param>
		/// <param name="end_datetime">end_datetime</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="paid_datetime">paid_datetime</param>
		public static void UpdateInvoice(int @invoice_id, int @company_id, string @terms, string @notes, string @payment_method, decimal @total_amount, bool @is_credit, bool @is_paid, DateTime @start_datetime, DateTime @end_datetime, DateTime @creation_datetime, DateTime @paid_datetime)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try
				{
					helper.BeginTransaction();
					UpdateInvoice(@invoice_id, @company_id, @terms, @notes, @payment_method, @total_amount, @is_credit, @is_paid, @start_datetime, @end_datetime, @creation_datetime, @paid_datetime, helper);
					helper.Commit();
				}
				catch 
				{
					helper.Rollback();	
					throw;
				}
			}
		}
		
		/// <summary>
		/// Updates a Invoice into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="invoice_id">invoice_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="terms">terms</param>
		/// <param name="notes">notes</param>
		/// <param name="payment_method">payment_method</param>
		/// <param name="total_amount">total_amount</param>
		/// <param name="is_credit">is_credit</param>
		/// <param name="is_paid">is_paid</param>
		/// <param name="start_datetime">start_datetime</param>
		/// <param name="end_datetime">end_datetime</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="paid_datetime">paid_datetime</param>
		/// <param name="helper">helper</param>
		internal static void UpdateInvoice(int @invoice_id, int @company_id, string @terms, string @notes, string @payment_method, decimal @total_amount, bool @is_credit, bool @is_paid, DateTime @start_datetime, DateTime @end_datetime, DateTime @creation_datetime, DateTime @paid_datetime, SqlHelper @helper)
		{
			string commandText = "InvoiceUpdate";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@invoice_id", EntityBase.GetDatabaseValue(@invoice_id)));
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@terms", EntityBase.GetDatabaseValue(@terms)));
			parameters.Add(new SqlParameter("@notes", EntityBase.GetDatabaseValue(@notes)));
			parameters.Add(new SqlParameter("@payment_method", EntityBase.GetDatabaseValue(@payment_method)));
			parameters.Add(new SqlParameter("@total_amount", EntityBase.GetDatabaseValue(@total_amount)));
			parameters.Add(new SqlParameter("@is_credit", @is_credit));
			parameters.Add(new SqlParameter("@is_paid", @is_paid));
			parameters.Add(new SqlParameter("@start_datetime", EntityBase.GetDatabaseValue(@start_datetime)));
			parameters.Add(new SqlParameter("@end_datetime", EntityBase.GetDatabaseValue(@end_datetime)));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			parameters.Add(new SqlParameter("@paid_datetime", EntityBase.GetDatabaseValue(@paid_datetime)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Deletes a Invoice from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="invoice_id">invoice_id</param>
		public static void DeleteInvoice(int @invoice_id)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try 
				{
					helper.BeginTransaction();
					DeleteInvoice(@invoice_id, helper);
					helper.Commit();
				} 
				catch 
				{
					helper.Rollback();
					throw;
				}
			}
		}
		
		/// <summary>
		/// Deletes a Invoice from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="invoice_id">invoice_id</param>
		/// <param name="helper">helper</param>
		internal static void DeleteInvoice(int @invoice_id, SqlHelper @helper)
		{
			string commandText = "InvoiceDelete";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@invoice_id", @invoice_id));
		
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Creates a new Invoice object.
		/// </summary>
		/// <returns>The newly created Invoice object.</returns>
		public static Invoice CreateInvoice()
		{
			return InitializeNew<Invoice>();
		}
		
		/// <summary>
		/// Retrieve information for a Invoice by a Invoice's unique identifier.
		/// </summary>
		/// <param name="invoice_id">invoice_id</param>
		/// <returns>Invoice</returns>
		public static Invoice GetInvoice(int invoice_id)
		{
			string commandText = "InvoiceGet";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@invoice_id", invoice_id));
			
			return GetOne<Invoice>(commandText, parameters);
		}
		
		/// <summary>
		/// Gets a collection Invoice objects.
		/// </summary>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		public static EntityList<Invoice> GetInvoices()
		{
			string commandText = "InvoiceGetAll";
		
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			return GetList<Invoice>(commandText, parameters);
		}
		
		/// <summary>
        /// Gets a collection Invoice objects.
        /// </summary>
		/// <param name="orderBy">order by</param>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">PageSize</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Invoice objects.</returns>
        protected static EntityList<Invoice> GetInvoices(string orderBy, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Invoice>(SelectFieldList, "FROM [dbo].[Invoices]", new List<SqlParameter>(),orderBy,  startRowIndex, pageSize, out totalRows);
        }

		/// <summary>
        /// Gets a collection Invoice objects.
        /// </summary>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Invoice objects.</returns>
        public static EntityList<Invoice> GetInvoices(int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Invoice>(SelectFieldList, "FROM [dbo].[Invoices]", new List<SqlParameter>(), null,  startRowIndex, pageSize, out totalRows);
        }
		
		/// <summary>
		/// Gets a collection Invoice objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <param name="orderBy">the order by clause. Should start with "order by"</param>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoices(string where, SqlParameter parameter, string orderBy)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetInvoices(string.Empty, where, parameters, orderBy);
		}
		
		/// <summary>
		/// Gets a collection Invoice objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoices(string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetInvoices(string.Empty, where, parameters, Invoice.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Invoice objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoices(string prefix, string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetInvoices(prefix, where, parameters, Invoice.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Invoice objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoices(string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetInvoices(string.Empty, where, parameters, Invoice.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Invoice objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoices(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetInvoices(prefix, where, parameters, Invoice.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Invoice objects by custom where clause and order by.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <param name="orderBy">the order by clause. Shoudl start with "order by"</param>
		/// <returns>The retrieved collection of Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoices(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters, string orderBy)
		{
			string commandText = @"SELECT " + prefix + "" + Invoice.SelectFieldList + "FROM [dbo].[Invoices] " + where + " " + orderBy;			
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				using (IDataReader reader = helper.ExecuteDataReader(commandText, CommandType.Text, parameters))
				{
					return EntityBase.InitializeList<Invoice>(reader);
				}
			}
		}		
		
		/// <summary>
        /// Gets a collection Address objects.
        /// </summary>
		/// <param name="orderBy">order by</param>
		/// <param name="where">where</param>
		/// <param name=parameters">parameters</param>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">PageSize</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Address objects.</returns>
        protected static EntityList<Invoice> GetInvoices(string orderBy, string where, System.Collections.Generic.List<SqlParameter> parameters, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Invoice>(SelectFieldList, "FROM [dbo].[Invoices] " + where, parameters, orderBy, startRowIndex, pageSize, out totalRows);			
		}
		
		/// <summary>
		/// Gets a collection of Invoice objects by a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>A collection Invoice objects.</returns>
		public static EntityList<Invoice> GetInvoicesBycompany_(Company @company_) 
		{
			string commandText = "InvoiceGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<Invoice>(@company_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of Invoice objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoicesBycompany_(Company @company_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Invoices] 
WHERE 
	[Invoices].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<Invoice>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Invoice objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Invoice objects.</returns>
		protected static EntityList<Invoice> GetInvoicesBycompany_(int @company_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Invoices] 
WHERE 
	[Invoices].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<Invoice>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of Invoice objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Invoice objects.</returns>
		public static EntityList<Invoice> GetInvoicesBycompany_(Company @company_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Invoices] 
WHERE 
	[Invoices].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<Invoice>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Invoice objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Invoice objects.</returns>
		public static EntityList<Invoice> GetInvoicesBycompany_(int @company_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Invoices] 
WHERE 
	[Invoices].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<Invoice>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of Invoice objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <returns>A collection Invoice objects.</returns>
		public static EntityList<Invoice> GetInvoicesBycompany_(int @company_id) 
		{
			string commandText = "InvoiceGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<Invoice>(commandText, parameters);
		}

		/// <summary>
		/// Create a new Invoice object from a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>The newly created Invoice object.</returns>
		public static Invoice CreateInvoiceBycompany_(Company @company_)
		{
			Invoice invoice = InitializeNew<Invoice>();
			
			invoice.company_id = @company_.company_id;
			
			invoice.company_ = @company_;
			
			return invoice;
		}
		
		/// <summary>
		/// Deletes Invoice objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
		public static void DeleteInvoicesBycompany_(Company company) 
		{
			string commandText = "InvoiceDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company.company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes Invoice objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		public static void DeleteInvoicesBycompany_(int company_id) 
		{
			string commandText = "InvoiceDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		#endregion
		
		#region Subclasses
		public static partial class InvoiceProperties
		{
			public const string invoice_id = "invoice_id";
			public const string company_id = "company_id";
			public const string terms = "terms";
			public const string notes = "notes";
			public const string payment_method = "payment_method";
			public const string total_amount = "total_amount";
			public const string is_credit = "is_credit";
			public const string is_paid = "is_paid";
			public const string start_datetime = "start_datetime";
			public const string end_datetime = "end_datetime";
			public const string creation_datetime = "creation_datetime";
			public const string paid_datetime = "paid_datetime";
		}
		#endregion
	}
}
