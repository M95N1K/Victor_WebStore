using System.Collections.Generic;

namespace Victor_WebStore.Domain.ViewModels
{
    public class SelektableBrandViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}
