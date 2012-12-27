using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    public class SectionController : Controller
    {
        public IRepository<Section> repository;

        public SectionController(IRepository<Section> repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View();
        }


        public Dictionary<int, string> GetAllShortNameSections()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list = repository.Repository.Select(x => new { x.id, x.short_name }).Distinct().ToDictionary(f => f.id, s => s.short_name);
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
            IEnumerable<string> list = repository.Repository.Select(x => x.short_name).AsEnumerable();
            return list;
        }





    }
}
