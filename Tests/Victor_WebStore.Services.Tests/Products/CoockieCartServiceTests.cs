﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace Victor_WebStore.Services.Tests.Products
{
    [TestClass]
    public class CoockieCartServiceTests
    {
        private Cart _cart;
        private Mock<IProductService> _productServiceMock;
        private ICartService _cartService;

        [TestInitialize]
        public void InitTest()
        {
            _cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1},
                    new CartItem { ProductId = 2, Quantity = 3},
                }
            };

            _productServiceMock = new Mock<IProductService>();
            _productServiceMock
                .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(new List<ProductDTO>
                {
                    new ()
                    { 
                        Id=1,
                        Name = "Product 1",
                        Brand = new BrandDTO { Id = 1,Name = "Brand 1"},
                        Category = new CategoryDTO { Id = 1, Name = "Category 1"},
                    },

                    new ()
                    {
                        Id=2,
                        Name = "Product 2",
                        Brand = new BrandDTO { Id = 2,Name = "Brand 2"},
                        Category = new CategoryDTO { Id = 2, Name = "Category 2"},
                    },

                });

        }

        [TestMethod]
        public void Cart_Class_ItemsCount_Correct_Quantity()
        {
            const int expected_item_count = 4;

            var actual_ItemCount = _cart.ItemsCount;

            Assert.Equal(expected_item_count, actual_ItemCount);
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
