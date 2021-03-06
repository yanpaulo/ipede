﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using iPede.Site;
using System.Web.Http;

namespace iPede.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            AutoMapperConfig.Configure();
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Entities.iPedeContext,
                        Migrations.Configuration>());
            }
        }
    }
}
