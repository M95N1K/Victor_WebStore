using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.ViewComponents
{
    public class BreadCrumbsViewComponent: ViewComponent
    {
        private readonly IProductService _productService;

        public BreadCrumbsViewComponent(IProductService ProductService) => _productService = ProductService;
        public IViewComponentResult Invoke()
        {
            

            var model = new BreadCrumbsViewModel();

            if(int.TryParse(Request.Query["categoryId"],out var category_id))
                model.Category = _productService.GetCategoryById(category_id).FromDTO();

            if (int.TryParse(Request.Query["brandId"], out var brand_id))
                model.Brand = _productService.GetBrandById(brand_id).FromDTO();

            if(int.TryParse(ViewContext.RouteData.Values["id"]?.ToString(), out var product_id) )
            {
                var product = _productService.GetProductById(product_id);
                if (product is not null)
                    model.Product = product.Name;
            }
            return View(model);
        }
    }
}
