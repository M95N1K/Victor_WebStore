using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Controllers
{
    public class SitemapController : Controller
    {
        public IActionResult Index([FromServices] IProductService product)
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index","Home")),
                new SitemapNode(Url.Action("Contact","Home")),
                new SitemapNode(Url.Action("Blog","Home")),
                new SitemapNode(Url.Action("BlogSingle","Home")),
                new SitemapNode(Url.Action("Shop","Catalog")),
                new SitemapNode(Url.Action("Index","WebApi")),
            };
            nodes.AddRange(product.GetCategories().Select(c => new SitemapNode(Url.Action("shop","catalog", new { categoryId = c.Id}))));

            foreach (var brand in product.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("shop","catalog", new { brandId = brand.Id})));

            foreach (var p in product.GetProducts(null).ProductsToPage)
            {
                nodes.Add(new SitemapNode(Url.Action("ProductDetails", "catalog", new { p.Id })));
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
