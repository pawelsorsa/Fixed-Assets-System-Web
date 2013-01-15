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
    public class SectionController : Controller
    {
        public IRepository<Section> sectionRepository;
        private SectionListView sectionListView;


        public SectionController(IRepository<Section> repo)
        {
            sectionRepository = repo;
            sectionListView = new SectionListView(sectionRepository);
        }

        public ActionResult Index()
        {

            if (Request.IsAjaxRequest())
            {
                return PartialView("Section/_SectionIndex", sectionListView.CreateListModel(1, false, "Id", 10, false, false, ""));
            }

            return View(sectionListView.CreateListModel(1, false, "Id", 10, false, false, ""));
        }


        [HttpGet]
        public ActionResult List(int Page = 1, bool ShowAll = false, string OrderBy = "Id", int ItemsPerPage = 10, bool ASC = false, bool Search = false, string Query = "")
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Section/_SectionList", sectionListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
            }

            return View(sectionListView.CreateListModel(Page, ShowAll, OrderBy, ItemsPerPage, ASC, Search, Query));
        }

        [HttpPost]
        public ActionResult List(PaginatedListModel<Section> model)
        {
            model.Page = 1;
            return RedirectToAction("List", model);
        }


        [HttpGet]
        public ActionResult Edit(string ShortName)
        {
            Section model = sectionRepository.Repository.FirstOrDefault(x => x.short_name == ShortName);

            if (model != null)
            {
                SectionModel sm = CreateSectionModelFromSection(model);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Section/_SectionEdit", sm);
                }
                return View("Edit", sm);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Podana sekcja nie istnieje",
                    Action = "Index",
                    Controller = "Section"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", inf_model);
                }
                return View("Info", inf_model);
            }
        }


        [HttpPost]
        public ActionResult Edit(SectionModel model)
        {
            ModelState.Remove("short_name");
            if (ModelState.IsValid)
            {
                try
                {
                    Section sec = sectionRepository.Repository.FirstOrDefault(x => x.id == model.id);
                    UpdateSection(ref sec, model);
                    sectionRepository.EditObject(sec);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas edytowania sekcji. Pole skrócona nazwa istnieje w bazie danych. Proszę podać inną nazwę.", ex.InnerException);
                }
                catch (Exception ex)
                {
                    throw new Exception("Nie udało się edytować sekcji. Proszę skontaktować się z administratorem. Error message: " + ex.Message);
                }
                
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Section/_SectionEdit", model);
                }
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Delete(string ShortName)
        {
            Section section = sectionRepository.Repository.FirstOrDefault(x => x.short_name == ShortName);
            if (section != null)
            {
                DeleteObjectByName model = new DeleteObjectByName();
                model.Description = "Czy napewno chcesz usunąć sekcje: " + section.short_name + "?";
                model.Name = ShortName;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Section/_SectionDelete", model);
                }

                return View("Delete", model);
            }
            else
            {
                InfoModel model = new InfoModel()
                {
                    Description = "Podana sekcja nie istnieje",
                    Action = "Index",
                    Controller = "Section"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", model);
                }
                return View("Info", model);
            }
        }

        [HttpPost]
        public ActionResult Delete(DeleteObjectByName model)
        {
            try
            {
                Section sec = new Section() { short_name = model.Name };
                sectionRepository.DeleteObject(sec);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Wystąpił błąd podczas usuwania sekcji. Aby usunąć sekcję należy usunąć z niej wszystkich pracowników", ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception("Wystąpił błąd podczas usuwania sekcji. Proszę skontaktować się z administratorem", ex.InnerException);
           
            }
        }


        [HttpGet]
        public ActionResult Add()
        {
            SectionModel model = new SectionModel();

            if (Request.IsAjaxRequest())
            {
                return PartialView("Section/_SectionAdd", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SectionModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Section section = CreateSectionFromSectionModel(model);
                    sectionRepository.AddObject(section);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    throw new DbUpdateException("Wystąpił błąd podczas dodawania sekcji. Pole skrócona nazwa nie może się powtarzać: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Wystąpił błąd podczas dodawania sekcji. Proszę o kontakt z administratorem. Error message: " + ex.Message);
                }
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Section/_SectionAdd", model);
                }
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult Details(string ShortName)
        {
            Section model = sectionRepository.Repository.FirstOrDefault(x => x.short_name == ShortName);
            if (model != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Section/_SectionDetails", model);
                }
                return View("Details", model);
            }
            else
            {
                InfoModel inf_model = new InfoModel()
                {
                    Description = "Podana sekcja nie istnieje",
                    Action = "Index",
                    Controller = "Section"
                };
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Info", inf_model);
                }
                return View("Info", inf_model);
            }
        }


        public Dictionary<int, string> GetAllShortNameSections()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list = sectionRepository.Repository.Select(x => new { x.id, x.short_name }).Distinct().ToDictionary(f => f.id, s => s.short_name);
            return list;
        }

        public int GetSectionIdIfSectionExist(string section)
        {
            int id_section = 0;

            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict = this.GetAllShortNameSections();
            if (dict.ContainsValue(section))
            {
                id_section = dict.Where(x => x.Value == section).Select(x => x.Key).First();
            }

            return id_section;
        }

        public List<SelectListItem> SectionsShortNamesList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var x in this.GetAllShortNameSections())
            {
                items.Add(new SelectListItem { Text = x.Value, Value = x.Key.ToString() });
            }

            return items;
        }

        IEnumerable<string> SectionNameList()
        {
            IEnumerable<string> list = sectionRepository.Repository.Select(x => x.short_name).AsEnumerable();
            return list;
        }

        public JsonResult SortByList()
        {
            IQueryable list = sectionListView.OrderByList().AsQueryable();

            return Json(new SelectList(
                            list,
                            "Value",
                            "Text"), JsonRequestBehavior.AllowGet);
        }

        public void UpdateSection(ref Section section, SectionModel model)
        {
            section.email = model.email;
            section.id = model.id;
            section.locality = model.locality;
            section.name = model.name;
            section.phone_number = model.phone_number;
            section.post = model.post;
            section.postal_code = model.postal_code;
            section.short_name = model.short_name;
            section.street = model.street;
        }

        public SectionModel CreateSectionModelFromSection(Section section)
        {
            SectionModel temp = new SectionModel();
            temp.email = section.email;
            temp.id = section.id;
            temp.locality = section.locality;
            temp.name = section.name;
            temp.phone_number = section.phone_number;
            temp.post = section.post;
            temp.postal_code = section.postal_code;
            temp.short_name = section.short_name;
            temp.street = section.street;
            return temp;
        }

        public Section CreateSectionFromSectionModel(SectionModel model)
        {
            Section temp = new Section();
            temp.email = model.email;
            temp.id = model.id;
            temp.locality = model.locality;
            temp.name = model.name;
            temp.phone_number = model.phone_number;
            temp.post = model.post;
            temp.postal_code = model.postal_code;
            temp.short_name = model.short_name;
            temp.street = model.street;
            return temp;
        }


    }
}
