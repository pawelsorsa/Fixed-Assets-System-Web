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
    public class DeviceController : Controller
    {
        private IRepository<Device> deviceRepository;
        private PeripheralDeviceController peripheralDeviceController;
        private DeviceListView deviceListView;


        public DeviceController(IRepository<Device> deviceRepository, IRepository<PeripheralDevice> peripheralRepository)
        {
            this.deviceRepository = deviceRepository;
            peripheralDeviceController = new PeripheralDeviceController(peripheralRepository);
            deviceListView = new DeviceListView(this.deviceRepository, peripheralDeviceController);
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


    }
}
