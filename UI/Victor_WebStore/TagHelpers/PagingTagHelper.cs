using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageViewModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");


            for (int i = 1, total_count = PageViewModel.TotalPages; i <= total_count; i++)
            {
                ul.InnerHtml.AppendHtml(CreateElement(i));
            }

            output.Content.AppendHtml(ul);
        }

        private TagBuilder CreateElement(int PageNumber)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            if (PageNumber == PageViewModel.Page)
            {
                a.MergeAttribute("data-page", PageViewModel.Page.ToString());
                li.AddCssClass("active");
                foreach (var (key, value) in PageUrlValues.Where(v => v.Value != null))
                {
                    a.MergeAttribute($"data-{key}", value.ToString());
                }
            }
            else
            {
                PageUrlValues["page"] = PageNumber;
                a.Attributes["href"] = "#";
                foreach (var (key,value) in PageUrlValues.Where(v => v.Value != null))
                {
                    a.MergeAttribute($"data-{key}", value.ToString());
                }
            }
            a.InnerHtml.AppendHtml(PageNumber.ToString());
            li.InnerHtml.AppendHtml(a);
            return li;
        }
    }
}
