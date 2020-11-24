using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.ServicesHost.Controllers
{
    [Route(WebApiAddress.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        #region Конструктор
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService OrderService)
        {
            _orderService = OrderService;
        } 
        #endregion

        [HttpPost("{userName}")]
        public OrderDTO CreateOrder([FromBody]CreateOrderModel orderModel, string userName)
        {
            return _orderService.CreateOrder(orderModel, userName);
        }

        [HttpGet("{id}")]
        public IEnumerable<OrderItemDTO> GetOrderItemsByOrder(int id)
        {
            return _orderService.GetOrderItemsByOrder(id);
        }

        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            return _orderService.GetUserOrders(userName);
        }
    }
}
