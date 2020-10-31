using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.Infrastructure.Interfaces;

namespace Victor_WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admins")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var products = _productService.GetProducts(null).ToList();
            return View(products);
        }
    }
}
