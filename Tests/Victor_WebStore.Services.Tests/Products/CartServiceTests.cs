using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Victor_WebStore.Domain.DTO.Order;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace Victor_WebStore.Services.Tests.Products
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _cart;
        private Mock<IProductService> _productServiceMock;
        private Mock<ICartStore> _cartStoreMock;
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
                        Price = 1,
                        Brand = new BrandDTO { Id = 1,Name = "Brand 1"},
                        Category = new CategoryDTO { Id = 1, Name = "Category 1"},
                    },

                    new ()
                    {
                        Id=2,
                        Name = "Product 2",
                        Price = 2,
                        Brand = new BrandDTO { Id = 2,Name = "Brand 2"},
                        Category = new CategoryDTO { Id = 2, Name = "Category 2"},
                    },

                });

            _cartStoreMock = new Mock<ICartStore>();
            _cartStoreMock.Setup(c => c.Cart).Returns(_cart);

            _cartService = new CartService(_productServiceMock.Object,_cartStoreMock.Object);
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

        [TestMethod]
        public void CartService_AddToCart_WorkCorrect()
        {
            _cart.Items.Clear();

            const int expected_id = 4;
            const int expected_items_count = 1;

            _cartService.AddToCart(expected_id);

            Assert.Equal(expected_items_count, _cart.ItemsCount);
            Assert.Single(_cart.Items);
            Assert.Equal(expected_id, _cart.Items.First().ProductId);
        }

        [TestMethod]
        public void CartService_DecrimentFromCart_WorkCorrect()
        {
            const int expected_id = 2;
            const int expected_quantity = 2;

            _cartService.DecrimentFromCart(expected_id);

            var actual_quantity = _cart.Items.FirstOrDefault(c => c.ProductId == expected_id).Quantity;

            Assert.Equal(expected_quantity,actual_quantity);
        }

        [TestMethod]
        public void CartService_IncrimentFromCart_WorkCorrect()
        {
            const int expected_id = 1;
            const int expected_quantity = 2;

            _cartService.IncrimentFromCart(expected_id);

            var actual_quantity = _cart.Items.FirstOrDefault(c => c.ProductId == expected_id).Quantity;

            Assert.Equal(expected_quantity, actual_quantity);
        }

        [TestMethod]
        public void CartService_RemoveAll_WorkCorrect()
        {
            const int expected_items_count = 0;

            _cartService.RemoveAll();

            var actual_items_count = _cart.Items.Count;

            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_WorkCorrect()
        {
            const int expected_items_count = 1;
            const int expected_id = 2;
            const int remove_id = 1;

            _cartService.RemoveFromCart(remove_id);

            var actual_items_count = _cart.Items.Count;
            var actual_id = _cart.Items.First().ProductId;

            Assert.Equal(expected_items_count, actual_items_count);
            Assert.Equal(expected_id, actual_id);
        }

        [TestMethod]
        public void CartService_TransformCart_Returns_CartViewModel()
        {
            const int expected_items_count = 2;

            var result = _cartService.TransformCart();
            var actual_items_count = result.Items.Count;
            Assert.IsType<CartViewModel>(result);
            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartService_ToOrderItems_Returns_IEnumerableOrderItemDTO()
        {
            const int expected_items_count = 2;

            var result = _cartService.ToOrderItems();
            var actual_items_count = result.Count();

            Assert.IsType<List<OrderItemDTO>>(result.ToList());
            Assert.Equal(expected_items_count, actual_items_count);
        }

        [TestMethod]
        public void CartService_SetQuantityFromCart_Work()
        {
            const int expected_id = 1;
            const int expected_quantity = 2;

            _cartService.SetQuantityFromCart(expected_id, expected_quantity);

            var actual_quantity = _cart.Items.FirstOrDefault(c => c.ProductId == expected_id).Quantity;

            Assert.Equal(expected_quantity, actual_quantity);
        }
    }
}
