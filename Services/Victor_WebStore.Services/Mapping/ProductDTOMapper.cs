using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Services.Mapping
{
    public static class ProductDTOMapper
    {
        public static ProductDTO ToDTO(this Product Product) => Product is null ? null : new ProductDTO
        {
            Brand = Product.Brand.ToDTO(),
            Category = Product.Category.ToDTO(),
            Id = Product.Id,
            ImageUrl = Product.ImageUrl,
            Name = Product.Name,
            Order = Product.Order,
            Price = Product.Price,
        };

        public static Product FromDTO(this ProductDTO Product) => Product is null ? null : new Product
        {
            Brand = Product.Brand.FromDTO(),
            Category = Product.Category.FromDTO(),
            Id = Product.Id,
            ImageUrl = Product.ImageUrl,
            Name = Product.Name,
            BrandId = Product.Brand?.Id,
            CategoryId = Product.Category.Id,
            Order = Product.Order,
            Price = Product.Price,
        };

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) => products.Select(ToDTO);
        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> products) => products.Select(FromDTO);
    }

}
