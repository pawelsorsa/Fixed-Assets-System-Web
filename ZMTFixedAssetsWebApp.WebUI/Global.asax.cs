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
               "Person",                                      // URL with parameters
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
             new { controller = "Person", action = "List", page = UrlParameter.Optional, sortby = UrlParameter.Optional },
             new { page = @"^\d{1,3}$" }
            );

            routes.MapRoute(
              null,                                           // Route name
              "Person/Edit/{id}",                                      // URL with parameters
              new { controller = "Person", action = "Edit", id = UrlParameter.Optional }
              );


            routes.MapRoute(
             null,                                           // Route name
             "Person/Delete/{id}",                                      // URL with parameters
             new { controller = "Person", action = "Delete", id = UrlParameter.Optional }
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

            // **************** MEMEBERSHIPUSER ************************************

            routes.MapRoute(
            null,                                           // Route name
            "MembershipUser",                                      // URL with parameters
            new { controller = "MembershipUser", action = "Index", sortby = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "MembershipUser/Delete/{username}",                                      // URL with parameters
            new { controller = "MembershipUser", action = "Delete", username = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "MembershipUser/Edit/{username}",                                      // URL with parameters
            new { controller = "MembershipUser", action = "Edit", username = UrlParameter.Optional }
            );


            // **************** MEMEBERSHIPROLE ************************************

            routes.MapRoute(
            null,                                           // Route name
            "MembershipRole",                                      // URL with parameters
            new { controller = "MembershipRole", action = "Index", sortby = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "MembershipRole/Delete/{RoleName}",                                      // URL with parameters
            new { controller = "MembershipRole", action = "Delete", RoleName = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "MembershipRole/Edit/{RoleName}",                                      // URL with parameters
            new { controller = "MembershipRole", action = "Edit", username = UrlParameter.Optional }
            );

            // **************** SECTION ************************************

            routes.MapRoute(
            null,                                           // Route name
            "Section",                                      // URL with parameters
            new { controller = "Section", action = "Index", sortby = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "Section/Delete/{ShortName}",                                      // URL with parameters
            new { controller = "Section", action = "Delete", ShortName = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "Section/Edit/{ShortName}",                                      // URL with parameters
            new { controller = "Section", action = "Edit", ShortName = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "Section/Details/{ShortName}",                                      // URL with parameters
            new { controller = "Section", action = "Details", ShortName = UrlParameter.Optional }
            );


            // **************** DEVICE ************************************

            routes.MapRoute(
            null,                                           // Route name
            "Device",                                      // URL with parameters
            new { controller = "Device", action = "Index", sortby = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "Device/Delete/{id}",                                      // URL with parameters
            new { controller = "Device", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "Device/Edit/{id}",                                      // URL with parameters
            new { controller = "Device", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "Device/Details/{id}",                                      // URL with parameters
            new { controller = "Device", action = "Details", id = UrlParameter.Optional }
            );

            // **************** PERIPHERAL DEVICE ************************************

            routes.MapRoute(
            null,                                           // Route name
            "PeripheralDevice",                                      // URL with parameters
            new { controller = "PeripheralDevice", action = "Index", sortby = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "PeripheralDevice/Delete/{id}",                                      // URL with parameters
            new { controller = "PeripheralDevice", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "PeripheralDevice/Edit/{id}",                                      // URL with parameters
            new { controller = "PeripheralDevice", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            null,                                           // Route name
            "PeripheralDevice/Details/{id}",                                      // URL with parameters
            new { controller = "PeripheralDevice", action = "Details", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
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