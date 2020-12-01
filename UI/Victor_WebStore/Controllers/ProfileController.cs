using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOrderService _orderService;

        public ProfileController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = _orderService.GetUserOrders(User.Identity.Name);

            var orderModels = new List<UserOrderViewModel>(orders.Count());

            foreach (var order in orders)
            {
                orderModels.Add(order.FromDTO().ToUserOrderViewModel());
            }

            return View(orderModels);
        }

        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderItemsByOrder(id);
            return View(order);
        }


    }
}
