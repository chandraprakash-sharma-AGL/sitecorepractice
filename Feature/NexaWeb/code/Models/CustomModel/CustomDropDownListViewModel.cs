using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.ExperienceForms.Mvc.Models.Fields;
using System;
using Sitecore.Feature.NexaWeb.Constants;
namespace Sitecore.Feature.NexaWeb.Models.CustomModel
{
    [Serializable]
    public class CustomDropDownListViewModel : DropDownListViewModel
    {
        public string CustomeDropDownListFieldName { get; set; }
        public string CustomeDropDownListLabel { get; set; }
        public string CustomeDropDownListTextField { get; set; }
        public string CustomeDropDownListValueField { get; set; }

        protected override void InitializeValidations(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            base.InitializeValidations(item);
            CustomeDropDownListFieldName = item?.Fields[CustomeConstants.CustomeDropDownListFieldName]?.Value;
                CustomeDropDownListLabel = item?.Fields[CustomeConstants.CustomeDropDownListLabel]?.Value;
            CustomeDropDownListTextField = item?.Fields[CustomeConstants.CustomeDropDownListTextField]?.Value;
            CustomeDropDownListValueField = item?.Fields[CustomeConstants.CustomeDropDownListValueField]?.Value;
        }
        protected override void UpdateItemFields(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            base.UpdateItemFields(item);
            var customeDropDownListFieldName = item.Fields[CustomeConstants.CustomeDropDownListFieldName];
            if(customeDropDownListFieldName != null)
            {
                customeDropDownListFieldName.SetValue(CustomeDropDownListFieldName, true);

            }
            var customeDropDownListLabel = item.Fields[CustomeConstants.CustomeDropDownListLabel];
            if (customeDropDownListLabel != null)
            {
                customeDropDownListLabel.SetValue(CustomeDropDownListLabel, true);

            }
            var customeDropDownListTextField = item.Fields[CustomeConstants.CustomeDropDownListTextField];
            if (customeDropDownListTextField != null)
            {
                customeDropDownListTextField.SetValue(CustomeDropDownListTextField, true);

            }
            var customeDropDownListValueField = item.Fields[CustomeConstants.CustomeDropDownListValueField];
            if (customeDropDownListValueField != null)
            {
                customeDropDownListValueField.SetValue(CustomeDropDownListValueField, true);

            }
        }
    }
}