using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Controllers
{
    public class ProductsController : Controller
    {
        List<ProductsViewModel> _products = new List<ProductsViewModel>()
        {
            new ProductsViewModel()
            {
                Id = 1,
                Type = "Материнская плата",
                Manufacturer = "MSI",
                Model = "B450-A PRO MAX",
                Price = 7500.30
            },
            new ProductsViewModel()
            {
                Id = 2,
                Type = "Материнская плата",
                Manufacturer = "Biostar",
                Model = "H110MDE",
                Price = 3600.50
            }
        };
        public IActionResult All()
        {
            return View(_products);
            //return Content("Content");
        }
        public IActionResult ProductDetails(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }
    }
}
