using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Victor_WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = AtributName)]
    public class ActiveRouteTagHelper : TagHelper
    {
        private const string AtributName = "vws-is-active-route";
        private const string IgnoreAction = "vws-ignore-acrion";

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignore_action = output.Attributes.ContainsName(IgnoreAction);
            if (IsActive(ignore_action))
                MakeActive(output);
            output.Attributes.RemoveAll(AtributName);
            output.Attributes.RemoveAll(IgnoreAction);
        }

        private bool IsActive(bool ignore_action)
        {
            var route_values = ViewContext.RouteData.Values;

            var current_controller = route_values["controller"].ToString();
            var current_action = route_values["action"].ToString();

            if (!string.IsNullOrEmpty(Controller) && !string.Equals(current_controller, Controller, StringComparison.OrdinalIgnoreCase))
                return false;
            if (!ignore_action && !string.IsNullOrEmpty(Action) && !string.Equals(current_action, Action, StringComparison.OrdinalIgnoreCase))
                return false;

            foreach (var (key, value) in RouteValues)
                if (!route_values.ContainsKey(key) || route_values[key].ToString() != value)
                    return false;

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var class_attribute = output.Attributes.FirstOrDefault(attr => attr.Name == "class");
            if (class_attribute is null)
                output.Attributes.Add("class", "active");
            else
            {
                if (class_attribute.Value.ToString()?.Contains("active") ?? false)
                    return;
                output.Attributes.SetAttribute("class", class_attribute.Value + " active");
            }
            
        }
    }

    [HtmlTargetElement(Attributes = IgnoreAction)]
    public class IgnoreActionTagHelper : TagHelper
    {
        private const string IgnoreAction = "vws-ignore-acrion";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(IgnoreAction);
        }
    }
}
