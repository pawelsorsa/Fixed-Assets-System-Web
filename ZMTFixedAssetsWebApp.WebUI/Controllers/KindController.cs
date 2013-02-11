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
    public class KindController : Controller
    {
        private IRepository<Kind> kindRepository;
        private KindListView kindListView;

        public KindController(IRepository<Kind> kindRepository)
        {
            this.kindRepository = kindRepository;
            this.kindListView = new KindListView(kindRepository);
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Kind/_KindIndex", kindListView.CreateListModel(1, false, "id", 10, false, false, ""));
            }
            return View(kindListView.CreateListModel(1, false, "id", 10, false, false, ""));
        }

        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "id", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Kind/_KindList", kindListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(kindListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<Kind> model)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Kind kind = kindRepository.Repository.FirstOrDefault(x => x.id == id);

            if (kind != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Kind/_KindEdit", kind);
                }
                return View("Kind", kind);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podany rodzaj nie istnieje",
                    Action = "Index",
                    Controller = "Kind"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [HttpPost]
        public ActionResult Edit(Kind model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Kind kind = kindRepository.Repository.FirstOrDefault(x => x.id == model.id);
                    kind.name = model.name;
                    kindRepository.EditObject(kind);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas edytowania rodzaju. Pole nazwa istnieje w bazie danych. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Nie udało się edytować rodzaju. Proszę skontaktować się z administratorem. " + ex.InnerException);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Kind/_KindEdit", model);
                }
                return View(model);
            }
        }


        public ActionResult Details(int id)
        {
            Kind model = kindRepository.Repository.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Kind/_KindDetails", model);
                }
                return View("Details", model);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Podany rodzaj nie istnieje",
                    Action = "Index",
                    Controller = "Kind"
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
            Kind kind = kindRepository.Repository.FirstOrDefault(x => x.id == id);
            if (kind != null)
            {
                DeleteObjectById model = new DeleteObjectById();
                model.Description = "Czy napewno chcesz usunąć rodzaj: " + kind.name + "?";
                model.Id = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Kind/_KindDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podany rodzaj nie istnieje",
                    Action = "Index",
                    Controller = "Kind"
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
                Kind kind = new Kind() { id = model.Id };
                kindRepository.DeleteObject(kind);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Wystąpił błąd podczas usuwania rodzaju. Aby usunąć rodzaj należy edytować urządzenia tego typu.", ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania rodzaju. Proszę skontaktować się z administratorem", ex.InnerException);

            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            Kind model = new Kind();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Kind/_KindAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Kind model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    kindRepository.AddObject(model);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas dodawania rodzaju. Podana nazwa istnieje w bazie. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania rodzaju. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Kind/_KindAdd", model);
                }
                return View(model);
            }
        }

        public JsonResult SortByList()
        {
            IQueryable list = kindListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }

    }
}
