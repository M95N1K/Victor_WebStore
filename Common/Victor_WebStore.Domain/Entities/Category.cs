using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Victor_WebStore.Domain.Entities.Base;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities
{
    [Table("Categories")]
    public class Category : NamedEntity, IOrderEntity
    {
        /// <summary>
        /// Родительская секция (при наличии)
        /// </summary>
        public int? ParentId { get; set; }

        public int Order { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
