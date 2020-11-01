using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        IEnumerable<OrderItem> GetOrderItemsByOrder(int id);

        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    }
}
