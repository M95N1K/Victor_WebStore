using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Victor_WebStore.ViewComponents
{
    public class LoginLogoutVC: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
