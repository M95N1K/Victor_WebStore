using System.Collections.Generic;
using System.Linq;

namespace Victor_WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; }

        public int ItemCount => Items?.Sum(x => x.Value) ?? 0;
        public decimal TotalPrice => Items?.Sum(x => x.Key.Price * x.Value) ?? 0;
    }
}
