using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IProductRes productsRes;
        private readonly IOrdersRes ordersRes;
        private readonly IRolesRes rolesRes;

        public AdminController(IProductRes productList, IOrdersRes ordersRes, IRolesRes rolesRes)
        {
            this.productsRes = productList;
            this.ordersRes = ordersRes;
            this.rolesRes = rolesRes;
        }

        public ActionResult Orders()
        {
            var orders = ordersRes.GetAll();
            return View(orders);
        }

        public ActionResult OrderDetails(Guid orderId)
        {
            var order = ordersRes.TryGetById(orderId);
            return View(order);
        }

        public IActionResult UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            ordersRes.UpdateStatus(orderId, status);
            return RedirectToAction("Orders");
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Roles()
        {
            var roles = rolesRes.GetAll();
            return View(roles);
        }

        public IActionResult RemoveRole(string roleName)
        {
            rolesRes.Remove(roleName);
            return RedirectToAction("Roles");
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            if (rolesRes.TryGetByName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует.");
            }
            if (ModelState.IsValid)
            {
                rolesRes.Add(role);
                return RedirectToAction("Roles");
            }
            return View(role);
        }

        public ActionResult Products()
        {
            var products = productsRes.GetProducts();
            return View(products);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsRes.Add(product);
            return RedirectToAction("Products");
        }

        public ActionResult EditProduct(int productId)
        {
            var product = productsRes.TryGetById(productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsRes.Update(product);
            return RedirectToAction("Products");
        }
    }
}
