/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 05/09/2012 12:37:26 AM.

     The NuSoft Framework is an open source project developed
     by NuSoft Solutions (http://www.nusoftsolutions.com).
     The latest version of the framework templates and detailed license
     is available at http://www.codeplex.com/NuSoftFramework.

     This file will be overwritten when regenerating your code.
</generated>
------------------------------------------------------------------------*/

using System;

namespace DocketPlace.Business.Framework
{
	/// <summary>
	/// A custom EntityEventArgs class that has a reference to the Entity firing the event
	/// </summary>
    public class EntityEventArgs : System.EventArgs
    {
        private EntityBaseReadOnly _entity;
		/// <summary>
		/// The entity firing the event
		/// </summary>
        public EntityBaseReadOnly Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }
		
		private SqlHelper _helper;
		/// <summary>
		///	The current Sql helper. This allows data access in an event to participate 
		/// in the same transaction as the object firing the event.
		/// </summary>
		public SqlHelper Helper
		{
			get { return _helper; }
			set { _helper = value; }
		}
		
		/// <summary>
		/// Public constructor taking the entity and a helper
		/// </summary>
        public EntityEventArgs(EntityBaseReadOnly entity, SqlHelper helper)
        {
            this._entity = entity;
			this._helper = helper;
        }

		/// <summary>
		/// Public constructor taking the entity
		/// </summary>
        public EntityEventArgs(EntityBaseReadOnly entity)
        {
            this._entity = entity;
        }
    }
}
