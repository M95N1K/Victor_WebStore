using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.ViewModels
{
    public class UserOrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalSum { get; set; }
    }

    public static class UserOrderExtension
    { 
        public static UserOrderViewModel ToUserOrderViewModel(this Order order)
        {
            UserOrderViewModel result = new UserOrderViewModel()
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                TotalSum = order.OrderItems.Sum(o => o.Price * o.Quantity),
                Date = order.Date
            };
            return result;
        }
    }

}
