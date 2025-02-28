using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesRes rolesRes;

        public RoleController(IRolesRes rolesRes)
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
            return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return View(role);
        }
    }
}
