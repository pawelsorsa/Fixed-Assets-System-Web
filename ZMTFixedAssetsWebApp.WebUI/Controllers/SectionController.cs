using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;
using ZMTFixedAssetsWebApp.WebUI.ListViews;
using ZMTFixedAssetsWebApp.WebUI.Models;

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



    }
}
