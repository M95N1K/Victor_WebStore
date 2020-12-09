using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Victor_WebStore.DAL;
using Victor_WebStore.Domain.DTO.Products;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Interfaces.Services;
using Victor_WebStore.Services.Mapping;

namespace Victor_WebStore.Services
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            return _context.Brands.ToDTO().ToList();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _context.Categories.ToDTO().ToList();
        }

        public ProductDTO GetProductById(int id)
        {
            var result = _context.Products
                .Include(p => p.Brand)
                .Include(c => c.Category)
                .FirstOrDefault(x => x.Id == id);
            if (result is null)
                return null;
            return result.ToDTO();
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter)
        {
            var listProduct = _context.Products
                .Include(b => b.Brand)
                .Include(c => c.Category)
                .AsQueryable();
            if (filter != null)
            {
                if (filter.BrandId.HasValue)
                    listProduct = listProduct.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
                if (filter.CategoryId.HasValue)
                    listProduct = listProduct.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));
            }
            return listProduct.ToDTO().ToList();
        }
    }
}
