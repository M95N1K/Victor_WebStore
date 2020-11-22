using System.Collections.Generic;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        IEnumerable<OrderItem> GetOrderItemsByOrder(int id);

        Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName);
    }
}
