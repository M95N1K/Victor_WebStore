using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities.Base;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities
{
    public class Brand : NamedEntity, IOrderEdinty
    {
        public int Order { get; set; }
    }
}
