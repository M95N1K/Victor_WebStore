using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities.Base;

namespace Victor_WebStore.Domain.Entities
{
    public class OrderItem: BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
