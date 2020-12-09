using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.ViewComponents
{
    public class FeaturesItemsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public FeaturesItemsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var allitems = GetItems();
            var items = new List<ProductViewModel>();
            Random random = new Random(DateTime.UtcNow.Millisecond);
            for (int i = 0; i < 6; i++)
            {
                ProductViewModel tmp;
                do
                {
                    tmp = allitems[random.Next(allitems.Count)];
                } while (items.FirstOrDefault(x => x.Id == tmp.Id) != null);
                items.Add(tmp);
            }
            //var items = GetItems();

            return View(items);
        }

        private List<ProductViewModel> GetItems()
        {
            var products = _productService.GetProducts(null);

            List<ProductViewModel> result = new List<ProductViewModel>();

            foreach (var item in products)
            {
                result.Add(new ProductViewModel
                {
                    Brand = item.Brand.Name,
                    Id = item.Id,
                    ImageUrl = item.ImageUrl,
                    Name = item.Name,
                    Order = item.Order,
                    Price = item.Price
                });
            }

            return result;
        }
    }
}
