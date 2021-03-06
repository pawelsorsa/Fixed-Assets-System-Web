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
    public partial class PeripheralDevice
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

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Device> Devices
        {
            get
            {
                if (_devices == null)
                {
                    var newCollection = new FixupCollection<Device>();
                    newCollection.CollectionChanged += FixupDevices;
                    _devices = newCollection;
                }
                return _devices;
            }
            set
            {
                if (!ReferenceEquals(_devices, value))
                {
                    var previousValue = _devices as FixupCollection<Device>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupDevices;
                    }
                    _devices = value;
                    var newValue = value as FixupCollection<Device>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupDevices;
                    }
                }
            }
        }
        private ICollection<Device> _devices;

        #endregion
        #region Association Fixup
    
        private void FixupDevices(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Device item in e.NewItems)
                {
                    item.PeripheralDevice = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Device item in e.OldItems)
                {
                    if (ReferenceEquals(item.PeripheralDevice, this))
                    {
                        item.PeripheralDevice = null;
                    }
                }
            }
        }

        #endregion
    }
}
