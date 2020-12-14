using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.ViewComponents
{
    public class CartItemCountViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int CartItemCount) => View(CartItemCount);
    }
}
