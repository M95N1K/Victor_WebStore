using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain.ViewModels
{
    public class PageViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages =>PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
