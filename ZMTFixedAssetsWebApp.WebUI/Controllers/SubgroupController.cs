using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.WebUI.ListViews;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.WebUI.Models;
using System.Data.Entity.Infrastructure;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    public class SubgroupController : Controller
    {
        private IRepository<Subgroup> subgroupRepository;
        private SubgroupListView subgroupListView;

        public SubgroupController(IRepository<Subgroup> subgroupRepository)
        {
            this.subgroupRepository = subgroupRepository;
            this.subgroupListView = new SubgroupListView(subgroupRepository);
        }

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Subgroup/_SubgroupIndex", subgroupListView.CreateListModel(1, false, "id", 10, false, false, ""));
            }
            return View(subgroupListView.CreateListModel(1, false, "id", 10, false, false, ""));
        }

        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "id", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Subgroup/_SubgroupList", subgroupListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(subgroupListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<Subgroup> model)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Subgroup subgroup = subgroupRepository.Repository.FirstOrDefault(x => x.id == id);

            if (subgroup != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Subgroup/_SubgroupEdit", subgroup);
                }
                return View("Subgroup", subgroup);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podana grupa nie istnieje",
                    Action = "Index",
                    Controller = "Subgroup"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [HttpPost]
        public ActionResult Edit(Subgroup model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Subgroup subgroup = subgroupRepository.Repository.FirstOrDefault(x => x.id == model.id);
                    subgroup.short_name = model.short_name;
                    subgroup.name = model.name;
                    subgroupRepository.EditObject(subgroup);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas edytowania grupy. Pole nazwa istnieje w bazie danych. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Nie udało się edytować grupy. Proszę skontaktować się z administratorem. " + ex.InnerException);
                }
            }
            else
            {   
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Subgroup/_SubgroupEdit", model);
                }
                return View(model);
            }
        }

        public ActionResult Details(int id)
        {
            Subgroup model = subgroupRepository.Repository.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Subgroup/_SubgroupDetails", model);
                }
                return View("Details", model);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Podana grupa nie istnieje",
                    Action = "Index",
                    Controller = "Subgroup"
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
            Subgroup subgroup = subgroupRepository.Repository.FirstOrDefault(x => x.id == id);
            if (subgroup != null)
            {
                DeleteObjectById model = new DeleteObjectById();
                model.Description = "Czy napewno chcesz usunąć grupę: " + subgroup.name + "?";
                model.Id = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Subgroup/_SubgroupDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podana grupa nie istnieje",
                    Action = "Index",
                    Controller = "Subgroup"
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
                Subgroup subgroup = new Subgroup() { id = model.Id };
                subgroupRepository.DeleteObject(subgroup);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Wystąpił błąd podczas usuwania grupy. Aby usunąć rodzaj należy edytować środki trwałe tego typu.", ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania grupy. Proszę skontaktować się z administratorem", ex.InnerException);

            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            Subgroup model = new Subgroup();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Subgroup/_SubgroupAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Subgroup model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    subgroupRepository.AddObject(model);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas dodawania grupy. Podana nazwa istnieje w bazie. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania grupy. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Subgroup/_SubgroupAdd", model);
                }
                return View(model);
            }
        }

        public JsonResult SortByList()
        {
            IQueryable list = subgroupListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }

    }
}
