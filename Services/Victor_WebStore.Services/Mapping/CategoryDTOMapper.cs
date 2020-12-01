using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Services.Mapping
{
    public static class CategoryDTOMapper
    {
        public static CategoryDTO ToDTO(this Category Category) => Category is null ? null : new CategoryDTO
        {
            Id = Category.Id,
            Name = Category.Name,
            Order = Category.Order,
            ParentId = Category.ParentId,
        };

        public static Category FromDTO(this CategoryDTO Category) => Category is null ? null : new Category
        {
            Id = Category.Id,
            Name = Category.Name,
            Order = Category.Order,
            ParentId = Category.ParentId,
        };

        public static IEnumerable<CategoryDTO> ToDTO(this IEnumerable<Category> Categorys) => Categorys.Select(ToDTO);
        public static IEnumerable<Category> FromDTO(this IEnumerable<CategoryDTO> Categorys) => Categorys.Select(FromDTO);
    }

}
