using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.ListViews;
using ZMTFixedAssetsWebApp.WebUI.Models;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [HandleError]
    public class DeviceController : Controller
    {
        private IRepository<Device> deviceRepository;
        private PeripheralDeviceController peripheralDeviceController;
        private DeviceListView deviceListView;


        public DeviceController(IRepository<Device> deviceRepository, IRepository<PeripheralDevice> peripheralRepository)
        {
            this.deviceRepository = deviceRepository;
            peripheralDeviceController = new PeripheralDeviceController(peripheralRepository);
            deviceListView = new DeviceListView(this.deviceRepository);
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Device/_DeviceIndex", deviceListView.CreateListModel(1, false, "id", 10, false, false, ""));
            }
            return View(deviceListView.CreateListModel(1, false, "id", 10, false, false, ""));
        }

        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "id", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Device/_DeviceList", deviceListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(deviceListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }


        [HttpPost]
        public ActionResult List(PaginatedListModel<DeviceExtendedModel> model)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        public ActionResult Details(int id)
        {
            Device model = deviceRepository.Repository.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Device/_DeviceDetails", model);
                }
                return View("Details", model);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Podane urządzenie nie istnieje",
                    Action = "Index",
                    Controller = "Device"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", inf_model);
                }
                return View("Info", inf_model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Device device = deviceRepository.Repository.FirstOrDefault(x => x.id == id);

            if (device != null)
            {

                DeviceExtendedModel model = CreateDeviceModelFromDevice(device);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Device/_DeviceEdit", model);
                }
                return View("Device", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podane urządzenie nie istnieje",
                    Action = "Index",
                    Controller = "Device"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [HttpPost]
        public ActionResult Edit(DeviceExtendedModel model)
        {
            //ModelState.Remove("id");
            if (ModelState.IsValid)
            {
                try
                {
                    Device device = deviceRepository.Repository.FirstOrDefault(x => x.id == model.id);
                    UpdateDevice(ref device, model);
                    deviceRepository.EditObject(device);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception("Nie udało się edytować urządzenia. Proszę skontaktować się z administratorem. " + ex.InnerException);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Device/_DeviceEdit", model);
                }
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult Add()
        {
            DeviceExtendedModel model = new DeviceExtendedModel();
            model.SectionList = peripheralDeviceController.DevicesListItem();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Device/_DeviceAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(DeviceExtendedModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Device device = CreateDeviceFromDeviceModel(model);
                    deviceRepository.AddObject(device);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania urządzenia. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                model.SectionList = peripheralDeviceController.DevicesListItem();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Device/_DeviceAdd", model);
                }
                return View(model);
            }
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            Device device = deviceRepository.Repository.FirstOrDefault(x => x.id == id);
            if (device != null)
            {
                DeleteObjectById model = new DeleteObjectById();
                model.Description = "Czy napewno chcesz usunąć urządzenie: " + device.id +  "?";
                model.Id = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Device/_DeviceDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podane urządzenie nie istnieje",
                    Action = "Index",
                    Controller = "Device"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }



        [HttpPost]
        public ActionResult Delete(DeleteObjectById model)
        {
            Device device = new Device() { id = model.Id };
            deviceRepository.DeleteObject(device);
            return RedirectToAction("Index");

        }




        public JsonResult SortByList()
        {
            IQueryable list = deviceListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }

        DeviceExtendedModel CreateDeviceModelFromDevice(Device device)
        {
            DeviceExtendedModel model = new DeviceExtendedModel();
            model.comment = device.comment;
            model.id = device.id;
            model.id_fixed_asset = device.id_fixed_asset;
            model.ip_address = device.ip_address;
            model.mac_address = device.mac_address;
            model.modell = device.model;
            model.peripheral_device_id = device.id_peripheral_device;
            model.peripheral_device_name = device.PeripheralDevice.name;
            model.producer = device.producer;
            model.serial_number = device.serial_number;

            return model;
        }

        Device CreateDeviceFromDeviceModel(DeviceExtendedModel device)
        {
            Device model = new Device();
            model.comment = device.comment;
            model.id = device.id;
            model.id_fixed_asset = device.id_fixed_asset;
            model.ip_address = device.ip_address;
            model.mac_address = device.mac_address;
            int id_peripheral;
            int.TryParse(device.peripheral_device_name, out id_peripheral);
            model.id_peripheral_device = id_peripheral;
            model.producer = device.producer;
            model.serial_number = device.serial_number;

            return model;
        }

        void UpdateDevice(ref Device dev, DeviceExtendedModel model)
        {
            dev.comment = model.comment;
            dev.id_fixed_asset = model.id_fixed_asset;
            dev.ip_address = model.ip_address;
            dev.mac_address = model.mac_address;
            dev.model = model.modell;
            dev.producer = model.producer;
            dev.serial_number = model.serial_number;
        }
    }
}
