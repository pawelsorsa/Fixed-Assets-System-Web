using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZMTFixedAssetsWebApp.WebUI.Infrastructure;
using ZMTFixedAssetsWebApp.WebUI.Controllers;

namespace ZMTFixedAssetsWebApp.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            //filters.Add(new HandleErrorAttribute()
            //{
            //    ExceptionType = typeof(Exception),
            //    View = "Index",
            //});

            //filters.Add(new HandleErrorAttribute
            //{
            //    ExceptionType = typeof(HttpException),
            //    View = "NotFound"
            //});

            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          
            routes.MapRoute(
               null,                                           // Route name
               "Person/Index",                                      // URL with parameters
               new { controller = "Person", action = "Index", sortby = UrlParameter.Optional }
            );
            
            routes.MapRoute(
               null,                                           // Route name
               "Person/List",                                      // URL with parameters
               new { controller = "Person", action = "List", sortby = UrlParameter.Optional }
            );

            routes.MapRoute(
             null,                                           // Route name
             "Person/List/{page}",                                      // URL with parameters
             new { controller = "Person", action = "List", section = (string)null, page = UrlParameter.Optional, sortby = UrlParameter.Optional },
             new { page = @"^\d{1,3}$" }
            );

            routes.MapRoute(
               null,                                           // Route name
               "Person/List/{section}",                                      // URL with parameters
               new { controller = "Person", action = "List", section = UrlParameter.Optional, page = 1 }
            );

            routes.MapRoute(
               null,                                           // Route name
               "Person/List/{section}/{page}",                                      // URL with parameters
               new { controller = "Person", action = "List", section = UrlParameter.Optional, page = 1 }
            );

            routes.MapRoute(
             null,                                           // Route name
             "Person/Edit/{id}",                                      // URL with parameters
             new { controller = "Person", action = "Edit", id = UrlParameter.Optional },
             new { id = @"^\d{1,3}$" }
             );

            routes.MapRoute(
                "SortByList",
                "Person/Lists/SortByList",
                new { controller = "Person", action = "SortByList" }
            );

            routes.MapRoute(
               null,
               "Person/Search",
               new { controller = "Person", action = "Search", query = UrlParameter.Optional }
           );
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Person", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

      
    }
}