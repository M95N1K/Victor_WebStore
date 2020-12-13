using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageViewModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        public PagingTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            var url_helper = _urlHelperFactory.GetUrlHelper(ViewContext);

            for (int i = 1, total_count = PageViewModel.TotalPages; i <= total_count; i++)
            {
                ul.InnerHtml.AppendHtml(CreateElement(i, url_helper));
            }

            output.Content.AppendHtml(ul);
        }

        private TagBuilder CreateElement(int PageNumber, IUrlHelper url_helper)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            if (PageNumber == PageViewModel.Page)
                li.AddCssClass("active");
            else
            {
                PageUrlValues["page"] = PageNumber;
                a.Attributes["href"] = url_helper.Action(PageAction, PageUrlValues);
            }
            a.InnerHtml.AppendHtml(PageNumber.ToString());
            li.InnerHtml.AppendHtml(a);
            return li;
        }
    }
}
