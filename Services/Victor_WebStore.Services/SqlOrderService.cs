using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.DAL;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.Services
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreContext webStoreContext, UserManager<User> userManager)
        {
            _context = webStoreContext;
            _userManager = userManager;
        }
        public OrderDTO CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.Order.Address,
                    Name = orderModel.Order.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Order.Phone,
                    User = user
                };
                _context.Orders.Add(order);
                foreach (var item in orderModel.Items)
                {
                    var productVm = item.Product;
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(productVm.Id));
                    if (product == null)
                        throw new InvalidOperationException("Товар не найден в базе");
                    var orderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Order = order,
                        Product = product
                    };
                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();
                return order.ToDTO();
            }
        }

        public IEnumerable<OrderItemDTO> GetOrderItemsByOrder(int id)
        {
            var result = _context.OrderItems
                .Include(x => x.Product)
                //.Include(x => x.Product.Brand)
                .Include(x => x.Order)
                .Where(x => x.Order.Id == id);
            return result.Select(p=>p.ToDTO());
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            var result = _context.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.User)
                .Where(x => x.User.UserName == userName).ToList();

            return result.Select(p=>p.ToDTO());
        }
    }
}
