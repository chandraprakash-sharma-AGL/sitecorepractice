using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.NexaWeb.Models
{
    public class state
    {
        public List<stateList> states { get; set; }
    }
    public class stateList
    {
        public int id { get; set; }
        public string stateName { get; set; }
        public string stateCode { get; set; }
    }
}