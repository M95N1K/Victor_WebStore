using System.Collections.Generic;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Interfaces.Services
{
    public interface IProductService
    {
        IEnumerable<CategoryDTO> GetCategories();
        IEnumerable<BrandDTO> GetBrands();
        PageProductDTO GetProducts(ProductFilter filter);
        ProductDTO GetProductById(int id);
        CategoryDTO GetCategoryById(int id);
        BrandDTO GetBrandById(int id);
    }
}
