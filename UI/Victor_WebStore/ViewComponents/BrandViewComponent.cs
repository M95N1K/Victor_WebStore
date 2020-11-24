using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BrandViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrand();
            return View(brands);
        }

        private List<BrandViewModel> GetBrand()
        {
            var products = _productService.GetProducts(null);
            var brand = _productService.GetBrands();
            var brandList = new List<BrandViewModel>();
            foreach (var item in brand)
            {
                brandList.Add(new BrandViewModel
                {
                    Id = item.Id,
                    Count = products.Count(p => p.Brand.Name == item.Name),
                    Name = item.Name,
                    Order = item.Order
                });
            }
            return brandList;
        }
    }
}
