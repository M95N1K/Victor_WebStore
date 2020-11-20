using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.ViewModels
{
    public class BrandViewModel : INameEntity, ICountEntity, IOrderEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
        public int Order { get; set; }
    }
}
