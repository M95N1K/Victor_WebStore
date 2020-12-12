using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Victor_WebStore.ViewComponents
{
    public class TotalPriceViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(KeyValuePair<int, decimal> TotalPrice) => View(TotalPrice);
    }
}
