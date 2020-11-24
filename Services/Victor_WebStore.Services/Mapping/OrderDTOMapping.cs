using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Services.Mapping
{
    public static class OrderDTOMapping
    {
        public static OrderDTO ToDTO(this Order order) => order is null ? null : new OrderDTO
        { 
            Address=order.Address,
            Date = order.Date,
            Name = order.Name,
            Id = order.Id,
            Phone = order.Phone,
            Items = order.OrderItems.Select(ToDTO),
        };

        public static OrderItemDTO ToDTO(this OrderItem item) => item is null ? null : new OrderItemDTO
        {
            Id = item.Id,
            OrderId = item.Order.Id,
            Price = item.Price,
            Product = item.Product.ToDTO(),
            Quantity = item.Quantity,
        };

        public static Order FromDTO(this OrderDTO order) => order is null ? null : new Order
        {
            Address = order.Address,
            Date = order.Date,
            Name = order.Name,
            Id = order.Id,
            Phone = order.Phone,
            OrderItems = order.Items.Select(FromDTO),
        };

        public static OrderItem FromDTO(this OrderItemDTO item) => item is null ? null : new OrderItem
        {
            Id = item.Id,
            Order = new Order {Id = item.OrderId },
            Price = item.Price,
            Product = item.Product.FromDTO(),
            Quantity = item.Quantity,
        };

    }
}
