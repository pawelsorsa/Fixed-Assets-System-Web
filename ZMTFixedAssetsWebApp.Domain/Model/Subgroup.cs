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
    public partial class Subgroup
    {
        #region Primitive Properties
    
        public virtual int id
        {
            get;
            set;
        }
    
        public virtual string short_name
        {
            get;
            set;
        }
    
        public virtual string name
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<FixedAsset> FixedAsset
        {
            get
            {
                if (_fixedAsset == null)
                {
                    var newCollection = new FixupCollection<FixedAsset>();
                    newCollection.CollectionChanged += FixupFixedAsset;
                    _fixedAsset = newCollection;
                }
                return _fixedAsset;
            }
            set
            {
                if (!ReferenceEquals(_fixedAsset, value))
                {
                    var previousValue = _fixedAsset as FixupCollection<FixedAsset>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupFixedAsset;
                    }
                    _fixedAsset = value;
                    var newValue = value as FixupCollection<FixedAsset>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupFixedAsset;
                    }
                }
            }
        }
        private ICollection<FixedAsset> _fixedAsset;

        #endregion
        #region Association Fixup
    
        private void FixupFixedAsset(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (FixedAsset item in e.NewItems)
                {
                    item.Subgroup = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (FixedAsset item in e.OldItems)
                {
                    if (ReferenceEquals(item.Subgroup, this))
                    {
                        item.Subgroup = null;
                    }
                }
            }
        }

        #endregion
    }
}