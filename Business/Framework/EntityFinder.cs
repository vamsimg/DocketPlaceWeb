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
using System.Collections.Generic;
using System.Text;

namespace DocketPlace.Business.Framework
{
    public class EntityFinder : System.Collections.Specialized.NameObjectCollectionBase
    {
        public void Add(string propertyName, object propertyValue)
        {
            this.BaseAdd(propertyName, propertyValue);
        }

        public string GetKey(int index)
        {
            return base.BaseGetKey(index);
        }

        public object GetValue(int index)
        {
            return base.BaseGet(index);
        }
    }
}
