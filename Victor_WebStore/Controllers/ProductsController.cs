using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.Infrastructure.Interfaces;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [Route("all")]
        public IActionResult All()
        {
            return View(_productsService.GetAll());
        }
        [Route("{id?}")]
        public IActionResult ProductDetails(int id)
        {
            var product = _productsService.GetById(id);
            if (product == null)
                return RedirectToAction("NotFound404","Home");
            return View(product);
        }

        [HttpGet]
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new ProductsViewModel());

            var model = _productsService.GetById(id.Value);
            if (model == null)
                return RedirectToAction("NotFound404", "Home");// возвращаем результат 404 Not Found

            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(ProductsViewModel product)
        {
            if (product.Id > 0)
            {
                var item = _productsService.GetById(product.Id);
                if (ReferenceEquals(item, null))
                    return RedirectToAction("NotFound404", "Home");

                item.Manufacturer = product.Manufacturer;
                item.Model = product.Model;
                item.Price = product.Price;
                item.Type = product.Type;
            }
            else
            {
                _productsService.AddNew(product);
            }

            _productsService.Commit();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int Id)
        {
            //_productsService.Delete(Id);
            //return RedirectToAction(nameof(All));
            return View(_productsService.GetById(Id));
        }

        [HttpPost]
        [Route("delete/{id?}")]
        public IActionResult Delete(ProductsViewModel model)
        {
            _productsService.Delete(model.Id);
            return RedirectToAction(nameof(All));
        }

    }
}
