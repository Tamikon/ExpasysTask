using ASP_Homework_Product.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesRepository rolesRes;

        public RoleController(IRolesRepository rolesRes)
        {
            this.rolesRes = rolesRes;
        }

        public ActionResult Index()
        {
            var roles = rolesRes.GetAll();
            return View(roles);
        }

        public IActionResult Remove(string roleName)
        {
            rolesRes.Remove(roleName);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Role role)
        {
            if (rolesRes.TryGetByName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует.");
            }
            if (ModelState.IsValid)
            {
                rolesRes.Add(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
    }
}
