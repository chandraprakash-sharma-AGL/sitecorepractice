using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.NexaWeb.Models
{
    [SitecoreType(TemplateId = "{F5A6EA3F-F731-4171-982A-89C25A72F49E}", AutoMap = true)]
    public class EBookingModel
    {
        [SitecoreField("CarList")]
        public virtual List<CarModelDetails> CarList { get; set; }
    }

    [SitecoreType(TemplateId = "{250B8620-3DA4-44D7-9955-091C92554E48}", AutoMap = true)]

    public class CarModelDetails
    {
        [SitecoreField("CarModelName")]
        public virtual string CarModelName { get; set; }

        [SitecoreField("CarModelCode")]
        public virtual string CarModelCode { get; set; }

        [SitecoreField("CarLogoImage")]
        public virtual Image CarLogoImage { get; set; }

        [SitecoreField("CarImage")]
        public virtual Image CarImage { get; set; }
    }

}