/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 29/08/2012 9:37:21 PM.

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
	/// This object represents the properties and methods of a RewardSetting.
	/// </summary>
	[Serializable()]
	[DebuggerDisplay("setting_id: {setting_id}")]
	public partial class RewardSetting
	{
		#region Public Properties
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _setting_id = int.MinValue;
		/// <summary>
		/// setting_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(true, true, false)]
		public int setting_id
		{
			[DebuggerStepThrough()]
			get { return this._setting_id; }
			protected set 
			{
				if (this._setting_id != value) 
				{
					this._setting_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("setting_id");
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
		private int _expiry_days = int.MinValue;
		/// <summary>
		/// expiry_days
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public int expiry_days
		{
			[DebuggerStepThrough()]
			get { return this._expiry_days; }
			set 
			{
				if (this._expiry_days != value) 
				{
					this._expiry_days = value;
					this.IsDirty = true;	
					OnPropertyChanged("expiry_days");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _points_per_dollar = int.MinValue;
		/// <summary>
		/// points_per_dollar
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public int points_per_dollar
		{
			[DebuggerStepThrough()]
			get { return this._points_per_dollar; }
			set 
			{
				if (this._points_per_dollar != value) 
				{
					this._points_per_dollar = value;
					this.IsDirty = true;	
					OnPropertyChanged("points_per_dollar");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _points_threshold = int.MinValue;
		/// <summary>
		/// points_threshold
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public int points_threshold
		{
			[DebuggerStepThrough()]
			get { return this._points_threshold; }
			set 
			{
				if (this._points_threshold != value) 
				{
					this._points_threshold = value;
					this.IsDirty = true;	
					OnPropertyChanged("points_threshold");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private decimal _voucher_amount = decimal.MinValue;
		/// <summary>
		/// voucher_amount
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public decimal voucher_amount
		{
			[DebuggerStepThrough()]
			get { return this._voucher_amount; }
			set 
			{
				if (this._voucher_amount != value) 
				{
					this._voucher_amount = value;
					this.IsDirty = true;	
					OnPropertyChanged("voucher_amount");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _enable_vouchers = false;
		/// <summary>
		/// enable_vouchers
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public bool enable_vouchers
		{
			[DebuggerStepThrough()]
			get { return this._enable_vouchers; }
			set 
			{
				if (this._enable_vouchers != value) 
				{
					this._enable_vouchers = value;
					this.IsDirty = true;	
					OnPropertyChanged("enable_vouchers");
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
				return typeof(RewardSetting).Name + "Insert";
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
				return typeof(RewardSetting).Name + "Update";
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
				return typeof(RewardSetting).Name + "Delete";
			}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// The default protected constructor
		/// </summary>
		protected RewardSetting() { }
		#endregion
		
		#region Public Methods
		

        /// <summary>
        /// Refreshes the entity with data from the data source. Child entity objects and entity list objects will be preserved (ie. they will not be replaced with new objects so that references to them are retained, such as bound data controls).
        /// </summary>
        public override void Refresh()
		{
			this.Replace(GetRewardSetting(this.setting_id));
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
	[RewardSettings].[setting_id],
	[RewardSettings].[company_id],
	[RewardSettings].[expiry_days],
	[RewardSettings].[points_per_dollar],
	[RewardSettings].[points_threshold],
	[RewardSettings].[voucher_amount],
	[RewardSettings].[enable_vouchers]
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
                return "RewardSettings";
            }
        }

		#endregion
		
		#region Static Methods
		/// <summary>
		/// Insert a RewardSetting into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <param name="expiry_days">expiry_days</param>
		/// <param name="points_per_dollar">points_per_dollar</param>
		/// <param name="points_threshold">points_threshold</param>
		/// <param name="voucher_amount">voucher_amount</param>
		/// <param name="enable_vouchers">enable_vouchers</param>
		public static void InsertRewardSetting(int @company_id, int @expiry_days, int @points_per_dollar, int @points_threshold, decimal @voucher_amount, bool @enable_vouchers)
		{
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.BeginTransaction();
            		InsertRewardSetting(@company_id, @expiry_days, @points_per_dollar, @points_threshold, @voucher_amount, @enable_vouchers, helper);
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
		/// Insert a RewardSetting into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <param name="expiry_days">expiry_days</param>
		/// <param name="points_per_dollar">points_per_dollar</param>
		/// <param name="points_threshold">points_threshold</param>
		/// <param name="voucher_amount">voucher_amount</param>
		/// <param name="enable_vouchers">enable_vouchers</param>
		/// <param name="helper">helper</param>
		internal static void InsertRewardSetting(int @company_id, int @expiry_days, int @points_per_dollar, int @points_threshold, decimal @voucher_amount, bool @enable_vouchers, SqlHelper @helper)
		{
			string commandText = "RewardSettingInsert";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@expiry_days", EntityBase.GetDatabaseValue(@expiry_days)));
			parameters.Add(new SqlParameter("@points_per_dollar", EntityBase.GetDatabaseValue(@points_per_dollar)));
			parameters.Add(new SqlParameter("@points_threshold", EntityBase.GetDatabaseValue(@points_threshold)));
			parameters.Add(new SqlParameter("@voucher_amount", EntityBase.GetDatabaseValue(@voucher_amount)));
			parameters.Add(new SqlParameter("@enable_vouchers", @enable_vouchers));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Updates a RewardSetting into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="setting_id">setting_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="expiry_days">expiry_days</param>
		/// <param name="points_per_dollar">points_per_dollar</param>
		/// <param name="points_threshold">points_threshold</param>
		/// <param name="voucher_amount">voucher_amount</param>
		/// <param name="enable_vouchers">enable_vouchers</param>
		public static void UpdateRewardSetting(int @setting_id, int @company_id, int @expiry_days, int @points_per_dollar, int @points_threshold, decimal @voucher_amount, bool @enable_vouchers)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try
				{
					helper.BeginTransaction();
					UpdateRewardSetting(@setting_id, @company_id, @expiry_days, @points_per_dollar, @points_threshold, @voucher_amount, @enable_vouchers, helper);
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
		/// Updates a RewardSetting into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="setting_id">setting_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="expiry_days">expiry_days</param>
		/// <param name="points_per_dollar">points_per_dollar</param>
		/// <param name="points_threshold">points_threshold</param>
		/// <param name="voucher_amount">voucher_amount</param>
		/// <param name="enable_vouchers">enable_vouchers</param>
		/// <param name="helper">helper</param>
		internal static void UpdateRewardSetting(int @setting_id, int @company_id, int @expiry_days, int @points_per_dollar, int @points_threshold, decimal @voucher_amount, bool @enable_vouchers, SqlHelper @helper)
		{
			string commandText = "RewardSettingUpdate";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@setting_id", EntityBase.GetDatabaseValue(@setting_id)));
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@expiry_days", EntityBase.GetDatabaseValue(@expiry_days)));
			parameters.Add(new SqlParameter("@points_per_dollar", EntityBase.GetDatabaseValue(@points_per_dollar)));
			parameters.Add(new SqlParameter("@points_threshold", EntityBase.GetDatabaseValue(@points_threshold)));
			parameters.Add(new SqlParameter("@voucher_amount", EntityBase.GetDatabaseValue(@voucher_amount)));
			parameters.Add(new SqlParameter("@enable_vouchers", @enable_vouchers));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Deletes a RewardSetting from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="setting_id">setting_id</param>
		public static void DeleteRewardSetting(int @setting_id)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try 
				{
					helper.BeginTransaction();
					DeleteRewardSetting(@setting_id, helper);
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
		/// Deletes a RewardSetting from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="setting_id">setting_id</param>
		/// <param name="helper">helper</param>
		internal static void DeleteRewardSetting(int @setting_id, SqlHelper @helper)
		{
			string commandText = "RewardSettingDelete";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@setting_id", @setting_id));
		
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Creates a new RewardSetting object.
		/// </summary>
		/// <returns>The newly created RewardSetting object.</returns>
		public static RewardSetting CreateRewardSetting()
		{
			return InitializeNew<RewardSetting>();
		}
		
		/// <summary>
		/// Retrieve information for a RewardSetting by a RewardSetting's unique identifier.
		/// </summary>
		/// <param name="setting_id">setting_id</param>
		/// <returns>RewardSetting</returns>
		public static RewardSetting GetRewardSetting(int setting_id)
		{
			string commandText = "RewardSettingGet";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@setting_id", setting_id));
			
			return GetOne<RewardSetting>(commandText, parameters);
		}
		
		/// <summary>
		/// Gets a collection RewardSetting objects.
		/// </summary>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		public static EntityList<RewardSetting> GetRewardSettings()
		{
			string commandText = "RewardSettingGetAll";
		
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			return GetList<RewardSetting>(commandText, parameters);
		}
		
		/// <summary>
        /// Gets a collection RewardSetting objects.
        /// </summary>
		/// <param name="orderBy">order by</param>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">PageSize</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of RewardSetting objects.</returns>
        protected static EntityList<RewardSetting> GetRewardSettings(string orderBy, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<RewardSetting>(SelectFieldList, "FROM [dbo].[RewardSettings]", new List<SqlParameter>(),orderBy,  startRowIndex, pageSize, out totalRows);
        }

		/// <summary>
        /// Gets a collection RewardSetting objects.
        /// </summary>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of RewardSetting objects.</returns>
        public static EntityList<RewardSetting> GetRewardSettings(int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<RewardSetting>(SelectFieldList, "FROM [dbo].[RewardSettings]", new List<SqlParameter>(), null,  startRowIndex, pageSize, out totalRows);
        }
		
		/// <summary>
		/// Gets a collection RewardSetting objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <param name="orderBy">the order by clause. Should start with "order by"</param>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettings(string where, SqlParameter parameter, string orderBy)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetRewardSettings(string.Empty, where, parameters, orderBy);
		}
		
		/// <summary>
		/// Gets a collection RewardSetting objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettings(string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetRewardSettings(string.Empty, where, parameters, RewardSetting.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection RewardSetting objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettings(string prefix, string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetRewardSettings(prefix, where, parameters, RewardSetting.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection RewardSetting objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettings(string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetRewardSettings(string.Empty, where, parameters, RewardSetting.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection RewardSetting objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettings(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetRewardSettings(prefix, where, parameters, RewardSetting.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection RewardSetting objects by custom where clause and order by.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <param name="orderBy">the order by clause. Shoudl start with "order by"</param>
		/// <returns>The retrieved collection of RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettings(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters, string orderBy)
		{
			string commandText = @"SELECT " + prefix + "" + RewardSetting.SelectFieldList + "FROM [dbo].[RewardSettings] " + where + " " + orderBy;			
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				using (IDataReader reader = helper.ExecuteDataReader(commandText, CommandType.Text, parameters))
				{
					return EntityBase.InitializeList<RewardSetting>(reader);
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
        protected static EntityList<RewardSetting> GetRewardSettings(string orderBy, string where, System.Collections.Generic.List<SqlParameter> parameters, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<RewardSetting>(SelectFieldList, "FROM [dbo].[RewardSettings] " + where, parameters, orderBy, startRowIndex, pageSize, out totalRows);			
		}
		
		/// <summary>
		/// Gets a collection of RewardSetting objects by a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>A collection RewardSetting objects.</returns>
		public static EntityList<RewardSetting> GetRewardSettingsBycompany_(Company @company_) 
		{
			string commandText = "RewardSettingGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<RewardSetting>(@company_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of RewardSetting objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettingsBycompany_(Company @company_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[RewardSettings] 
WHERE 
	[RewardSettings].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<RewardSetting>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of RewardSetting objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection RewardSetting objects.</returns>
		protected static EntityList<RewardSetting> GetRewardSettingsBycompany_(int @company_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[RewardSettings] 
WHERE 
	[RewardSettings].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<RewardSetting>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of RewardSetting objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection RewardSetting objects.</returns>
		public static EntityList<RewardSetting> GetRewardSettingsBycompany_(Company @company_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[RewardSettings] 
WHERE 
	[RewardSettings].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<RewardSetting>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of RewardSetting objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection RewardSetting objects.</returns>
		public static EntityList<RewardSetting> GetRewardSettingsBycompany_(int @company_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[RewardSettings] 
WHERE 
	[RewardSettings].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<RewardSetting>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of RewardSetting objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <returns>A collection RewardSetting objects.</returns>
		public static EntityList<RewardSetting> GetRewardSettingsBycompany_(int @company_id) 
		{
			string commandText = "RewardSettingGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<RewardSetting>(commandText, parameters);
		}

		/// <summary>
		/// Create a new RewardSetting object from a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>The newly created RewardSetting object.</returns>
		public static RewardSetting CreateRewardSettingBycompany_(Company @company_)
		{
			RewardSetting rewardSetting = InitializeNew<RewardSetting>();
			
			rewardSetting.company_id = @company_.company_id;
			
			rewardSetting.company_ = @company_;
			
			return rewardSetting;
		}
		
		/// <summary>
		/// Deletes RewardSetting objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
		public static void DeleteRewardSettingsBycompany_(Company company) 
		{
			string commandText = "RewardSettingDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company.company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes RewardSetting objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		public static void DeleteRewardSettingsBycompany_(int company_id) 
		{
			string commandText = "RewardSettingDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		#endregion
		
		#region Subclasses
		public static partial class RewardSettingProperties
		{
			public const string setting_id = "setting_id";
			public const string company_id = "company_id";
			public const string expiry_days = "expiry_days";
			public const string points_per_dollar = "points_per_dollar";
			public const string points_threshold = "points_threshold";
			public const string voucher_amount = "voucher_amount";
			public const string enable_vouchers = "enable_vouchers";
		}
		#endregion
	}
}
