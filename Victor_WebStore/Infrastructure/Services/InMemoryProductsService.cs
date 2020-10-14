using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Infrastructure.Interfaces;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Infrastructure.Services
{
    public class InMemoryProductsService: IProductsService
    {
        private readonly List<ProductsViewModel> _products = new List<ProductsViewModel>()
        {
            new ProductsViewModel()
            {
                Id = 1,
                Type = "Материнская плата",
                Manufacturer = "MSI",
                Model = "B450-A PRO MAX",
                Price = 7500.30
            },
            new ProductsViewModel()
            {
                Id = 2,
                Type = "Материнская плата",
                Manufacturer = "Biostar",
                Model = "H110MDE",
                Price = 3600.50
            }
        };

        public void AddNew(ProductsViewModel product)
        {
            if (_products.Count > 0) //Ищем максимальный Id если список не пуст
                product.Id = _products.Max(e => e.Id) + 1;
            else
                product.Id = 1;
            _products.Add(product);
        }

        public void Commit()
        {
            //nope
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product is null)
                return;

            _products.Remove(product);
        }

        public IEnumerable<ProductsViewModel> GetAll()
        {
            return _products;
        }

        public ProductsViewModel GetById(int id)
        {
            return _products.FirstOrDefault(e => e.Id.Equals(id));
        }
    }
}
