using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.Domain.Entities;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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
            {
                _logger.LogDebug("Входящяа модель не прошла валидацию");
                return View(model);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка входа");
                _logger.LogWarning("Ошибка входа пользователя {0}",model.UserName);
                return View(model);
            }
            _logger.LogInformation("Пользователь {0} вошел в систему",model.UserName);
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
            {
                _logger.LogDebug("Входящяа модель не прошла валидацию");
                return View(model);
            }

            var user = new User { UserName = model.UserName, Email = model.Email };
            var createResult = await _userManager.CreateAsync(user, model.Password);

            if (!createResult.Succeeded)
            {
                foreach (var item in createResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(model);
                }
                _logger.LogWarning("Не удалось зарегистрировать пользователя {0}, причины {1}",
                                   model.UserName,
                                   string.Join(" , ", createResult.Errors.Select(e => e.Description)));
                return View(model);
            }

            await _signInManager.SignInAsync(user, false);
            _logger.LogInformation("Пользователь {0} автоматически вошел в систему", user.UserName);
            IdentityResult result_addRole = await _userManager.AddToRoleAsync(user, "Users");

            if (!result_addRole.Succeeded)
            {
                _logger.LogWarning("Ошибка установки роли \"Users\" пользователю {0}, причина: {1}",
                    user.UserName,
                    string.Join(" , ", result_addRole.Errors.Select(errors => errors.Description)));
            }
            else 
            {
                _logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogDebug("Пользователь вышел из системы");
            return RedirectToAction("Index", "Home");
        }


    }

}
