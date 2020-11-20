using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginUserViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка входа");
                return View(model);
            }

            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User { UserName = model.UserName, Email = model.Email };
            var createResult = await _userManager.CreateAsync(user, model.Password);

            if (!createResult.Succeeded)
            {
                foreach (var item in createResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(model);
                }
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, "Users");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }

}
