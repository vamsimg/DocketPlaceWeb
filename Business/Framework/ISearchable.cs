/*------------------------------------------------------------------------
<generated>
     This code was generated by The NuSoft Framework v3.0
     Generated at 09/09/2012 4:52:05 PM.

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
    /// Interface.  Used by base classes to read entity information
    /// </summary>
    public interface ISearchable
    {
        EntityBaseReadOnly GetEntityById(string uniqueIdentifier);
    }
}
