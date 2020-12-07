using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using Victor_WebStore.Controllers;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace Victor_WebStore.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public void Chekout_ModelState_Invalid_Returns_ViewModel()
        {
            const int expected_count_product = 3;
            const int expected_count_cart_item = 6;

            CartViewModel cartViewModel = new CartViewModel
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    {new ProductViewModel { Id = 1, Name = "Product 1",Price = 1,},1 },
                    {new ProductViewModel { Id = 2, Name = "Product 2",Price = 2,},2 },
                    {new ProductViewModel { Id = 3, Name = "Product 3",Price = 3,},3 },
                },
            };

            OrderViewModel orderViewModel = new OrderViewModel
            {
                Name = "Name",
                Address = "Address",
                Phone = "InValid",
            };

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel
            {
                CartViewModel = cartViewModel,
                OrderViewModel = orderViewModel,
            };

            Mock<ICartService> cart_service_mock = new Mock<ICartService>();
            cart_service_mock
                .Setup(c => c.TransformCart())
                .Returns(cartViewModel);

            Mock<IOrderService> order_service_mock = new Mock<IOrderService>();

            CartController controller = new CartController(cart_service_mock.Object, order_service_mock.Object);
            controller.ModelState.AddModelError("Error", "Model Invalid");

            var result = controller.Chekout(orderViewModel);

            var view_result = Assert.IsType<ViewResult>(result);
            var model_result = Assert.IsAssignableFrom<OrderDetailsViewModel>(view_result.Model);
            Assert.Equal(orderViewModel.Phone, model_result.OrderViewModel.Phone);
            Assert.Equal(expected_count_cart_item, model_result.CartViewModel.ItemCount);
            Assert.Equal(expected_count_product, model_result.CartViewModel.Items.Count);
        }

        [TestMethod]
        public void Checkout_ModelState_Valid_Returns_RedirectToAction()
        {
            const string expected_action = "OrderConfirmed";
            const int expected_id = 1;
            
            IEnumerable<OrderItemDTO> items = new List<OrderItemDTO>
            {
                new OrderItemDTO { Id = 1, Price = 1, Quantity = 1, Product = new ProductDTO{ Id = 1,Name="Prodict 1",Price = 1} },
                new OrderItemDTO { Id = 2, Price = 4, Quantity = 2, Product = new ProductDTO{ Id = 2,Name="Prodict 2",Price = 2} },
                new OrderItemDTO { Id = 3, Price = 9, Quantity = 3, Product = new ProductDTO{ Id = 3,Name="Prodict 3",Price = 3} },
            };
            OrderViewModel orderView = new OrderViewModel
            {
                Address = "Address",
                Name = "Name",
                Phone = "Valid",
            };

            Mock<ICartService> cart_service_mock = new Mock<ICartService>();
            cart_service_mock
                .Setup(cs => cs.ToOrderItems())
                .Returns(items);

            Mock<IOrderService> order_service_mock = new Mock<IOrderService>();
            order_service_mock
                .Setup(os => os.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
                .Returns(new OrderDTO 
                { 
                    Id = expected_id
                });

            CartController controller = new CartController(cart_service_mock.Object, order_service_mock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "TestUser") }))
                    }
                }
            };


            var result = controller.Chekout(orderView);

            var redirect_result = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(expected_action, redirect_result.ActionName);
            Assert.Null(redirect_result.ControllerName);
            int param = (int)redirect_result.RouteValues["id"];
            Assert.Equal(expected_id, param);
        }
    }

}
