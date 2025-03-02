using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Controllers;
using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersManager usersManager;

        public UserController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        public ActionResult Index()
        {
            var userAccounts = usersManager.GetAll();
            return View(userAccounts);
        }

        public ActionResult Details(string name)
        {
            var userAccount = usersManager.TryGetByName(name);
            return View(userAccount);
        }

        public ActionResult ChangePassword(string name)
        {
            var changePassword = new ChangePassword()
            {
                UserName = name
            };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (changePassword.UserName == changePassword.Password)
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");
            }

            if (ModelState.IsValid)
            {
                usersManager.ChangePassword(changePassword.UserName, changePassword.Password);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
