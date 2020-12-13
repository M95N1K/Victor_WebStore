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

        public IViewComponentResult Invoke(string BrandId)
        {
            var brands = GetBrand();
            var brand_id = int.TryParse(BrandId, out var id) ? id : (int?)null;
            return View(new SelektableBrandViewModel 
            {
                Brands = brands,
                CurrentBrandId = brand_id,
            });
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
                    Count = products.ProductsToPage.Count(p => p.Brand.Name == item.Name),
                    Name = item.Name,
                    Order = item.Order
                });
            }
            return brandList;
        }
    }
}
