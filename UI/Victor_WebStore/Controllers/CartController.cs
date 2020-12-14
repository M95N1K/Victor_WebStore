using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Details()
        {
            var model = new OrderDetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);
        }

        public IActionResult DecrimentFromCart(int id)
        {
            _cartService.DecrimentFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult IncimentFromCart(int id)
        {
            _cartService.IncrimentFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Redirect(returnUrl);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Chekout(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                OrderDTO order = new OrderDTO
                {
                    Address = model.Address,
                    Name = model.Name,
                    Phone = model.Phone,
                };

                List<OrderItemDTO> items = _cartService.ToOrderItems().ToList();

                CreateOrderModel orderModel = new CreateOrderModel
                {
                    Items = items,
                    Order = order,
                };

                var orderResult = _orderService.CreateOrder(orderModel,
                    User.Identity.Name);
                _cartService.RemoveAll();
                return RedirectToAction("OrderConfirmed", new { id = orderResult.Id });
            }

            var detailsModel = new OrderDetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };
            return View("Details", detailsModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region AJAXMetods

        public IActionResult GetCartView() => ViewComponent("TopCart");

        public IActionResult GetCartItemCount(int id)
        {
            var item = _cartService.TransformCart().Items.FirstOrDefault(k => k.Key.Id == id);

            return ViewComponent("CartItemCount", item.Value);
        }

        public IActionResult GetCartItemPrice(int id)
        {
            var item = _cartService.TransformCart().Items.FirstOrDefault(k => k.Key.Id == id);

            return ViewComponent("CartItemPrice", item.Value * item.Key.Price);
        }

        public IActionResult GetTotalPriceView()
        {
            var items = _cartService.TransformCart();
            decimal price = items.TotalPrice;
            return ViewComponent("TotalPrice", price);
        }

        public IActionResult GetTotalCountView()
        {
            var items = _cartService.TransformCart();
            var count = items.ItemCount;
            return ViewComponent("TotalCount", count);
        }

        public IActionResult DecrimentFromCartAJAX(int id)
        {
            _cartService.DecrimentFromCart(id);
            return Ok();
            //return RedirectToAction("Details");
        }

        public IActionResult IncimentFromCartAJAX(int id)
        {
            _cartService.IncrimentFromCart(id);
            return Ok();
            //return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCartAJAX(int id)
        {
            _cartService.RemoveFromCart(id);
            return Ok();
            //return RedirectToAction("Details");
        }

        public IActionResult RemoveAllAJAX()
        {
            _cartService.RemoveAll();
            return Ok();
            //return RedirectToAction("Details");
        }

        public IActionResult AddToCartAJAX(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Ok();
            //return Redirect(returnUrl);
        }

        #endregion
    }
}
