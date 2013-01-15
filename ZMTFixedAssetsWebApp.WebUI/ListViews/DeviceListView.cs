using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMTFixedAssetsWebApp.WebUI.Models;
using ZMTFixedAssetsWebApp.WebUI.LinqHelpers;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.WebUI.Controllers;

namespace ZMTFixedAssetsWebApp.WebUI.ListViews
{

    public sealed class DeviceListView : ListViewModel<Device, DeviceExtendedModel>
    {
        private IRepository<PeripheralDevice> peripheralDeviceRepository;
        private PeripheralDeviceController peripheralDeviceController;

        public DeviceListView(IRepository<Device> deviceRepository, PeripheralDeviceController peripheralDeviceController):base(deviceRepository)
        {
            this.peripheralDeviceController = peripheralDeviceController;
        }

        protected override DeviceExtendedModel CreateExtendedObejctModelFromObjectModel(Device obj)
        {
            DeviceExtendedModel model = new DeviceExtendedModel();
            model.comment = obj.comment;
            model.id = obj.id;
            model.id_fixed_asset = obj.id_fixed_asset;
            model.id_peripheral_device = obj.id_peripheral_device;
            model.ip_address = obj.ip_address;
            model.mac_address = obj.mac_address;
            model.model = obj.model;
            model.producer = obj.producer;
            model.serial_number = obj.serial_number;
            model.peripheral_device_name = peripheralDeviceController.GetPeripheralDeviceNameById(obj.id_peripheral_device);
            return model;
        }

        public override List<System.Web.Mvc.SelectListItem> OrderByList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "ID", Value = "id" });
            items.Add(new SelectListItem { Text = "Nr. inwentarzowy", Value = "id_fixed_asset" });
            items.Add(new SelectListItem { Text = "Adres IP", Value = "ip_address" });
            items.Add(new SelectListItem { Text = "Adres MAC", Value = "mac_address" });
            items.Add(new SelectListItem { Text = "Model", Value = "model" });
            items.Add(new SelectListItem { Text = "Producent", Value = "producer" });
            items.Add(new SelectListItem { Text = "Numer seryjny", Value = "serial_number" });
            items.Add(new SelectListItem { Text = "Typ urządzenia", Value = "peripheral_device_name" });
            return items;
        }

        protected override CountRecordsAndCreateListModel<Device> CountRecordsAndCreateListModel(Domain.Abstract.IRepository<Device> repo, string sortby, bool asc, string query, bool search)
        {
            List<Device> deviceList = new List<Device>();
            Dictionary<string, string> QueryList = CreateQueryListDictionary(query);
            deviceList = Repositry.Repository.OrderByFieldNullLast(sortby, asc, "id").ToList();
            

            if (QueryList.Count != 0)
            {
                string ID, InwNumber, IPAddress, MACAddress, Model, Producer, SerialNumber, PeripheralDevice;
                ID = InwNumber = IPAddress = MACAddress = Model = Producer = SerialNumber = PeripheralDevice = "";
                QueryList.TryGetValue("ID", out ID);
                QueryList.TryGetValue("InwNumber", out InwNumber);
                QueryList.TryGetValue("IPAddress", out IPAddress);
                QueryList.TryGetValue("MACAddress", out MACAddress);
                QueryList.TryGetValue("Model", out Model);
                QueryList.TryGetValue("Producer", out Producer);
                QueryList.TryGetValue("SerialNumber", out SerialNumber);
                QueryList.TryGetValue("PeripheralDevice", out PeripheralDevice);

                int _id, _inw_number;
        
                int.TryParse(ID, out _id);
                int.TryParse(InwNumber, out _inw_number);


                deviceList = deviceList.Where(x =>
                    (ID != null ? x.id == _id : x.id != 0) &&
                   // (_inw_number != null ? x.id_fixed_asset == _inw_number : x.id_fixed_asset != 0)
                    (IPAddress != null ? x.ip_address == IPAddress : x.ip_address != "") &&
                    (MACAddress != null ? x.mac_address == MACAddress : x.mac_address != "") &&
                    (Model != null ? x.model == Model : x.model != "") &&
                    (Producer != null ? x.producer == Producer : x.producer != "") &&
                    (SerialNumber != null ? x.serial_number == SerialNumber : x.serial_number != "") &&
                    (PeripheralDevice != null ? x.id_peripheral_device == 1 : x.id_peripheral_device != 0)
                    ).ToList();
            }

            int count = deviceList.Count();
            return new CountRecordsAndCreateListModel<Device>() { List = deviceList.AsQueryable(), Count = count };
        
        }
    }
}