using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Area("Admin")]
public class RoleController : Controller
{
    private readonly IRolesRepository _rolesRepository;

    public RoleController(IRolesRepository rolesRepository)
    {
        this._rolesRepository = rolesRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = _rolesRepository.GetAll();
        return Json(new { success = true, roles = roles.Select(r => new { r.Name }) });
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Role role)
    {
        if (_rolesRepository.TryGetByName(role.Name) != null)
        {
            return Json(new { success = false, errors = new[] { "Такая роль уже существует" } });
        }
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }
        _rolesRepository.Add(role);
        return Json(new { success = true });
    }

    [HttpPost]
    public IActionResult Remove(string roleName)
    {
        var role = _rolesRepository.TryGetByName(roleName);
        if (role == null) return Json(new { success = false, error = "Роль не найдена" });
        _rolesRepository.Remove(roleName);
        return Json(new { success = true });
    }
}