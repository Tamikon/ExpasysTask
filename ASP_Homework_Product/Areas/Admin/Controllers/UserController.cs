using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Area("Admin")]
public class UserController : Controller
{
    private readonly IUsersManager usersManager;

    public UserController(IUsersManager usersManager)
    {
        this.usersManager = usersManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var userAccounts = usersManager.GetAll();
        return Json(new { success = true, users = userAccounts.Select(u => new { u.Name, u.Phone }) });
    }

    public IActionResult Details(string name)
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetUserDetails(string name)
    {
        var user = usersManager.TryGetByName(name);
        if (user == null) return Json(new { success = false, error = "Пользователь не найден" });
        return Json(new { success = true, user = new { user.Name, user.Phone } });
    }

    public IActionResult ChangePassword(string name)
    {
        return View();
    }

    [HttpPost]
    public IActionResult ChangePassword(ChangePassword changePassword)
    {
        if (changePassword.UserName == changePassword.Password)
        {
            return Json(new { success = false, errors = new[] { "Логин и пароль не должны совпадать" } });
        }
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }
        usersManager.ChangePassword(changePassword.UserName, changePassword.Password);
        return Json(new { success = true });
    }

    public IActionResult EditRights(string name)
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetUserRights(string name)
    {
        var user = usersManager.TryGetByName(name);
        if (user == null) return Json(new { success = false, error = "Пользователь не найден" });
        return Json(new { success = true, user = new { user.Name, user.IsBlocked } });
    }

    [HttpPost]
    public IActionResult EditRights(UserAccount updatedUser)
    {
        var user = usersManager.TryGetByName(updatedUser.Name);
        if (user == null) return Json(new { success = false, error = "Пользователь не найден" });
        user.IsBlocked = updatedUser.IsBlocked;
        return Json(new { success = true });
    }
}