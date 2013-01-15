//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace ZMTFixedAssetsWebApp.Domain.Model
{
    public partial class Entities : ObjectContext
    {
        public const string ConnectionString = "name=Entities";
        public const string ContainerName = "Entities";
    
        #region Constructors
    
        public Entities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public Entities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public Entities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<Contractor> Contractors
        {
            get { return _contractors  ?? (_contractors = CreateObjectSet<Contractor>("Contractors")); }
        }
        private ObjectSet<Contractor> _contractors;
    
        public ObjectSet<Kind> Kinds
        {
            get { return _kinds  ?? (_kinds = CreateObjectSet<Kind>("Kinds")); }
        }
        private ObjectSet<Kind> _kinds;
    
        public ObjectSet<Licence> Licences
        {
            get { return _licences  ?? (_licences = CreateObjectSet<Licence>("Licences")); }
        }
        private ObjectSet<Licence> _licences;
    
        public ObjectSet<Subgroup> Subgroups
        {
            get { return _subgroups  ?? (_subgroups = CreateObjectSet<Subgroup>("Subgroups")); }
        }
        private ObjectSet<Subgroup> _subgroups;
    
        public ObjectSet<Person> People
        {
            get { return _people  ?? (_people = CreateObjectSet<Person>("People")); }
        }
        private ObjectSet<Person> _people;
    
        public ObjectSet<Section> Sections
        {
            get { return _sections  ?? (_sections = CreateObjectSet<Section>("Sections")); }
        }
        private ObjectSet<Section> _sections;
    
        public ObjectSet<Device> Devices
        {
            get { return _devices  ?? (_devices = CreateObjectSet<Device>("Devices")); }
        }
        private ObjectSet<Device> _devices;
    
        public ObjectSet<FixedAsset> FixedAssets
        {
            get { return _fixedAssets  ?? (_fixedAssets = CreateObjectSet<FixedAsset>("FixedAssets")); }
        }
        private ObjectSet<FixedAsset> _fixedAssets;
    
        public ObjectSet<PeripheralDevice> PeripheralDevices
        {
            get { return _peripheralDevices  ?? (_peripheralDevices = CreateObjectSet<PeripheralDevice>("PeripheralDevices")); }
        }
        private ObjectSet<PeripheralDevice> _peripheralDevices;

        #endregion
    }
}
