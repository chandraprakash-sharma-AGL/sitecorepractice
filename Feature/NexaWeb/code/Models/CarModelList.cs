using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.NexaWeb.Models
{
    public class CarModelList
    {
        public string status { get; set; }
        public List<Car_Model_List> car_model_list { get; set; }
    }

    public class Car_Model_List
    {
        public object[] channels { get; set; }
        public int id { get; set; }
        public string msil_code { get; set; }
        public string car_model { get; set; }
    }



}



public class Rootobject
{
    public string status { get; set; }
    public Car_Model_List[] car_model_list { get; set; }
}

public class Car_Model_List
{
    public object[] channels { get; set; }
    public int id { get; set; }
    public string msil_code { get; set; }
    public string car_model { get; set; }
}
