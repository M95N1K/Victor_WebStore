using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Controllers;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;
using Victor_WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace Victor_WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {

        [TestMethod]
        public void Product_Details_Returns_View_with_ProductDTO()
        {
            const int expected_price = 10;
            const int expected_id = 1;

            Mock<IProductService> _product_service_mock = new Mock<IProductService>();
            _product_service_mock
                .Setup(service => service.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Name = $"Product {id}",
                    Order = 1,
                    Price = expected_price,
                    Brand = new BrandDTO
                    {
                        Order = 1,
                        Id = 1,
                        Name = "Brand name",
                    },
                    Category = new CategoryDTO
                    {
                        Name = "Category name",
                        Id = 1,
                        Order = 1
                    }

                });

            CatalogController _controller = new CatalogController(_product_service_mock.Object);

            var result = _controller.ProductDetails(expected_id);

            var result_view = Assert.IsType<ViewResult>(result);
            var result_model = Assert.IsAssignableFrom<ProductViewModel>(result_view.Model);

            Assert.Equal(expected_id, result_model.Id);
            Assert.Equal(expected_price, result_model.Price);
        }

        [TestMethod]
        public void Shop_Return_CorrectView()
        {
            var products = new[]
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 1m,
                    Order = 0,
                    ImageUrl = "product1.png",
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = "Brand of product 1"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 1,
                        Name = "Category of product 1"
                    },
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 2m,
                    Order = 0,
                    ImageUrl = "product2.png",
                    Brand = new BrandDTO
                    {
                        Id = 2,
                        Name = "Brand of product 2"
                    },
                    Category = new CategoryDTO
                    {
                        Id = 2,
                        Name = "Category of product 2"
                    },
                },
            };

            var _product_service_mock = new Mock<IProductService>();
            _product_service_mock
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(products);

            const int expected_category_id = 1;
            const int expected_brand_id = 5;
            const int expected_product_count = 2;

            CatalogController _controller = new CatalogController(_product_service_mock.Object);

            var result = _controller.Shop(expected_category_id, expected_brand_id);

            var result_view = Assert.IsType<ViewResult>(result);
            var result_model = Assert.IsAssignableFrom<CatalogViewModel>(result_view.Model);

            Assert.Equal(expected_category_id, result_model.CategoryId);
            Assert.Equal(expected_brand_id, result_model.BrandId);
            Assert.Equal(expected_product_count, result_model.Products.Count());
        }
    }
}
