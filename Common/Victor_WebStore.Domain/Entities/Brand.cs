using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Victor_WebStore.Domain.Entities.Base;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities
{
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderEntity, ICountEntity
    {
        public int Order { get; set; }
        public int Count { get; set; }
    }
}
