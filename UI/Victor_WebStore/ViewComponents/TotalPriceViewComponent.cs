using Microsoft.AspNetCore.Mvc;

namespace Victor_WebStore.ViewComponents
{
    public class TotalPriceViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(decimal TotalPrice) => View(TotalPrice);
    }
}
