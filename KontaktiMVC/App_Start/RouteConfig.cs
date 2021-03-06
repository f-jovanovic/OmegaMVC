﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KontaktiMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "BrojRoute",                                           // Route name
                "Broj/{action}/{id}",                            // URL with parameters
                new { controller = "Broj", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Osoba", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
