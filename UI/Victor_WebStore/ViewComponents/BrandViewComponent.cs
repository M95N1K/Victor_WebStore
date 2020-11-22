﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

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
            var brand = _productService.GetBrands();
            var brandList = new List<BrandViewModel>();
            foreach (var item in brand)
            {
                brandList.Add(new BrandViewModel
                {
                    Id = item.Id,
                    Count = item.Count,
                    Name = item.Name,
                    Order = item.Order
                });
            }
            return brandList;
        }
    }
}
