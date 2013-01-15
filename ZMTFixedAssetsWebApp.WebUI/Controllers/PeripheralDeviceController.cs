using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    public class PeripheralDeviceController : Controller
    {
        private IRepository<PeripheralDevice> peripheralDeviceRepository;

        public PeripheralDeviceController(IRepository<PeripheralDevice> peripheralDeviceRepository)
        {
            this.peripheralDeviceRepository = peripheralDeviceRepository;
        }


        public string GetPeripheralDeviceNameById(int id)
        {
            return peripheralDeviceRepository.Repository.FirstOrDefault(x => x.id == id).name;
        }

        public Dictionary<int, string> GetAllNames()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list = peripheralDeviceRepository.Repository.Select(x => new { x.id, x.name }).Distinct().ToDictionary(f => f.id, s => s.name);
            return list;
        }

    }
}