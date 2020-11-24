using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Victor_WebStore.Clients.Base;
using Victor_WebStore.Domain;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Interfaces.Services;

namespace Victor_WebStore.Clients.Order
{
    public class OrderClients : BaseClient, IOrderService
    {
        public OrderClients(IConfiguration Configuration) : base(Configuration, WebApiAddress.Orders)
        {
        }

        public OrderDTO CreateOrder(CreateOrderModel orderModel, string userName)
        {
            return Post($"{_ServiceAddress}/{userName}", orderModel).Content.ReadAsAsync<OrderDTO>().Result;
        }

        public IEnumerable<OrderItemDTO> GetOrderItemsByOrder(int id)
        {
            return Get<IEnumerable<OrderItemDTO>>($"{_ServiceAddress}/{id}");
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            return Get<IEnumerable<OrderDTO>>($"{_ServiceAddress}/user/{userName}");
        }
    }
}
