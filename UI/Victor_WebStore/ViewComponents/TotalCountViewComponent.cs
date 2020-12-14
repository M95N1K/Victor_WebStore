using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Victor_WebStore.ViewComponents
{
    public class TotalCountViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int TotalCount) => View(TotalCount);
    }
}
