using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private const string __PageSize = "PageSize";
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop(int? categoryId, int? brandId, int page = 1, int? pageSize = null)
        {
            var page_size = pageSize ?? (int.TryParse(_configuration[__PageSize], out var size) ? size : (int?)null);

            var products = _productService.GetProducts(
                new ProductFilter
                {
                    BrandId = brandId,
                    CategoryId = categoryId,
                    Page = page,
                    PageSize = page_size,
                });

            // сконвертируем в CatalogViewModel
            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.ProductsToPage.FromDTO().Select(p => p.ToViewModel()).OrderBy(p => p.Order).ToList(),
                PageViewModel = new PageViewModel
                {
                    Page = page,
                    PageSize = page_size ?? 0,
                    TotalItems = products.TotalCount,
                }
            };

            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            if (product is null)
                return RedirectToActionPermanent("NotFound404", "home");
            return View(product.FromDTO().ToViewModel());
        }

        #region AJAX_Metods
        public IActionResult GetItemsAJAX(int? categoryId, int? brandId, int page = 1, int? pageSize = null)
        {
            var tmp = GetPageProduct(categoryId, brandId, page, pageSize);
            return PartialView("Partial/_ProductItems", tmp);
        }

        private IEnumerable<ProductViewModel> GetPageProduct(int? categoryId, int? brandId, int page = 1, int? pageSize = null)
        {
            var page_size = pageSize ?? (int.TryParse(_configuration[__PageSize], out var size) ? size : (int?)null);

            var products = _productService.GetProducts(
                new ProductFilter
                {
                    BrandId = brandId,
                    CategoryId = categoryId,
                    Page = page,
                    PageSize = page_size,
                });

            return products.ProductsToPage.FromDTO().Select(p => p.ToViewModel()).OrderBy(p => p.Order).AsEnumerable();
        } 
        #endregion
    }
}
