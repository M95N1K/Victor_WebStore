using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victor_WebStore.Domain.ViewModels;
using Assert = Xunit.Assert;

namespace Victor_WebStore.Services.Tests.Products
{
    [TestClass]
    public class CoockieCartServiceTests
    {

        [TestMethod]
        public void Cart_Class_ItemsCount_Correct_Quantity()
        {
            const int expected_item_count = 4;
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1},
                    new CartItem { ProductId = 2, Quantity = 3},
                }
            };

            Assert.Equal(expected_item_count, cart.ItemsCount);
        }

        [TestMethod]
        public void CartViewModel_Correct_Data()
        {
            var item = new Dictionary<ProductViewModel, int>();
            item.Add(new ProductViewModel { Id = 1, Name = "Product 1", Price = 1 }, 1);
            item.Add(new ProductViewModel { Id = 2, Name = "Product 2", Price = 2 }, 3);

            var cartVM = new CartViewModel
            {
                Items = item,
            };

            const int expected_item_count = 4;
            const int expected_total_price = 7;

            var actual_itemcount = cartVM.ItemCount;
            var actual_totalPrice = cartVM.TotalPrice;

            Assert.Equal(expected_item_count, actual_itemcount);
            Assert.Equal(expected_total_price, actual_totalPrice);

        }


    }
}
