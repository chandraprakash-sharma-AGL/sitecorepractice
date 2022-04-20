using Sitecore.Feature.NexaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.NexaWeb.Controllers
{
    public class NexaWebController : Controller
    {
        // GET: NexaWeb
        public ActionResult Comments()
        {
            var model = new CommentsModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Comments(CommentsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return Redirect("/");
        }
    }
}


