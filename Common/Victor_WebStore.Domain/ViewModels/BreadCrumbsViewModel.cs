using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.Entities;

namespace Victor_WebStore.Domain.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public string Product { get; set; }
    }
}
