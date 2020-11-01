using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Victor_WebStore.Infrastructure.Interfaces;
using Victor_WebStore.ViewModels;

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
                orderModels.Add(order.ToUserOrderViewModel());
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
