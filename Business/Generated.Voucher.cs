/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 09/09/2012 8:50:59 PM.

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
	/// This object represents the properties and methods of a Voucher.
	/// </summary>
	[Serializable()]
	[DebuggerDisplay("voucher_id: {voucher_id}")]
	public partial class Voucher
	{
		#region Public Properties
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _voucher_id = int.MinValue;
		/// <summary>
		/// voucher_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(true, true, false)]
		public int voucher_id
		{
			[DebuggerStepThrough()]
			get { return this._voucher_id; }
			protected set 
			{
				if (this._voucher_id != value) 
				{
					this._voucher_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("voucher_id");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _customer_id = int.MinValue;
		/// <summary>
		/// customer_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public int customer_id
		{
			[DebuggerStepThrough()]
			get { return this._customer_id; }
			set 
			{
				if (this._customer_id != value) 
				{
					this._customer_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("customer_id");
					this._customer_ = null;
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
		private string _code = String.Empty;
		/// <summary>
		/// code
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public string code
		{
			[DebuggerStepThrough()]
			get { return this._code; }
			set 
			{
				if (this._code != value) 
				{
					this._code = value;
					this.IsDirty = true;	
					OnPropertyChanged("code");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal _dollar_value = decimal.MinValue;
		/// <summary>
		/// dollar_value
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public decimal dollar_value
		{
			[DebuggerStepThrough()]
			get { return this._dollar_value; }
			set 
			{
				if (this._dollar_value != value) 
				{
					this._dollar_value = value;
					this.IsDirty = true;	
					OnPropertyChanged("dollar_value");
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
		private DateTime _expiry_datetime = DateTime.MinValue;
		/// <summary>
		/// expiry_datetime
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public DateTime expiry_datetime
		{
			[DebuggerStepThrough()]
			get { return this._expiry_datetime; }
			set 
			{
				if (this._expiry_datetime != value) 
				{
					this._expiry_datetime = value;
					this.IsDirty = true;	
					OnPropertyChanged("expiry_datetime");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private DateTime _used_datetime = DateTime.MinValue;
		/// <summary>
		/// used_datetime
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public DateTime used_datetime
		{
			[DebuggerStepThrough()]
			get { return this._used_datetime; }
			set 
			{
				if (this._used_datetime != value) 
				{
					this._used_datetime = value;
					this.IsDirty = true;	
					OnPropertyChanged("used_datetime");
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
		private Customer _customer_ = null;
		/// <summary>
		/// The parent Customer object
		/// </summary>
		public Customer customer_
		{
			get 
			{
				if (_customer_ == null) 
				{
					_customer_ = GetParentEntity(Customer.GetCustomer(this.customer_id)) as Customer;
				}
				return _customer_;
			}
			set
			{
				if(_customer_ != value) 
				{
					_customer_ = value;
					
					if (value != null) 
					{
						this.customer_id = value.customer_id;
					}
					else 
					{
						this.customer_id = int.MinValue;
					}
				}
			}
		}
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntityList<PointsLog> _pointsLogsByvoucher_ = null;
		/// <summary>
		/// A collection of PointsLog children objects
		/// </summary>
		public EntityList<PointsLog> PointsLogsByvoucher_
		{
			get 
			{
				if (_pointsLogsByvoucher_ == null) {
					_pointsLogsByvoucher_ = DocketPlace.Business.PointsLog.GetPointsLogsByvoucher_(this);
				}
				return _pointsLogsByvoucher_;
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
				return typeof(Voucher).Name + "Insert";
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
				return typeof(Voucher).Name + "Update";
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
				return typeof(Voucher).Name + "Delete";
			}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// The default protected constructor
		/// </summary>
		protected Voucher() { }
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Creates a PointsLog for this Voucher object
		/// </summary>
		public PointsLog CreatePointsLog()
		{
			return DocketPlace.Business.PointsLog.CreatePointsLogByvoucher_(this);
		}
		

        /// <summary>
        /// Refreshes the entity with data from the data source. Child entity objects and entity list objects will be preserved (ie. they will not be replaced with new objects so that references to them are retained, such as bound data controls).
        /// </summary>
        public override void Refresh()
		{
			this.Replace(GetVoucher(this.voucher_id));
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
			
			if (_customer_ != null)
			{	
				this.customer_id = this.customer_.customer_id;
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
	[Vouchers].[voucher_id],
	[Vouchers].[customer_id],
	[Vouchers].[company_id],
	[Vouchers].[code],
	[Vouchers].[dollar_value],
	[Vouchers].[creation_datetime],
	[Vouchers].[expiry_datetime],
	[Vouchers].[used_datetime]
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
                return "Vouchers";
            }
        }

		#endregion
		
		#region Static Methods
		/// <summary>
		/// Insert a Voucher into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="customer_id">customer_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="code">code</param>
		/// <param name="dollar_value">dollar_value</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="expiry_datetime">expiry_datetime</param>
		/// <param name="used_datetime">used_datetime</param>
		public static void InsertVoucher(int @customer_id, int @company_id, string @code, decimal @dollar_value, DateTime @creation_datetime, DateTime @expiry_datetime, DateTime @used_datetime)
		{
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.BeginTransaction();
            		InsertVoucher(@customer_id, @company_id, @code, @dollar_value, @creation_datetime, @expiry_datetime, @used_datetime, helper);
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
		/// Insert a Voucher into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="customer_id">customer_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="code">code</param>
		/// <param name="dollar_value">dollar_value</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="expiry_datetime">expiry_datetime</param>
		/// <param name="used_datetime">used_datetime</param>
		/// <param name="helper">helper</param>
		internal static void InsertVoucher(int @customer_id, int @company_id, string @code, decimal @dollar_value, DateTime @creation_datetime, DateTime @expiry_datetime, DateTime @used_datetime, SqlHelper @helper)
		{
			string commandText = "VoucherInsert";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", EntityBase.GetDatabaseValue(@customer_id)));
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@code", EntityBase.GetDatabaseValue(@code)));
			parameters.Add(new SqlParameter("@dollar_value", EntityBase.GetDatabaseValue(@dollar_value)));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			parameters.Add(new SqlParameter("@expiry_datetime", EntityBase.GetDatabaseValue(@expiry_datetime)));
			parameters.Add(new SqlParameter("@used_datetime", EntityBase.GetDatabaseValue(@used_datetime)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Updates a Voucher into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="voucher_id">voucher_id</param>
		/// <param name="customer_id">customer_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="code">code</param>
		/// <param name="dollar_value">dollar_value</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="expiry_datetime">expiry_datetime</param>
		/// <param name="used_datetime">used_datetime</param>
		public static void UpdateVoucher(int @voucher_id, int @customer_id, int @company_id, string @code, decimal @dollar_value, DateTime @creation_datetime, DateTime @expiry_datetime, DateTime @used_datetime)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try
				{
					helper.BeginTransaction();
					UpdateVoucher(@voucher_id, @customer_id, @company_id, @code, @dollar_value, @creation_datetime, @expiry_datetime, @used_datetime, helper);
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
		/// Updates a Voucher into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="voucher_id">voucher_id</param>
		/// <param name="customer_id">customer_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="code">code</param>
		/// <param name="dollar_value">dollar_value</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="expiry_datetime">expiry_datetime</param>
		/// <param name="used_datetime">used_datetime</param>
		/// <param name="helper">helper</param>
		internal static void UpdateVoucher(int @voucher_id, int @customer_id, int @company_id, string @code, decimal @dollar_value, DateTime @creation_datetime, DateTime @expiry_datetime, DateTime @used_datetime, SqlHelper @helper)
		{
			string commandText = "VoucherUpdate";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@voucher_id", EntityBase.GetDatabaseValue(@voucher_id)));
			parameters.Add(new SqlParameter("@customer_id", EntityBase.GetDatabaseValue(@customer_id)));
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@code", EntityBase.GetDatabaseValue(@code)));
			parameters.Add(new SqlParameter("@dollar_value", EntityBase.GetDatabaseValue(@dollar_value)));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			parameters.Add(new SqlParameter("@expiry_datetime", EntityBase.GetDatabaseValue(@expiry_datetime)));
			parameters.Add(new SqlParameter("@used_datetime", EntityBase.GetDatabaseValue(@used_datetime)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Deletes a Voucher from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="voucher_id">voucher_id</param>
		public static void DeleteVoucher(int @voucher_id)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try 
				{
					helper.BeginTransaction();
					DeleteVoucher(@voucher_id, helper);
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
		/// Deletes a Voucher from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="voucher_id">voucher_id</param>
		/// <param name="helper">helper</param>
		internal static void DeleteVoucher(int @voucher_id, SqlHelper @helper)
		{
			string commandText = "VoucherDelete";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@voucher_id", @voucher_id));
		
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Creates a new Voucher object.
		/// </summary>
		/// <returns>The newly created Voucher object.</returns>
		public static Voucher CreateVoucher()
		{
			return InitializeNew<Voucher>();
		}
		
		/// <summary>
		/// Retrieve information for a Voucher by a Voucher's unique identifier.
		/// </summary>
		/// <param name="voucher_id">voucher_id</param>
		/// <returns>Voucher</returns>
		public static Voucher GetVoucher(int voucher_id)
		{
			string commandText = "VoucherGet";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@voucher_id", voucher_id));
			
			return GetOne<Voucher>(commandText, parameters);
		}
		
		/// <summary>
		/// Gets a collection Voucher objects.
		/// </summary>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchers()
		{
			string commandText = "VoucherGetAll";
		
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			return GetList<Voucher>(commandText, parameters);
		}
		
		/// <summary>
        /// Gets a collection Voucher objects.
        /// </summary>
		/// <param name="orderBy">order by</param>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">PageSize</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Voucher objects.</returns>
        protected static EntityList<Voucher> GetVouchers(string orderBy, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Voucher>(SelectFieldList, "FROM [dbo].[Vouchers]", new List<SqlParameter>(),orderBy,  startRowIndex, pageSize, out totalRows);
        }

		/// <summary>
        /// Gets a collection Voucher objects.
        /// </summary>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Voucher objects.</returns>
        public static EntityList<Voucher> GetVouchers(int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Voucher>(SelectFieldList, "FROM [dbo].[Vouchers]", new List<SqlParameter>(), null,  startRowIndex, pageSize, out totalRows);
        }
		
		/// <summary>
		/// Gets a collection Voucher objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <param name="orderBy">the order by clause. Should start with "order by"</param>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchers(string where, SqlParameter parameter, string orderBy)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetVouchers(string.Empty, where, parameters, orderBy);
		}
		
		/// <summary>
		/// Gets a collection Voucher objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchers(string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetVouchers(string.Empty, where, parameters, Voucher.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Voucher objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchers(string prefix, string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetVouchers(prefix, where, parameters, Voucher.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Voucher objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchers(string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetVouchers(string.Empty, where, parameters, Voucher.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Voucher objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchers(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetVouchers(prefix, where, parameters, Voucher.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Voucher objects by custom where clause and order by.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <param name="orderBy">the order by clause. Shoudl start with "order by"</param>
		/// <returns>The retrieved collection of Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchers(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters, string orderBy)
		{
			string commandText = @"SELECT " + prefix + "" + Voucher.SelectFieldList + "FROM [dbo].[Vouchers] " + where + " " + orderBy;			
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				using (IDataReader reader = helper.ExecuteDataReader(commandText, CommandType.Text, parameters))
				{
					return EntityBase.InitializeList<Voucher>(reader);
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
        protected static EntityList<Voucher> GetVouchers(string orderBy, string where, System.Collections.Generic.List<SqlParameter> parameters, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Voucher>(SelectFieldList, "FROM [dbo].[Vouchers] " + where, parameters, orderBy, startRowIndex, pageSize, out totalRows);			
		}
		
		/// <summary>
		/// Gets a collection of Voucher objects by a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycompany_(Company @company_) 
		{
			string commandText = "VoucherGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<Voucher>(@company_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of Voucher objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchersBycompany_(Company @company_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Voucher objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchersBycompany_(int @company_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of Voucher objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycompany_(Company @company_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Voucher objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycompany_(int @company_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of Voucher objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycompany_(int @company_id) 
		{
			string commandText = "VoucherGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<Voucher>(commandText, parameters);
		}

		/// <summary>
		/// Create a new Voucher object from a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>The newly created Voucher object.</returns>
		public static Voucher CreateVoucherBycompany_(Company @company_)
		{
			Voucher voucher = InitializeNew<Voucher>();
			
			voucher.company_id = @company_.company_id;
			
			voucher.company_ = @company_;
			
			return voucher;
		}
		
		/// <summary>
		/// Deletes Voucher objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
		public static void DeleteVouchersBycompany_(Company company) 
		{
			string commandText = "VoucherDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company.company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes Voucher objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		public static void DeleteVouchersBycompany_(int company_id) 
		{
			string commandText = "VoucherDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		/// <summary>
		/// Gets a collection of Voucher objects by a Customer object.
		/// </summary>
		/// <param name="customer_">customer_</param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycustomer_(Customer @customer_) 
		{
			string commandText = "VoucherGetByCustomer";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", @customer_.customer_id));
			
			return GetList<Voucher>(@customer_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of Voucher objects by a Customer object.
		/// </summary>
		/// <param name="customer">customer</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchersBycustomer_(Customer @customer_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[customer_id] = @customer_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@customer_id", @customer_.customer_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Voucher objects by a Customer object.
		/// </summary>
		/// <param name="customer_id">customer_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		protected static EntityList<Voucher> GetVouchersBycustomer_(int @customer_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[customer_id] = @customer_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", @customer_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of Voucher objects by a Customer object.
		/// </summary>
		/// <param name="customer">customer</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycustomer_(Customer @customer_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[customer_id] = @customer_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@customer_id", @customer_.customer_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Voucher objects by a Customer object.
		/// </summary>
		/// <param name="customer_id">customer_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycustomer_(int @customer_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Vouchers] 
WHERE 
	[Vouchers].[customer_id] = @customer_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", @customer_id));
			
			return GetList<Voucher>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of Voucher objects by a Customer unique identifier.
		/// </summary>
		/// <param name="customer_id">customer_id</param>
		/// <returns>A collection Voucher objects.</returns>
		public static EntityList<Voucher> GetVouchersBycustomer_(int @customer_id) 
		{
			string commandText = "VoucherGetByCustomer";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", @customer_id));
			
			return GetList<Voucher>(commandText, parameters);
		}

		/// <summary>
		/// Create a new Voucher object from a Customer object.
		/// </summary>
		/// <param name="customer_">customer_</param>
		/// <returns>The newly created Voucher object.</returns>
		public static Voucher CreateVoucherBycustomer_(Customer @customer_)
		{
			Voucher voucher = InitializeNew<Voucher>();
			
			voucher.customer_id = @customer_.customer_id;
			
			voucher.customer_ = @customer_;
			
			return voucher;
		}
		
		/// <summary>
		/// Deletes Voucher objects by a Customer object.
		/// </summary>
		/// <param name="customer">customer</param>
		public static void DeleteVouchersBycustomer_(Customer customer) 
		{
			string commandText = "VoucherDeleteByCustomer";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", customer.customer_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes Voucher objects by a Customer unique identifier.
		/// </summary>
		/// <param name="customer_id">customer_id</param>
		public static void DeleteVouchersBycustomer_(int customer_id) 
		{
			string commandText = "VoucherDeleteByCustomer";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@customer_id", customer_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		#endregion
		
		#region Subclasses
		public static partial class VoucherProperties
		{
			public const string voucher_id = "voucher_id";
			public const string customer_id = "customer_id";
			public const string company_id = "company_id";
			public const string code = "code";
			public const string dollar_value = "dollar_value";
			public const string creation_datetime = "creation_datetime";
			public const string expiry_datetime = "expiry_datetime";
			public const string used_datetime = "used_datetime";
		}
		#endregion
	}
}
