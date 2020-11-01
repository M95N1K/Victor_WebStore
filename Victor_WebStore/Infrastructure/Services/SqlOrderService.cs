using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.DAL;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Infrastructure.Interfaces;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Infrastructure.Services
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreContext webStoreContext, UserManager<User> userManager)
        {
            _webStoreContext = webStoreContext;
            _userManager = userManager;
        }
        public Order CreateOrder(OrderViewModel orderModel, CartViewModel transformCart, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _webStoreContext.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.Address,
                    Name = orderModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.Phone,
                    User = user
                };
                _webStoreContext.Orders.Add(order);
                foreach (var item in transformCart.Items)
                {
                    var productVm = item.Key;
                    var product = _webStoreContext.Products.FirstOrDefault(p => p.Id.Equals(productVm.Id));
                    if (product == null)
                        throw new InvalidOperationException("Товар не найден в базе");
                    var orderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Value,
                        Order = order,
                        Product = product
                    };
                    _webStoreContext.OrderItems.Add(orderItem);
                }

                _webStoreContext.SaveChanges();
                transaction.Commit();
                return order;
            }
        }

        public Order GetOrderById(int id)
        {
            return _webStoreContext.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _webStoreContext.Orders
                .Include(x => x.OrderItems)
                .Include(x => x.User)
                .Where(x => x.User.UserName == userName);
        }
    }
}
