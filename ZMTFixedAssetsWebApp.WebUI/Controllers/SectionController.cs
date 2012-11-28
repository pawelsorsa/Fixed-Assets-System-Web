using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMTFixedAssetsWebApp.Domain.Abstract;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    public class SectionController : Controller
    {
        public ISectionRepository repository;

        public SectionController(ISectionRepository repo)
        {
            repository = repo;
        }

        public Dictionary<int, string> GetAllShortNameSections()
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list = repository.Sections.Select(x => new { x.id, x.short_name }).Distinct().ToDictionary(f => f.id, s => s.short_name);
            return list;
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
