using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain.DTO.Products
{
    public class PageProductDTO
    {
        public IEnumerable<ProductDTO> ProductsToPage { get; set; }
        public int TotalCount { get; set; }
    }
}
