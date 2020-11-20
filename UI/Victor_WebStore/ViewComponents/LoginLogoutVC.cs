using Microsoft.AspNetCore.Mvc;

namespace Victor_WebStore.ViewComponents
{
    public class LoginLogoutVC : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
