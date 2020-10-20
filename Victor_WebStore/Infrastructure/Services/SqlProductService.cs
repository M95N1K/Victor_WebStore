using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.DAL;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Infrastructure.Interfaces;

namespace Victor_WebStore.Infrastructure.Services
{
    public class SqlProductService : IProductService
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var listProduct = _context.Products.AsQueryable();
            if (filter.BrandId.HasValue)
                listProduct = listProduct.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                listProduct = listProduct.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));

            return listProduct.ToList();
        }
    }
}
