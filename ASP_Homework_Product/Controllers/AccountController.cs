using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Runtime.CompilerServices;

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
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Login));

            var userAccount = usersManager.TryGetByName(login.UserName);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "Такого пользователя не существует");
                return RedirectToAction(nameof(Login));
            }

            if (userAccount.Password != login.Password)
            {
                ModelState.AddModelError("", "Неправильный пароль");
                return RedirectToAction(nameof(Login));
            }

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (register.UserName == register.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать.");
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine($"Регистрируем пользователя: {register.UserName}");

                usersManager.Add(new UserAccount
                {
                    Name = register.UserName,
                    Password = register.Password,
                    Phone = register.Phone
                });

                Console.WriteLine($"После регистрации пользователей: {usersManager.GetAll().Count}");

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return RedirectToAction(nameof(Register));
        }

    }
}
