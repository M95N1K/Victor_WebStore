﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain.Entities
{
    public class ProductFilter
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public List<int> Ids { get; set; }
    }
}
