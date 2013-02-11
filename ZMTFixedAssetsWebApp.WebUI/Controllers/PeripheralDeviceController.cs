using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.ListViews;
using ZMTFixedAssetsWebApp.WebUI.Models;
using System.Data.Entity.Infrastructure;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [HandleError]
    public class PeripheralDeviceController : Controller
    {
        private IRepository<PeripheralDevice> peripheralDeviceRepository;
        private PeripheralDeviceListView peripheralDeviceListView;

        public PeripheralDeviceController(IRepository<PeripheralDevice> peripheralDeviceRepository)
        {
            this.peripheralDeviceRepository = peripheralDeviceRepository;
            this.peripheralDeviceListView = new PeripheralDeviceListView(peripheralDeviceRepository);
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("PeripheralDevice/_PeripheralDeviceIndex", peripheralDeviceListView.CreateListModel(1, false, "id", 10, false, false, ""));
            }
            return View(peripheralDeviceListView.CreateListModel(1, false, "id", 10, false, false, ""));
        }

        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "id", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("PeripheralDevice/_PeripheralDeviceList", peripheralDeviceListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(peripheralDeviceListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<PeripheralDevice> model)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            PeripheralDevice device = peripheralDeviceRepository.Repository.FirstOrDefault(x => x.id == id);

            if (device != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("PeripheralDevice/_PeripheralDeviceEdit", device);
                }
                return View("PeripheralDevice", device);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podane urządzenie nie istnieje",
                    Action = "Index",
                    Controller = "PeripheralDevice"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [HttpPost]
        public ActionResult Edit(PeripheralDevice model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    PeripheralDevice device = peripheralDeviceRepository.Repository.FirstOrDefault(x => x.id == model.id);
                    device.name = model.name;
                    peripheralDeviceRepository.EditObject(device);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas edytowania urządzenia peryferyjnego. Pole nazwa istnieje w bazie danych. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Nie udało się edytować urządzenia peryferyjnego. Proszę skontaktować się z administratorem. " + ex.InnerException);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("PeripheralDevice/_PeripheralDeviceEdit", model);
                }
                return View(model);
            }
        }



        public ActionResult Details(int id)
        {
            PeripheralDevice model = peripheralDeviceRepository.Repository.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("PeripheralDevice/_PeripheralDeviceDetails", model);
                }
                return View("Details", model);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Podane urządzenie peryferyjne nie istnieje",
                    Action = "Index",
                    Controller = "PeripheralDevice"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", inf_model);
                }
                return View("Info", inf_model);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PeripheralDevice device = peripheralDeviceRepository.Repository.FirstOrDefault(x => x.id == id);
            if (device != null)
            {
                DeleteObjectById model = new DeleteObjectById();
                model.Description = "Czy napewno chcesz usunąć urządzenie peryferyjne: " + device.name + "?";
                model.Id = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("PeripheralDevice/_PeripheralDeviceDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podane urządzenie peryferyjne nie istnieje",
                    Action = "Index",
                    Controller = "PeripheralDevice"
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
            try
            {
                PeripheralDevice device = new PeripheralDevice() { id = model.Id };
                peripheralDeviceRepository.DeleteObject(device);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Wystąpił błąd podczas usuwania urządznia peryferyjnego. Aby usunąć urządzenie peryferyjne należy edytować urządzenia tego typu.", ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania urządzenia peryferyjnego. Proszę skontaktować się z administratorem", ex.InnerException);

            }
        }


        [HttpGet]
        public ActionResult Add()
        {
            PeripheralDevice model = new PeripheralDevice();
            if (Request.IsAjaxRequest())
            {
                return PartialView("PeripheralDevice/_PeripheralDeviceAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(PeripheralDevice model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    peripheralDeviceRepository.AddObject(model);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas dodawania urządznia peryferyjnego. Podana nazwa istnieje w bazie. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania urządzenia peryferyjnego. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("PeripheralDevice/_PeripheralDeviceAdd", model);
                }
                return View(model);
            }
        }



        public JsonResult SortByList()
        {
            IQueryable list = peripheralDeviceListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }


        public string GetPeripheralDeviceNameById(int id)
        {
            PeripheralDevice dev = peripheralDeviceRepository.Repository.FirstOrDefault(x => x.id == id);
            if (dev == null) return ""; else return dev.name;
        }

        public int GetPeripheralDeviceIdByName(string name)
        {
            PeripheralDevice dev = peripheralDeviceRepository.Repository.FirstOrDefault(x => x.name == name);
            if (dev == null) return 0; else return dev.id;
        }

        public Dictionary<int, string> GetAllNames()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list = peripheralDeviceRepository.Repository.Select(x => new { x.id, x.name }).Distinct().ToDictionary(f => f.id, s => s.name);
            return list;
        }


        public List<SelectListItem> DevicesListItem()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var x in this.GetAllNames())
            {
                items.Add(new SelectListItem { Text = x.Value, Value = x.Key.ToString() });
            }

            return items;
        }

    }
}