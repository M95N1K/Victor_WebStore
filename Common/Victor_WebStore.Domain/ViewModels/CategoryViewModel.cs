using System.Collections.Generic;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.ViewModels
{
    public class CategoryViewModel : INameEntity, IOrderEntity
    {
        public CategoryViewModel()
        {
            ChildCategories = new List<CategoryViewModel>();
        }

        public int Order { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        /// <summary>
        /// Дочерние секции
        /// </summary>
        public List<CategoryViewModel> ChildCategories { get; set; }

        /// <summary>
        /// Родительская секция
        /// </summary>
        public CategoryViewModel ParentCategory { get; set; }
    }
}
