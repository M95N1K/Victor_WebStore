using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.ViewModels
{
    public class ProductViewModel : INameEntity, IOrderEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }

    public static class ProductExtension
    {
        public static ProductViewModel ToViewModel(this Product product)
        {
            ProductViewModel result = new ProductViewModel()
            {
                Brand = product.Brand != null ? product.Brand.Name : string.Empty,
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Order = product.Order,
                Price = product.Price
            };

            return result;
        }
    }

}
