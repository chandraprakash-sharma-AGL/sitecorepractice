using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.NexaWeb.Models
{
    public class CarVariantsList
    {
        public string status { get; set; }
        public List<Car_Variant_List> car_variant_list { get; set; }
    }

    public class Car_Variant_List
    {
        public int id { get; set; }
        public int car_model_id { get; set; }
        public string msil_code { get; set; }
        public string car_variant { get; set; }
        public string fuel_type { get; set; }
    }
}