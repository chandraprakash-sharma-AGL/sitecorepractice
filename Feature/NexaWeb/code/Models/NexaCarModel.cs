
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.NexaWeb.Models
{
    [SitecoreType(TemplateId = "{250B8620-3DA4-44D7-9955-091C92554E48}", AutoMap = true)]
    public class NexaCarModel
    {
        public List<CarDetails> NexaCars { get; set; }
    }
    public class CarDetails
    {
        [SitecoreField("CarModelName")]
        public virtual string CarModelName { get; set; }

        [SitecoreField("CarModelCode")]
        public virtual string CarModelCode { get; set; }

        [SitecoreField("CarLogoImage")]
        public virtual MvcHtmlString CarLogoImage { get; set; }

        [SitecoreField("CarImage")]
        public virtual MvcHtmlString CarImage { get; set; }
    }
}

