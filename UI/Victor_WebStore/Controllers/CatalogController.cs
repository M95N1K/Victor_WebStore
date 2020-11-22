using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            return View(product.ToViewModel());
        }

        public IActionResult Shop(int? categoryId, int? brandId)
        {
            var products = _productService.GetProducts(
                new ProductFilter { BrandId = brandId, CategoryId = categoryId });

            // сконвертируем в CatalogViewModel
            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(p => p.ToViewModel()).OrderBy(p => p.Order).ToList()
            };

            return View(model);
        }
    }
}
