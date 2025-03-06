using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Area("Admin")]
public class RoleController : Controller
{
    private readonly IRolesRepository rolesRes;

    public RoleController(IRolesRepository rolesRes)
    {
        this.rolesRes = rolesRes;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = rolesRes.GetAll();
        return Json(new { success = true, roles = roles.Select(r => new { r.Name }) });
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Role role)
    {
        if (rolesRes.TryGetByName(role.Name) != null)
        {
            return Json(new { success = false, errors = new[] { "Такая роль уже существует" } });
        }
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }
        rolesRes.Add(role);
        return Json(new { success = true });
    }

    [HttpPost]
    public IActionResult Remove(string roleName)
    {
        var role = rolesRes.TryGetByName(roleName);
        if (role == null) return Json(new { success = false, error = "Роль не найдена" });
        rolesRes.Remove(roleName);
        return Json(new { success = true });
    }
}