using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP_Homework_Product.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersManager usersManager;

        public AccountController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            var userAccount = usersManager.TryGetByName(login.UserName);
            if (userAccount == null)
            {
                return Json(new { success = false, errors = new[] { "Такого пользователя не существует" } });
            }

            if (userAccount.IsBlocked)
            {
                return Json(new { success = false, errors = new[] { "Ваш аккаунт заблокирован" } });
            }

            if (userAccount.Password != login.Password)
            {
                return Json(new { success = false, errors = new[] { "Неправильный пароль" } });
            }

            await AuthenticateUser(userAccount);
            return Json(new { success = true, redirectUrl = Url.Action(nameof(HomeController.Index), "Home") });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            var newUser = new UserAccount
            {
                Name = register.UserName,
                Password = register.Password,
                Phone = register.Phone
            };
            usersManager.Add(newUser);
            await AuthenticateUser(newUser);
            return Json(new { success = true, redirectUrl = Url.Action(nameof(HomeController.Index), "Home") });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(new { success = true, redirectUrl = Url.Action(nameof(Login), "Account") });
        }

        private async Task AuthenticateUser(UserAccount user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.MobilePhone, user.Phone ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
