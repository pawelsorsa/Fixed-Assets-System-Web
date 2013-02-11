using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZMTFixedAssetsWebApp.WebUI.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Home/_Index");
            }
            return View();
        }

    }
}
