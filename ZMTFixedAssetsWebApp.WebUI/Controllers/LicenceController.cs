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
    public class LicenceController : Controller
    {
        private IRepository<Licence> licenceRepository;
        private LicenceListView licenceListView;

        public LicenceController(IRepository<Licence> licenceRepository)
        {
            this.licenceRepository = licenceRepository;
            this.licenceListView = new LicenceListView(licenceRepository);
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Licence/_LicenceIndex", licenceListView.CreateListModel(1, false, "id_number", 10, false, false, ""));
            }
            return View(licenceListView.CreateListModel(1, false, "id_number", 10, false, false, ""));
        }

        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "id_number", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Licence/_LicenceList", licenceListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(licenceListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<Licence> model)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Licence licence = licenceRepository.Repository.FirstOrDefault(x => x.id_number == id);

            if (licence != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Licence/_LicenceEdit", licence);
                }
                return View("Licence", licence);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podana licencja nie istnieje",
                    Action = "Index",
                    Controller = "Licence"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [HttpPost]
        public ActionResult Edit(Licence model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Licence licence = licenceRepository.Repository.FirstOrDefault(x => x.id_number == model.id_number);
                    licence.name = model.name;
                    licenceRepository.EditObject(licence);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas edytowania licencji.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Nie udało się edytować licencji. Proszę skontaktować się z administratorem. " + ex.InnerException);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Licence/_LicenceEdit", model);
                }
                return View(model);
            }
        }


        public ActionResult Details(int id)
        {
            Licence model = licenceRepository.Repository.FirstOrDefault(x => x.id_number == id);
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Licence/_LicenceDetails", model);
                }
                return View("Details", model);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Licencja nie istnieje",
                    Action = "Index",
                    Controller = "Licence"
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
            Licence kind = licenceRepository.Repository.FirstOrDefault(x => x.id_number == id);
            if (kind != null)
            {
                DeleteObjectById model = new DeleteObjectById();
                model.Description = "Czy napewno chcesz usunąć licencje: " + kind.name + "?";
                model.Id = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Licence/_LicenceDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podana licencja nie istnieje",
                    Action = "Index",
                    Controller = "Licence"
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
                Licence kind = new Licence() { id_number = model.Id };
                licenceRepository.DeleteObject(kind);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania licencji. Proszę skontaktować się z administratorem", ex.InnerException);

            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            Licence model = new Licence();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Licence/_LicenceAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Licence model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    licenceRepository.AddObject(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania licencji. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Licence/_LicenceAdd", model);
                }
                return View(model);
            }
        }

        public JsonResult SortByList()
        {
            IQueryable list = licenceListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }


    }
}
