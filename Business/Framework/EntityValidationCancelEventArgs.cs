/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 11/08/2012 4:48:52 PM.

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
    public class EntityValidationCancelEventArgs : EntityCancelEventArgs
    {
		private string _validationGroup;
		/// <summary>
		/// The validation group that is being validated.
		/// </summary>
		public string ValidationGroup
		{
			get { return _validationGroup; }
		}
		
		private string _propertyName;
		/// <summary>
		/// The property that is being validated.
		/// </summary>
		public string PropertyName
		{
			get { return _propertyName; }
		}
		
		/// <summary>
		/// Public constructor taking the entity and a helper
		/// </summary>
        public EntityValidationCancelEventArgs(EntityBase entity, string propertyName, string validationGroup) : base(entity)
        {
			this._propertyName = propertyName;
			this._validationGroup = validationGroup;
        }
		
		/// <summary>
		/// Public constructor taking the entity and a helper
		/// </summary>
        public EntityValidationCancelEventArgs(EntityBase entity, SqlHelper helper, string propertyName, string validationGroup) : base(entity, helper)
        {
			this._propertyName = propertyName;
			this._validationGroup = validationGroup;
        }

		/// <summary>
		/// Public constructor taking the entity
		/// </summary>
        public EntityValidationCancelEventArgs(EntityBase entity) : base(entity) { }

		/// <summary>
		/// Public constructor taking the entity and the helper
		/// </summary>
        public EntityValidationCancelEventArgs(EntityBase entity, SqlHelper helper) : base(entity, helper) { }
    }
}
