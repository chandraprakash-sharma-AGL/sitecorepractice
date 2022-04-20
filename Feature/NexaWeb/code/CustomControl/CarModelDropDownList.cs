using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NexaDataAccess;
using Sitecore.Data.Items;
using Sitecore.ExperienceForms.Mvc.DataSource;
using Sitecore.ExperienceForms.Mvc.Models;

namespace Sitecore.Feature.NexaWeb.CustomControl
{
    public class CarModelDropDownList : IListDataSourceProvider
    {
        public IEnumerable<Item> GetDataItems(string dataSource)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListFieldItem> GetListItems(string dataSource, string displayFieldName, string valueFieldName, string defaultSelection)
        {
            try
            {
                return GetCarModelItems(defaultSelection);
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error:", ex, this);
                return new List<ListFieldItem>();
            }
        }
        protected IEnumerable<ListFieldItem> GetCarModelItems(string defaultSection)
        {
            List<ListFieldItem> items;

            using (NexaDataDBEntities nexaDataDBEntities = new NexaDataDBEntities())
            {
                var carModels = nexaDataDBEntities.nexaCarModels.ToList();
                items = new List<ListFieldItem>();
                foreach (var x in carModels)
                {
                    items.Add(new ListFieldItem { Value = x.ModelCode, Text = x.ModelName });
                }
            }

            return items;
        }
    }
}