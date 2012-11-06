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
    public partial class Device
    {
        #region Primitive Properties
    
        public virtual int id
        {
            get;
            set;
        }
    
        public virtual int id_peripheral_device
        {
            get { return _id_peripheral_device; }
            set
            {
                if (_id_peripheral_device != value)
                {
                    if (PeripheralDevice != null && PeripheralDevice.id != value)
                    {
                        PeripheralDevice = null;
                    }
                    if (PeripheralDevice1 != null && PeripheralDevice1.id != value)
                    {
                        PeripheralDevice1 = null;
                    }
                    _id_peripheral_device = value;
                }
            }
        }
        private int _id_peripheral_device;
    
        public virtual string serial_number
        {
            get;
            set;
        }
    
        public virtual string ip_address
        {
            get;
            set;
        }
    
        public virtual string mac_address
        {
            get;
            set;
        }
    
        public virtual string producer
        {
            get;
            set;
        }
    
        public virtual string model
        {
            get;
            set;
        }
    
        public virtual string comment
        {
            get;
            set;
        }
    
        public virtual int id_fixed_asset
        {
            get { return _id_fixed_asset; }
            set
            {
                if (_id_fixed_asset != value)
                {
                    if (FixedAsset != null && FixedAsset.id != value)
                    {
                        FixedAsset = null;
                    }
                    _id_fixed_asset = value;
                }
            }
        }
        private int _id_fixed_asset;

        #endregion
        #region Navigation Properties
    
        public virtual PeripheralDevice PeripheralDevice
        {
            get { return _peripheralDevice; }
            set
            {
                if (!ReferenceEquals(_peripheralDevice, value))
                {
                    var previousValue = _peripheralDevice;
                    _peripheralDevice = value;
                    FixupPeripheralDevice(previousValue);
                }
            }
        }
        private PeripheralDevice _peripheralDevice;
    
        public virtual PeripheralDevice PeripheralDevice1
        {
            get { return _peripheralDevice1; }
            set
            {
                if (!ReferenceEquals(_peripheralDevice1, value))
                {
                    var previousValue = _peripheralDevice1;
                    _peripheralDevice1 = value;
                    FixupPeripheralDevice1(previousValue);
                }
            }
        }
        private PeripheralDevice _peripheralDevice1;
    
        public virtual FixedAsset FixedAsset
        {
            get { return _fixedAsset; }
            set
            {
                if (!ReferenceEquals(_fixedAsset, value))
                {
                    var previousValue = _fixedAsset;
                    _fixedAsset = value;
                    FixupFixedAsset(previousValue);
                }
            }
        }
        private FixedAsset _fixedAsset;

        #endregion
        #region Association Fixup
    
        private void FixupPeripheralDevice(PeripheralDevice previousValue)
        {
            if (previousValue != null && previousValue.Device.Contains(this))
            {
                previousValue.Device.Remove(this);
            }
    
            if (PeripheralDevice != null)
            {
                if (!PeripheralDevice.Device.Contains(this))
                {
                    PeripheralDevice.Device.Add(this);
                }
                if (id_peripheral_device != PeripheralDevice.id)
                {
                    id_peripheral_device = PeripheralDevice.id;
                }
            }
        }
    
        private void FixupPeripheralDevice1(PeripheralDevice previousValue)
        {
            if (previousValue != null && previousValue.Device1.Contains(this))
            {
                previousValue.Device1.Remove(this);
            }
    
            if (PeripheralDevice1 != null)
            {
                if (!PeripheralDevice1.Device1.Contains(this))
                {
                    PeripheralDevice1.Device1.Add(this);
                }
                if (id_peripheral_device != PeripheralDevice1.id)
                {
                    id_peripheral_device = PeripheralDevice1.id;
                }
            }
        }
    
        private void FixupFixedAsset(FixedAsset previousValue)
        {
            if (previousValue != null && previousValue.Device.Contains(this))
            {
                previousValue.Device.Remove(this);
            }
    
            if (FixedAsset != null)
            {
                if (!FixedAsset.Device.Contains(this))
                {
                    FixedAsset.Device.Add(this);
                }
                if (id_fixed_asset != FixedAsset.id)
                {
                    id_fixed_asset = FixedAsset.id;
                }
            }
        }

        #endregion
    }
}