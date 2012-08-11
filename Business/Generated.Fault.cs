/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 11/08/2012 4:50:58 PM.

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
	/// This object represents the properties and methods of a Fault.
	/// </summary>
	[Serializable()]
	[DebuggerDisplay("fault_id: {fault_id}")]
	public partial class Fault
	{
		#region Public Properties
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _fault_id = int.MinValue;
		/// <summary>
		/// fault_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(true, true, false)]
		public int fault_id
		{
			[DebuggerStepThrough()]
			get { return this._fault_id; }
			protected set 
			{
				if (this._fault_id != value) 
				{
					this._fault_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("fault_id");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _faultcode_id = int.MinValue;
		/// <summary>
		/// faultcode_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public int faultcode_id
		{
			[DebuggerStepThrough()]
			get { return this._faultcode_id; }
			set 
			{
				if (this._faultcode_id != value) 
				{
					this._faultcode_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("faultcode_id");
					this._faultcode_ = null;
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _logentry_id = int.MinValue;
		/// <summary>
		/// logentry_id
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public int logentry_id
		{
			[DebuggerStepThrough()]
			get { return this._logentry_id; }
			set 
			{
				if (this._logentry_id != value) 
				{
					this._logentry_id = value;
					this.IsDirty = true;	
					OnPropertyChanged("logentry_id");
					this._logentry_ = null;
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _requested_page = String.Empty;
		/// <summary>
		/// requested_page
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, true)]
		public string requested_page
		{
			[DebuggerStepThrough()]
			get { return this._requested_page; }
			set 
			{
				if (this._requested_page != value) 
				{
					this._requested_page = value;
					this.IsDirty = true;	
					OnPropertyChanged("requested_page");
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _description = String.Empty;
		/// <summary>
		/// description
		/// </summary>
		[DatabaseColumn()]
		[TypeConverter(typeof(MinToEmptyTypeConverter))]
		[DataObjectField(false, false, false)]
		public string description
		{
			[DebuggerStepThrough()]
			get { return this._description; }
			set 
			{
				if (this._description != value) 
				{
					this._description = value;
					this.IsDirty = true;	
					OnPropertyChanged("description");
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
		private FaultCode _faultcode_ = null;
		/// <summary>
		/// The parent FaultCode object
		/// </summary>
		public FaultCode faultcode_
		{
			get 
			{
				if (_faultcode_ == null) 
				{
					_faultcode_ = GetParentEntity(FaultCode.GetFaultCode(this.faultcode_id)) as FaultCode;
				}
				return _faultcode_;
			}
			set
			{
				if(_faultcode_ != value) 
				{
					_faultcode_ = value;
					
					if (value != null) 
					{
						this.faultcode_id = value.faultcode_id;
					}
					else 
					{
						this.faultcode_id = int.MinValue;
					}
				}
			}
		}
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LogEntry _logentry_ = null;
		/// <summary>
		/// The parent LogEntry object
		/// </summary>
		public LogEntry logentry_
		{
			get 
			{
				if (_logentry_ == null) 
				{
					_logentry_ = GetParentEntity(LogEntry.GetLogEntry(this.logentry_id)) as LogEntry;
				}
				return _logentry_;
			}
			set
			{
				if(_logentry_ != value) 
				{
					_logentry_ = value;
					
					if (value != null) 
					{
						this.logentry_id = value.logentry_id;
					}
					else 
					{
						this.logentry_id = int.MinValue;
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
				return typeof(Fault).Name + "Insert";
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
				return typeof(Fault).Name + "Update";
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
				return typeof(Fault).Name + "Delete";
			}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// The default protected constructor
		/// </summary>
		protected Fault() { }
		#endregion
		
		#region Public Methods
		

        /// <summary>
        /// Refreshes the entity with data from the data source. Child entity objects and entity list objects will be preserved (ie. they will not be replaced with new objects so that references to them are retained, such as bound data controls).
        /// </summary>
        public override void Refresh()
		{
			this.Replace(GetFault(this.fault_id));
		}

		#endregion
		
		#region Non-Public Methods
		/// <summary>
		/// This is called before an entity is saved to ensure that any parent entities keys are set properly
		/// </summary>
		protected override void EnsureParentProperties()
		{
			if (_faultcode_ != null)
			{	
				this.faultcode_id = this.faultcode_.faultcode_id;
			}
			
			if (_logentry_ != null)
			{	
				this.logentry_id = this.logentry_.logentry_id;
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
	[Faults].[fault_id],
	[Faults].[faultcode_id],
	[Faults].[logentry_id],
	[Faults].[requested_page],
	[Faults].[description],
	[Faults].[creation_datetime]
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
                return "Faults";
            }
        }

		#endregion
		
		#region Static Methods
		/// <summary>
		/// Insert a Fault into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="faultcode_id">faultcode_id</param>
		/// <param name="logentry_id">logentry_id</param>
		/// <param name="requested_page">requested_page</param>
		/// <param name="description">description</param>
		/// <param name="creation_datetime">creation_datetime</param>
		public static void InsertFault(int @faultcode_id, int @logentry_id, string @requested_page, string @description, DateTime @creation_datetime)
		{
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.BeginTransaction();
            		InsertFault(@faultcode_id, @logentry_id, @requested_page, @description, @creation_datetime, helper);
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
		/// Insert a Fault into the data store based on the primitive properties. This can be used as the 
		/// insert method for an ObjectDataSource.
		/// </summary>
		/// <param name="faultcode_id">faultcode_id</param>
		/// <param name="logentry_id">logentry_id</param>
		/// <param name="requested_page">requested_page</param>
		/// <param name="description">description</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="helper">helper</param>
		internal static void InsertFault(int @faultcode_id, int @logentry_id, string @requested_page, string @description, DateTime @creation_datetime, SqlHelper @helper)
		{
			string commandText = "FaultInsert";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", EntityBase.GetDatabaseValue(@faultcode_id)));
			parameters.Add(new SqlParameter("@logentry_id", EntityBase.GetDatabaseValue(@logentry_id)));
			parameters.Add(new SqlParameter("@requested_page", EntityBase.GetDatabaseValue(@requested_page)));
			parameters.Add(new SqlParameter("@description", EntityBase.GetDatabaseValue(@description)));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Updates a Fault into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="fault_id">fault_id</param>
		/// <param name="faultcode_id">faultcode_id</param>
		/// <param name="logentry_id">logentry_id</param>
		/// <param name="requested_page">requested_page</param>
		/// <param name="description">description</param>
		/// <param name="creation_datetime">creation_datetime</param>
		public static void UpdateFault(int @fault_id, int @faultcode_id, int @logentry_id, string @requested_page, string @description, DateTime @creation_datetime)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try
				{
					helper.BeginTransaction();
					UpdateFault(@fault_id, @faultcode_id, @logentry_id, @requested_page, @description, @creation_datetime, helper);
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
		/// Updates a Fault into the data store based on the primitive properties. This can be used as the 
		/// update method for an ObjectDataSource.
		/// </summary>
		/// <param name="fault_id">fault_id</param>
		/// <param name="faultcode_id">faultcode_id</param>
		/// <param name="logentry_id">logentry_id</param>
		/// <param name="requested_page">requested_page</param>
		/// <param name="description">description</param>
		/// <param name="creation_datetime">creation_datetime</param>
		/// <param name="helper">helper</param>
		internal static void UpdateFault(int @fault_id, int @faultcode_id, int @logentry_id, string @requested_page, string @description, DateTime @creation_datetime, SqlHelper @helper)
		{
			string commandText = "FaultUpdate";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@fault_id", EntityBase.GetDatabaseValue(@fault_id)));
			parameters.Add(new SqlParameter("@faultcode_id", EntityBase.GetDatabaseValue(@faultcode_id)));
			parameters.Add(new SqlParameter("@logentry_id", EntityBase.GetDatabaseValue(@logentry_id)));
			parameters.Add(new SqlParameter("@requested_page", EntityBase.GetDatabaseValue(@requested_page)));
			parameters.Add(new SqlParameter("@description", EntityBase.GetDatabaseValue(@description)));
			parameters.Add(new SqlParameter("@creation_datetime", EntityBase.GetDatabaseValue(@creation_datetime)));
			
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Deletes a Fault from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="fault_id">fault_id</param>
		public static void DeleteFault(int @fault_id)
		{
			using (SqlHelper helper = new SqlHelper()) 
			{
				try 
				{
					helper.BeginTransaction();
					DeleteFault(@fault_id, helper);
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
		/// Deletes a Fault from the data store based on the primitive primary keys. This can be used as the 
		/// delete method for an ObjectDataSource.
		/// </summary>
		/// <param name="fault_id">fault_id</param>
		/// <param name="helper">helper</param>
		internal static void DeleteFault(int @fault_id, SqlHelper @helper)
		{
			string commandText = "FaultDelete";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@fault_id", @fault_id));
		
			@helper.Execute(commandText, CommandType.StoredProcedure, parameters);
		}
		
		/// <summary>
		/// Creates a new Fault object.
		/// </summary>
		/// <returns>The newly created Fault object.</returns>
		public static Fault CreateFault()
		{
			return InitializeNew<Fault>();
		}
		
		/// <summary>
		/// Retrieve information for a Fault by a Fault's unique identifier.
		/// </summary>
		/// <param name="fault_id">fault_id</param>
		/// <returns>Fault</returns>
		public static Fault GetFault(int fault_id)
		{
			string commandText = "FaultGet";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@fault_id", fault_id));
			
			return GetOne<Fault>(commandText, parameters);
		}
		
		/// <summary>
		/// Gets a collection Fault objects.
		/// </summary>
		/// <returns>The retrieved collection of Fault objects.</returns>
		public static EntityList<Fault> GetFaults()
		{
			string commandText = "FaultGetAll";
		
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			return GetList<Fault>(commandText, parameters);
		}
		
		/// <summary>
        /// Gets a collection Fault objects.
        /// </summary>
		/// <param name="orderBy">order by</param>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">PageSize</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Fault objects.</returns>
        protected static EntityList<Fault> GetFaults(string orderBy, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Fault>(SelectFieldList, "FROM [dbo].[Faults]", new List<SqlParameter>(),orderBy,  startRowIndex, pageSize, out totalRows);
        }

		/// <summary>
        /// Gets a collection Fault objects.
        /// </summary>
		/// <param name="startRowIndex">Start Row Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="totalRows">Total rows</param>
        /// <returns>The retrieved collection of Fault objects.</returns>
        public static EntityList<Fault> GetFaults(int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Fault>(SelectFieldList, "FROM [dbo].[Faults]", new List<SqlParameter>(), null,  startRowIndex, pageSize, out totalRows);
        }
		
		/// <summary>
		/// Gets a collection Fault objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <param name="orderBy">the order by clause. Should start with "order by"</param>
		/// <returns>The retrieved collection of Fault objects.</returns>
		protected static EntityList<Fault> GetFaults(string where, SqlParameter parameter, string orderBy)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetFaults(string.Empty, where, parameters, orderBy);
		}
		
		/// <summary>
		/// Gets a collection Fault objects by custom where clause.
		/// </summary>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of Fault objects.</returns>
		protected static EntityList<Fault> GetFaults(string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetFaults(string.Empty, where, parameters, Fault.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Fault objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameter">The parameter that is in the where clause</param>
		/// <returns>The retrieved collection of Fault objects.</returns>
		protected static EntityList<Fault> GetFaults(string prefix, string where, SqlParameter parameter)
		{
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			
			parameters.Add(parameter);
						
			return GetFaults(prefix, where, parameters, Fault.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Fault objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of Fault objects.</returns>
		protected static EntityList<Fault> GetFaults(string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetFaults(string.Empty, where, parameters, Fault.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Fault objects by custom where clause.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <returns>The retrieved collection of Fault objects.</returns>
		protected static EntityList<Fault> GetFaults(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters)
		{
			return GetFaults(prefix, where, parameters, Fault.DefaultSortOrder);
		}
		
		/// <summary>
		/// Gets a collection Fault objects by custom where clause and order by.
		/// </summary>
		/// <param name="prefix">The prefix clause allows you to inject a distinct or top clause.</param>
		/// <param name="where">The where clause to use for the query. Should be parameterized and start with "where"</param>
		/// <param name="parameters">The parameters that are listed in the where clause</param>
		/// <param name="orderBy">the order by clause. Shoudl start with "order by"</param>
		/// <returns>The retrieved collection of Fault objects.</returns>
		protected static EntityList<Fault> GetFaults(string prefix, string where, System.Collections.Generic.List<SqlParameter> parameters, string orderBy)
		{
			string commandText = @"SELECT " + prefix + "" + Fault.SelectFieldList + "FROM [dbo].[Faults] " + where + " " + orderBy;			
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				using (IDataReader reader = helper.ExecuteDataReader(commandText, CommandType.Text, parameters))
				{
					return EntityBase.InitializeList<Fault>(reader);
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
        protected static EntityList<Fault> GetFaults(string orderBy, string where, System.Collections.Generic.List<SqlParameter> parameters, int startRowIndex, int pageSize, out long totalRows)
        {
            return GetList<Fault>(SelectFieldList, "FROM [dbo].[Faults] " + where, parameters, orderBy, startRowIndex, pageSize, out totalRows);			
		}
		
		/// <summary>
		/// Gets a collection of Fault objects by a FaultCode object.
		/// </summary>
		/// <param name="faultcode_">faultcode_</param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsByfaultcode_(FaultCode @faultcode_) 
		{
			string commandText = "FaultGetByFaultCode";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", @faultcode_.faultcode_id));
			
			return GetList<Fault>(@faultcode_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of Fault objects by a FaultCode object.
		/// </summary>
		/// <param name="faultCode">faultCode</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		protected static EntityList<Fault> GetFaultsByfaultcode_(FaultCode @faultcode_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[faultcode_id] = @faultcode_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@faultcode_id", @faultcode_.faultcode_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Fault objects by a FaultCode object.
		/// </summary>
		/// <param name="faultcode_id">faultcode_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		protected static EntityList<Fault> GetFaultsByfaultcode_(int @faultcode_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[faultcode_id] = @faultcode_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", @faultcode_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of Fault objects by a FaultCode object.
		/// </summary>
		/// <param name="faultCode">faultCode</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsByfaultcode_(FaultCode @faultcode_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[faultcode_id] = @faultcode_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@faultcode_id", @faultcode_.faultcode_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Fault objects by a FaultCode object.
		/// </summary>
		/// <param name="faultcode_id">faultcode_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsByfaultcode_(int @faultcode_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[faultcode_id] = @faultcode_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", @faultcode_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of Fault objects by a FaultCode unique identifier.
		/// </summary>
		/// <param name="faultcode_id">faultcode_id</param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsByfaultcode_(int @faultcode_id) 
		{
			string commandText = "FaultGetByFaultCode";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", @faultcode_id));
			
			return GetList<Fault>(commandText, parameters);
		}

		/// <summary>
		/// Create a new Fault object from a FaultCode object.
		/// </summary>
		/// <param name="faultcode_">faultcode_</param>
		/// <returns>The newly created Fault object.</returns>
		public static Fault CreateFaultByfaultcode_(FaultCode @faultcode_)
		{
			Fault fault = InitializeNew<Fault>();
			
			fault.faultcode_id = @faultcode_.faultcode_id;
			
			fault.faultcode_ = @faultcode_;
			
			return fault;
		}
		
		/// <summary>
		/// Deletes Fault objects by a FaultCode object.
		/// </summary>
		/// <param name="faultCode">faultCode</param>
		public static void DeleteFaultsByfaultcode_(FaultCode faultCode) 
		{
			string commandText = "FaultDeleteByFaultCode";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", faultCode.faultcode_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes Fault objects by a FaultCode unique identifier.
		/// </summary>
		/// <param name="faultcode_id">faultcode_id</param>
		public static void DeleteFaultsByfaultcode_(int faultcode_id) 
		{
			string commandText = "FaultDeleteByFaultCode";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@faultcode_id", faultcode_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		/// <summary>
		/// Gets a collection of Fault objects by a LogEntry object.
		/// </summary>
		/// <param name="logentry_">logentry_</param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsBylogentry_(LogEntry @logentry_) 
		{
			string commandText = "FaultGetByLogEntry";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@logentry_id", @logentry_.logentry_id));
			
			return GetList<Fault>(@logentry_, commandText, parameters);
		}
  

		/// <summary>
		/// Gets a collection of Fault objects by a LogEntry object.
		/// </summary>
		/// <param name="logEntry">logEntry</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		protected static EntityList<Fault> GetFaultsBylogentry_(LogEntry @logentry_, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[logentry_id] = @logentry_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@logentry_id", @logentry_.logentry_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Fault objects by a LogEntry object.
		/// </summary>
		/// <param name="logentry_id">logentry_id</param>
        /// <param name="orderBy"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		protected static EntityList<Fault> GetFaultsBylogentry_(int @logentry_id, string orderBy, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[logentry_id] = @logentry_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@logentry_id", @logentry_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, orderBy, startRowIndex, pageSize, out totalRows);
		}		
		
		
		/// <summary>
		/// Gets a collection of Fault objects by a LogEntry object.
		/// </summary>
		/// <param name="logEntry">logEntry</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsBylogentry_(LogEntry @logentry_, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[logentry_id] = @logentry_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
				
			parameters.Add(new SqlParameter("@logentry_id", @logentry_.logentry_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}

	    /// <summary>
		/// Gets a collection of Fault objects by a LogEntry object.
		/// </summary>
		/// <param name="logentry_id">logentry_id</param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRows"></param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsBylogentry_(int @logentry_id, long startRowIndex, int pageSize, out long totalRows) 
		{
			string commandText = @"
FROM 
	[dbo].[Faults] 
WHERE 
	[Faults].[logentry_id] = @logentry_id ";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@logentry_id", @logentry_id));
			
			return GetList<Fault>(SelectFieldList, commandText, parameters, null, startRowIndex, pageSize, out totalRows);
		}
		
	
		/// <summary>
		/// Gets a collection of Fault objects by a LogEntry unique identifier.
		/// </summary>
		/// <param name="logentry_id">logentry_id</param>
		/// <returns>A collection Fault objects.</returns>
		public static EntityList<Fault> GetFaultsBylogentry_(int @logentry_id) 
		{
			string commandText = "FaultGetByLogEntry";
			
			List<SqlParameter> parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter("@logentry_id", @logentry_id));
			
			return GetList<Fault>(commandText, parameters);
		}

		/// <summary>
		/// Create a new Fault object from a LogEntry object.
		/// </summary>
		/// <param name="logentry_">logentry_</param>
		/// <returns>The newly created Fault object.</returns>
		public static Fault CreateFaultBylogentry_(LogEntry @logentry_)
		{
			Fault fault = InitializeNew<Fault>();
			
			fault.logentry_id = @logentry_.logentry_id;
			
			fault.logentry_ = @logentry_;
			
			return fault;
		}
		
		/// <summary>
		/// Deletes Fault objects by a LogEntry object.
		/// </summary>
		/// <param name="logEntry">logEntry</param>
		public static void DeleteFaultsBylogentry_(LogEntry logEntry) 
		{
			string commandText = "FaultDeleteByLogEntry";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@logentry_id", logEntry.logentry_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		
		/// <summary>
		/// Deletes Fault objects by a LogEntry unique identifier.
		/// </summary>
		/// <param name="logentry_id">logentry_id</param>
		public static void DeleteFaultsBylogentry_(int logentry_id) 
		{
			string commandText = "FaultDeleteByLogEntry";
			
			System.Collections.Generic.List<SqlParameter> parameters = new System.Collections.Generic.List<SqlParameter>();
			parameters.Add(new SqlParameter("@logentry_id", logentry_id));
			
			using (SqlHelper helper = new SqlHelper()) 
			{
				helper.Execute(commandText, CommandType.StoredProcedure, parameters);
			}
		}
		#endregion
		
		#region Subclasses
		public static partial class FaultProperties
		{
			public const string fault_id = "fault_id";
			public const string faultcode_id = "faultcode_id";
			public const string logentry_id = "logentry_id";
			public const string requested_page = "requested_page";
			public const string description = "description";
			public const string creation_datetime = "creation_datetime";
		}
		#endregion
	}
}
