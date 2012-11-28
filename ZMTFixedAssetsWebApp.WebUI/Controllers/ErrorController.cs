using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewData["error_path"] = aspxerrorpath;
            return View();
        }


        public ActionResult AccessDenied(string action)
        {
            ViewData["action"] = action;
            return View();
        }


    }
}
