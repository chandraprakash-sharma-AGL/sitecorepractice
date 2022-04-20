using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.NexaWeb.Models
{
    public class AllStates
    {
        public string status { get; set; }
        public List<State_List> state_list { get; set; }
    }
    public class State_List
    {
        public object[] channels { get; set; }
        public int id { get; set; }
        public string msil_code { get; set; }
        public string state { get; set; }
        public object is_pan_required { get; set; }
    }

}