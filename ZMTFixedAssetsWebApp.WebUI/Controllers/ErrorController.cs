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
            if (Request.IsAjaxRequest())
            {
                return View("_ErrorIndex");
            }

            return View();
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
