using System;
using System.Collections.Generic;
using System.Text;

namespace Victor_WebStore.Domain.ViewModels
{
    public class SelectableCategoruViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public int? CurrentCategory { get; set; }

        public int? ParentCategory { get; set; }
    }
}
