using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Website1.Controllers
{
    public class CarTilesController : Controller
    {
        // GET: CarTiles
        public ActionResult CarTiles()
        {
            //Sitecore.Data.Database content = Sitecore.Context.ContentDatabase;
            //Sitecore.Data.Database database = Sitecore.Data.Database.GetDatabase("master");
            //Sitecore.Data.Items.Item item = database.SelectSingleItem("fast://sitecore/content/Website1/Website1/Global/CarTiles/");
            return View();
        }
    }
}