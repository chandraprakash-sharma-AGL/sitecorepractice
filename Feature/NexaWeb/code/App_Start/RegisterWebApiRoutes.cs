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
                RouteTable.Routes.MapRoute("EBooking", "api/sitecore/{controller}/{action}/{id}",
                new { controller = "EBooking", id = UrlParameter.Optional },
                new[] { "Sitecore.Foundation.NexaWeb.Controllers" }
            );
        }
    }
}