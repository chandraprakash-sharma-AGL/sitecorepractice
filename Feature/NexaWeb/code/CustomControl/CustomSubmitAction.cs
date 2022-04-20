using System.Linq;
using Sitecore.Diagnostics;
using static System.FormattableString;
using Sitecore.ExperienceForms.Models;
using Sitecore.ExperienceForms.Processing;
using Sitecore.ExperienceForms.Processing.Actions;
using System.Web.Mvc;

namespace Sitecore.Feature.NexaWeb.SubmitActions
{
    public class CustomSubmitAction : SubmitActionBase<string>
    {
        public CustomSubmitAction(ISubmitActionData submitActionData) : base(submitActionData)
        {
        }
        protected override bool TryParse(string value, out string target)
        {
            target = string.Empty;
            return true;
        }

        [ValidateAntiForgeryToken]
        protected override bool Execute(string data, FormSubmitContext formSubmitContext)
        {
            Assert.ArgumentNotNull(formSubmitContext, nameof(formSubmitContext));

            if (!formSubmitContext.HasErrors)
            {
                Logger.Info(Invariant($"Form {formSubmitContext.FormId} submitted successfully."), this);
            }
            else
            {
                Logger.Warn(Invariant($"Form {formSubmitContext.FormId} submitted with errors: {string.Join(", ", formSubmitContext.Errors.Select(t => t.ErrorMessage))}."), this);
            }

            return true;
        }
     
    }
}