using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Sitecore.Pipelines;

namespace Sitecore.Foundation.API.App_Start
{
    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("EmployeeApi", "api/sitecore/{controller}/{action}/{id}",
                new { controller = "EmployeeApi", id = UrlParameter.Optional },
                new[] { "Sitecore.Foundation.API.Controllers" }
            );
            RouteTable.Routes.MapRoute("NexaApi", "api/sitecore/{controller}/{action}/{id}",
                new { controller = "NexaApi", id = UrlParameter.Optional },
                new[] { "Sitecore.Foundation.API.Controllers" }
            );
            RouteTable.Routes.MapRoute("WebHttpApi", "api/sitecore/{controller}/{action}/{id}",
                new { controller = "WebHttpApi", id = UrlParameter.Optional },
                new[] { "Sitecore.Foundation.API.Controllers" }
            );
        }
    }
}