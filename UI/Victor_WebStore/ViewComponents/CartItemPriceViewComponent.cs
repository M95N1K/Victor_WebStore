using Microsoft.AspNetCore.Mvc;

namespace Victor_WebStore.ViewComponents
{
    public class CartItemPriceViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(decimal ItemPrice) => View(ItemPrice);
    }
}
