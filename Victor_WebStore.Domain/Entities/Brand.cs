using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities.Base;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities
{
    public class Brand : NamedEntity, IOrderEntity, ICountEntity
    {
        public int Order { get; set; }
        public int Count { get; set; }
    }
}
