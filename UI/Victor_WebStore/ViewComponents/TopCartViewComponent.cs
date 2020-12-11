using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.ViewComponents
{
    public class TopCartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public TopCartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke() => View(_cartService.TransformCart());
    }
}
