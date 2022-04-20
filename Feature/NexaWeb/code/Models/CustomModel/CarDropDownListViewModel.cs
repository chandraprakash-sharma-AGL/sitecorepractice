using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.DataSource;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using Sitecore.Feature.NexaWeb.CustomControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.NexaWeb.Models.CustomModel
{
    public class CarDropDownListViewModel :DropDownListViewModel
    {
        private IListDataSourceProvider listDataSourceProvider;
        protected override IListDataSourceProvider ListDataSourceProvider => listDataSourceProvider;
        protected override void InitItemProperties(Item item)
        {
            listDataSourceProvider = new CarModelDropDownList();
            base.InitItemProperties(item);
        }
    }
}