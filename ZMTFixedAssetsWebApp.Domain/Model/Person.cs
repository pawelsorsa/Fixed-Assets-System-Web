//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ZMTFixedAssetsWebApp.Domain.Model
{
    public partial class Person
    {
        #region Primitive Properties
    
        public virtual int id
        {
            get;
            set;
        }
    
        public virtual string name
        {
            get;
            set;
        }
    
        public virtual string surname
        {
            get;
            set;
        }
    
        public virtual string email
        {
            get;
            set;
        }
    
        public virtual Nullable<int> area_code
        {
            get;
            set;
        }
    
        public virtual Nullable<int> phone_number
        {
            get;
            set;
        }
    
        public virtual Nullable<int> phone_number2
        {
            get;
            set;
        }
    
        public virtual int id_section
        {
            get;
            set;
        }

        #endregion
    }
}