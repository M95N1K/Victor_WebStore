using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Victor_WebStore.Domain.Entities.Base.Interfaces;

namespace Victor_WebStore.Domain.Entities.Base
{
    public abstract class NamedEntity : INameEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name ="Имя")]
        public string Name { get; set; }
    }
}
