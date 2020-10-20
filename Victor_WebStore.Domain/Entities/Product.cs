using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities.Base
{
    public class Product : NamedEntity, IOrderEtinty
    {
        public int Order { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        //public string Manufacturer { get; set; }

    }
}
