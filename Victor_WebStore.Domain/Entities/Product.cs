using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Victor_WebStore.Domain.Entities.Base;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities
{
    [Table("Products")]
    public class Product : NamedEntity, IOrderEntity
    {
        public int Order { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("BrandId")]
        [Display(Name = "Брэнд")]
        public virtual Brand Brand { get; set; }

    }
}
