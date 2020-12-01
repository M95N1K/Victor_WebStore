using System.Collections.Generic;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string userName);

        IEnumerable<OrderItemDTO> GetOrderItemsByOrder(int id);

        OrderDTO CreateOrder(CreateOrderModel orderModel,  string userName);
    }
}
