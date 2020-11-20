using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.DAL;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces;

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
        public Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.Address,
                    Name = orderModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Phone,
                    User = user
                };
                _context.Orders.Add(order);
                foreach (var item in transformCart.Items)
                {
                    var productVm = item.Key;
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(productVm.Id));
                    if (product == null)
                        throw new InvalidOperationException("Товар не найден в базе");
                    var orderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Value,
                        Order = order,
                        Product = product
                    };
                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();
                return order;
            }
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrder(int id)
        {
            return _context.OrderItems
                .Include(x => x.Product)
                //.Include(x => x.Product.Brand)
                .Include(x => x.Order)
                .Where(x => x.Order.Id == id);
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _context.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.User)
                .Where(x => x.User.UserName == userName);
        }
    }
}
