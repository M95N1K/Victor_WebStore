using System;
using System.Collections.Generic;
using System.Text;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart{ get; set; }
    }
}
