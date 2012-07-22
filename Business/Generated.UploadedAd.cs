/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 21/07/2012 9:33:00 PM.

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
	/// This object represents the properties and methods of a UploadedAd.
	/// </summary>
	[Serializable()]
	[DebuggerDisplay("uploadedad_id: {uploadedad_id}")]
	public partial class UploadedAd
	{
		#region Public Properties
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _uploadedad_id = int.MinValue;
		/// <summary>
		/// uploadedad_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(true, true, false)]
		public int uploadedad_id
		{
			[DebuggerStepThrough()]
			get { return this._uploadedad_id; }
			protected set 
			{
				if (this._uploadedad_id != value) 
				{
					this._uploadedad_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("uploadedad_id");
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
		private string _title = String.Empty;
		/// <summary>
		/// title
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public string title
		{
			[DebuggerStepThrough()]
			get { return this._title; }
			set 
			{
				if (this._title != value) 
				{
					this._title = value;
					this.IsDirty = true;	
					OnPropertyChanged("title");
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
		private string _footer = String.Empty;
		/// <summary>
		/// footer
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public string footer
		{
			[DebuggerStepThrough()]
			get { return this._footer; }
			set 
			{
				if (this._footer != value) 
				{
					this._footer = value;
					this.IsDirty = true;	
					OnPropertyChanged("footer");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _global_barcode = String.Empty;
		/// <summary>
		/// global_barcode
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public string global_barcode
		{
			[DebuggerStepThrough()]
			get { return this._global_barcode; }
			set 
			{
				if (this._global_barcode != value) 
				{
					this._global_barcode = value;
					this.IsDirty = true;	
					OnPropertyChanged("global_barcode");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _is_active = false;
		/// <summary>
		/// is_active
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public bool is_active
		{
			[DebuggerStepThrough()]
			get { return this._is_active; }
			set 
			{
				if (this._is_active != value) 
				{
					this._is_active = value;
					this.IsDirty = true;	
					OnPropertyChanged("is_active");
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
		private string _data = String.Empty;
		/// <summary>
		/// data
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public string data
		{
			[DebuggerStepThrough()]
			get { return this._data; }
			set 
			{
				if (this._data != value) 
				{
					this._data = value;
					this.IsDirty = true;	
					OnPropertyChanged("data");
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
		private EntityList<LogEntry> _logEntriesByuploadedad_ = null;
		/// <summary>
		/// A collection of LogEntry children objects
		/// </summary>
		public EntityList<LogEntry> LogEntriesByuploadedad_
		{
			get 
			{
				if (_logEntriesByuploadedad_ == null) {
					_logEntriesByuploadedad_ = DocketPlace.Business.LogEntry.GetLogEntriesByuploadedad_(this);
				}
				return _logEntriesByuploadedad_;
			}
		}	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntityList<PlacedAd> _placedAdsByuploadedad_ = null;
		/// <summary>
		/// A collection of PlacedAd children objects
		/// </summary>
		public EntityList<PlacedAd> PlacedAdsByuploadedad_
		{
			get 
			{
				if (_placedAdsByuploadedad_ == null) {
					_placedAdsByuploadedad_ = DocketPlace.Business.PlacedAd.GetPlacedAdsByuploadedad_(this);
				}
				return _placedAdsByuploadedad_;
			}
		}	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntityList<RequestedAd> _requestedAdsByuploadedad_ = null;
		/// <summary>
		/// A collection of RequestedAd children objects
		/// </summary>
		public EntityList<RequestedAd> RequestedAdsByuploadedad_
		{
			get 
			{
				if (_requestedAdsByuploadedad_ == null) {
					_requestedAdsByuploadedad_ = DocketPlace.Business.RequestedAd.GetRequestedAdsByuploadedad_(this);
				}
				return _requestedAdsByuploadedad_;
			}
		}	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntityList<Store> _storesBydefault_uploadedad_ = null;
		/// <summary>
		/// A collection of Store children objects
		/// </summary>
		public EntityList<Store> StoresBydefault_uploadedad_
		{
			get 
			{
				if (_storesBydefault_uploadedad_ == null) {
					_storesBydefault_uploadedad_ = DocketPlace.Business.Store.GetStoresBydefault_uploadedad_(this);
				}
				return _storesBydefault_uploadedad_;
			}
		}	
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EntityList<Trigger> _triggersByuploadedad_ = null;
		/// <summary>
		/// A collection of Trigger children objects
		/// </summary>
		public EntityList<Trigger> TriggersByuploadedad_
		{
			get 
			{
				if (_triggersByuploadedad_ == null) {
					_triggersByuploadedad_ = DocketPlace.Business.Trigger.GetTriggersByuploadedad_(this);
				}
				return _triggersByuploadedad_;
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
				return typeof(UploadedAd).Name + "Insert";
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
				return typeof(UploadedAd).Name + "Update";
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
				return typeof(UploadedAd).Name + "Delete";
			}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// The default protected constructor
		/// </summary>
		protected UploadedAd() { }
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Creates a LogEntry for this UploadedAd object
		/// </summary>
		public LogEntry CreateLogEntry()
		{
			return DocketPlace.Business.LogEntry.CreateLogEntryByuploadedad_(this);
		}
		/// <summary>
		/// Creates a PlacedAd for this UploadedAd object
		/// </summary>
		public PlacedAd CreatePlacedAd()
		{
			return DocketPlace.Business.PlacedAd.CreatePlacedAdByuploadedad_(this);
		}
		/// <summary>
		/// Creates a RequestedAd for this UploadedAd object
		/// </summary>
		public RequestedAd CreateRequestedAd()
		{
			return DocketPlace.Business.RequestedAd.CreateRequestedAdByuploadedad_(this);
		}
		/// <summary>
		/// Creates a Store for this UploadedAd object
		/// </summary>
		public Store CreateStore()
		{
			return DocketPlace.Business.Store.CreateStoreBydefault_uploadedad_(this);
		}
		/// <summary>
		/// Creates a Trigger for this UploadedAd object
		/// </summary>
		public Trigger CreateTrigger()
		{
			return DocketPlace.Business.Trigger.CreateTriggerByuploadedad_(this);
		}
		

        /// <summary>
        /// Refreshes the entity with data from the data source. Child entity objects and entity list objects will be preserved (ie. they will not be replaced with new objects so that references to them are retained, such as bound data controls).
        /// </summary>
        public override void Refresh()
		{
			this.Replace(GetUploadedAd(this.uploadedad_id));
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
	[UploadedAds].[uploadedad_id],
	[UploadedAds].[company_id],
	[UploadedAds].[title],
	[UploadedAds].[notes],
	[UploadedAds].[footer],
	[UploadedAds].[global_barcode],
	[UploadedAds].[is_active],
	[UploadedAds].[creation_datetime],
	[UploadedAds].[data]
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
                return "UploadedAds";
            }
        }

		#endregion
		
		#region Static Methods
		/// <summary>
		/// Insert a UploadedAd into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <param name="title">title</param>
		/// <param name="notes">notes</param>
		/// <param name="footer">footer</param>
		/// <param name="global_barcode">global_barcode</param>
		/// <param name="is_active">is_active</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="data">data</param>
		public static void InsertUploadedAd(int @company_id, string @title, string @notes, string @footer, string @global_barcode, bool @is_active, DateTime @creation_datetime, string @data)
		{
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.BeginTransaction();
            		InsertUploadedAd(@company_id, @title, @notes, @footer, @global_barcode, @is_active, @creation_datetime, @data, helper);
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
		/// Insert a UploadedAd into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <param name="title">title</param>
		/// <param name="notes">notes</param>
		/// <param name="footer">footer</param>
		/// <param name="global_barcode">global_barcode</param>
		/// <param name="is_active">is_active</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="data">data</param>
		/// <param name="helper">helper</param>
		internal static void InsertUploadedAd(int @company_id, string @title, string @notes, string @footer, string @global_barcode, bool @is_active, DateTime @creation_datetime, string @data, SqlHelper @helper)
		{
			string commandText = "UploadedAdInsert";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@title", EntityBase.GetDatabaseValue(@title)));
			parameters.Add(new SqlParameter("@notes", EntityBase.GetDatabaseValue(@notes)));
			parameters.Add(new SqlParameter("@footer", EntityBase.GetDatabaseValue(@footer)));
			parameters.Add(new SqlParameter("@global_barcode", EntityBase.GetDatabaseValue(@global_barcode)));
			parameters.Add(new SqlParameter("@is_active", @is_active));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			parameters.Add(new SqlParameter("@data", EntityBase.GetDatabaseValue(@data)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Updates a UploadedAd into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="uploadedad_id">uploadedad_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="title">title</param>
		/// <param name="notes">notes</param>
		/// <param name="footer">footer</param>
		/// <param name="global_barcode">global_barcode</param>
		/// <param name="is_active">is_active</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="data">data</param>
		public static void UpdateUploadedAd(int @uploadedad_id, int @company_id, string @title, string @notes, string @footer, string @global_barcode, bool @is_active, DateTime @creation_datetime, string @data)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try
				{
					helper.BeginTransaction();
					UpdateUploadedAd(@uploadedad_id, @company_id, @title, @notes, @footer, @global_barcode, @is_active, @creation_datetime, @data, helper);
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
		/// Updates a UploadedAd into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="uploadedad_id">uploadedad_id</param>
		/// <param name="company_id">company_id</param>
		/// <param name="title">title</param>
		/// <param name="notes">notes</param>
		/// <param name="footer">footer</param>
		/// <param name="global_barcode">global_barcode</param>
		/// <param name="is_active">is_active</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="data">data</param>
		/// <param name="helper">helper</param>
		internal static void UpdateUploadedAd(int @uploadedad_id, int @company_id, string @title, string @notes, string @footer, string @global_barcode, bool @is_active, DateTime @creation_datetime, string @data, SqlHelper @helper)
		{
			string commandText = "UploadedAdUpdate";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@uploadedad_id", EntityBase.GetDatabaseValue(@uploadedad_id)));
			parameters.Add(new SqlParameter("@company_id", EntityBase.GetDatabaseValue(@company_id)));
			parameters.Add(new SqlParameter("@title", EntityBase.GetDatabaseValue(@title)));
			parameters.Add(new SqlParameter("@notes", EntityBase.GetDatabaseValue(@notes)));
			parameters.Add(new SqlParameter("@footer", EntityBase.GetDatabaseValue(@footer)));
			parameters.Add(new SqlParameter("@global_barcode", EntityBase.GetDatabaseValue(@global_barcode)));
			parameters.Add(new SqlParameter("@is_active", @is_active));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			parameters.Add(new SqlParameter("@data", EntityBase.GetDatabaseValue(@data)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Deletes a UploadedAd from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="uploadedad_id">uploadedad_id</param>
		public static void DeleteUploadedAd(int @uploadedad_id)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try 
				{
					helper.BeginTransaction();
					DeleteUploadedAd(@uploadedad_id, helper);
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
		/// Deletes a UploadedAd from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="uploadedad_id">uploadedad_id</param>
		/// <param name="helper">helper</param>
		internal static void DeleteUploadedAd(int @uploadedad_id, SqlHelper @helper)
		{
			string commandText = "UploadedAdDelete";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@uploadedad_id", @uploadedad_id));
		
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Creates a new UploadedAd object.
		/// </summary>
		/// <returns>The newly created UploadedAd object.</returns>
		public static UploadedAd CreateUploadedAd()
		{
			return InitializeNew<UploadedAd>();
		}
		
		/// <summary>
		/// Retrieve information for a UploadedAd by a UploadedAd's unique identifier.
		/// </summary>
		/// <param name="uploadedad_id">uploadedad_id</param>
		/// <returns>UploadedAd</returns>
		public static UploadedAd GetUploadedAd(int uploadedad_id)
		{
			string commandText = "UploadedAdGet";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@uploadedad_id", uploadedad_id));
			
			return GetOne<UploadedAd>(commandText, parameters);
		}
		
		/// <summary>
		/// Gets a collection UploadedAd objects.
		/// </summary>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		public static EntityList<UploadedAd> GetUploadedAds()
		{
			string commandText = "UploadedAdGetAll";
		
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			return GetList<UploadedAd>(commandText, parameters);
		}
		
		/// <summary>
        /// Gets a collection UploadedAd objects.
        /// </summary>
		/// <param name="orderBy">order by</param>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">PageSize</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of UploadedAd objects.</returns>
        protected static EntityList<UploadedAd> GetUploadedAds(string orderBy, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<UploadedAd>(SelectFieldList, "FROM [dbo].[UploadedAds]", new List<SqlParameter>(),orderBy,  startRowIndex, pageSize, out totalRows);
        }

		/// <summary>
        /// Gets a collection UploadedAd objects.
        /// </summary>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of UploadedAd objects.</returns>
        public static EntityList<UploadedAd> GetUploadedAds(int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<UploadedAd>(SelectFieldList, "FROM [dbo].[UploadedAds]", new List<SqlParameter>(), null,  startRowIndex, pageSize, out totalRows);
        }
		
		/// <summary>
		/// Gets a collection UploadedAd objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <param name="orderBy">the order by clause. Should start with "order by"</param>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAds(string where, SqlParameter parameter, string orderBy)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetUploadedAds(string.Empty, where, parameters, orderBy);
		}
		
		/// <summary>
		/// Gets a collection UploadedAd objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAds(string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetUploadedAds(string.Empty, where, parameters, UploadedAd.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection UploadedAd objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAds(string prefix, string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetUploadedAds(prefix, where, parameters, UploadedAd.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection UploadedAd objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAds(string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetUploadedAds(string.Empty, where, parameters, UploadedAd.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection UploadedAd objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAds(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetUploadedAds(prefix, where, parameters, UploadedAd.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection UploadedAd objects by custom where clause and order by.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <param name="orderBy">the order by clause. Shoudl start with "order by"</param>
		/// <returns>The retrieved collection of UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAds(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters, string orderBy)
		{
			string commandText = @"SELECT " + prefix + "" + UploadedAd.SelectFieldList + "FROM [dbo].[UploadedAds] " + where + " " + orderBy;			
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				using (IDataReader reader = helper.ExecuteDataReader(commandText, CommandType.Text, parameters))
				{
					return EntityBase.InitializeList<UploadedAd>(reader);
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
        protected static EntityList<UploadedAd> GetUploadedAds(string orderBy, string where, System.Collections.Generic.List<SqlParameter> parameters, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<UploadedAd>(SelectFieldList, "FROM [dbo].[UploadedAds] " + where, parameters, orderBy, startRowIndex, pageSize, out totalRows);			
		}
		
		/// <summary>
		/// Gets a collection of UploadedAd objects by a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>A collection UploadedAd objects.</returns>
		public static EntityList<UploadedAd> GetUploadedAdsBycompany_(Company @company_) 
		{
			string commandText = "UploadedAdGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<UploadedAd>(@company_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of UploadedAd objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAdsBycompany_(Company @company_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[UploadedAds] 
WHERE 
	[UploadedAds].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<UploadedAd>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of UploadedAd objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection UploadedAd objects.</returns>
		protected static EntityList<UploadedAd> GetUploadedAdsBycompany_(int @company_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[UploadedAds] 
WHERE 
	[UploadedAds].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<UploadedAd>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of UploadedAd objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection UploadedAd objects.</returns>
		public static EntityList<UploadedAd> GetUploadedAdsBycompany_(Company @company_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[UploadedAds] 
WHERE 
	[UploadedAds].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@company_id", @company_.company_id));
			
			return GetList<UploadedAd>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of UploadedAd objects by a Company object.
		/// </summary>
		/// <param name="company_id">company_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection UploadedAd objects.</returns>
		public static EntityList<UploadedAd> GetUploadedAdsBycompany_(int @company_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[UploadedAds] 
WHERE 
	[UploadedAds].[company_id] = @company_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<UploadedAd>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of UploadedAd objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		/// <returns>A collection UploadedAd objects.</returns>
		public static EntityList<UploadedAd> GetUploadedAdsBycompany_(int @company_id) 
		{
			string commandText = "UploadedAdGetByCompany";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", @company_id));
			
			return GetList<UploadedAd>(commandText, parameters);
		}

		/// <summary>
		/// Create a new UploadedAd object from a Company object.
		/// </summary>
		/// <param name="company_">company_</param>
		/// <returns>The newly created UploadedAd object.</returns>
		public static UploadedAd CreateUploadedAdBycompany_(Company @company_)
		{
			UploadedAd uploadedAd = InitializeNew<UploadedAd>();
			
			uploadedAd.company_id = @company_.company_id;
			
			uploadedAd.company_ = @company_;
			
			return uploadedAd;
		}
		
		/// <summary>
		/// Deletes UploadedAd objects by a Company object.
		/// </summary>
		/// <param name="company">company</param>
		public static void DeleteUploadedAdsBycompany_(Company company) 
		{
			string commandText = "UploadedAdDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company.company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes UploadedAd objects by a Company unique identifier.
		/// </summary>
		/// <param name="company_id">company_id</param>
		public static void DeleteUploadedAdsBycompany_(int company_id) 
		{
			string commandText = "UploadedAdDeleteByCompany";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@company_id", company_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		#endregion
		
		#region Subclasses
		public static partial class UploadedAdProperties
		{
			public const string uploadedad_id = "uploadedad_id";
			public const string company_id = "company_id";
			public const string title = "title";
			public const string notes = "notes";
			public const string footer = "footer";
			public const string global_barcode = "global_barcode";
			public const string is_active = "is_active";
			public const string creation_datetime = "creation_datetime";
			public const string data = "data";
		}
		#endregion
	}
}
