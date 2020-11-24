using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Services.Mapping
{
    public static class BrandDTOMapper
    {
        public static BrandDTO ToDTO(this Brand Brand) => Brand is null ? null : new BrandDTO
        {
            Id = Brand.Id,
            Name = Brand.Name,
            Order = Brand.Order,
            Count = Brand.Count,
        };

        public static Brand FromDTO(this BrandDTO Brand) => Brand is null ? null : new Brand
        {
            Id = Brand.Id,
            Name = Brand.Name,
            Order = Brand.Order,
            Count = Brand.Count,
        };

        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> Brands) => Brands.Select(ToDTO);
        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> Brands) => Brands.Select(FromDTO);
    }

}
