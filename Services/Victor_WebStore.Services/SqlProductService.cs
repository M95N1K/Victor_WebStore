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

        public BrandDTO GetBrandById(int id) => _context.Brands.FirstOrDefault(d => d.Id == id).ToDTO();

        public IEnumerable<BrandDTO> GetBrands()
        {
            return _context.Brands.ToDTO().ToList();
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _context.Categories.ToDTO().ToList();
        }

        public CategoryDTO GetCategoryById(int id) => _context.Categories.FirstOrDefault(d => d.Id == id).ToDTO();

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

        public PageProductDTO GetProducts(ProductFilter filter)
        {
            var listProduct = _context.Products
                .Include(b => b.Brand)
                .Include(c => c.Category)
                .AsQueryable();
            
                if (filter?.BrandId != null)
                    listProduct = listProduct.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
                if (filter?.CategoryId != null)
                    listProduct = listProduct.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));
                var total_count = listProduct.Count();
                if (filter?.PageSize > 0)
                    listProduct = listProduct
                        .OrderBy(p => p.Order)
                        .Skip((filter.Page - 1) * (int)filter.PageSize)
                        .Take((int)filter.PageSize);

            return new PageProductDTO
            {
                ProductsToPage = listProduct.ToDTO().AsEnumerable(),
                TotalCount  = total_count,
            };
             
        }
    }
}
