using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrdersRes ordersRes;

        public OrderController(IOrdersRes ordersRes)
        {
            this.ordersRes = ordersRes;
        }

        public ActionResult Index()
        {
            var orders = ordersRes.GetAll();
            return View(orders);
        }

        public ActionResult Details(Guid orderId)
        {
            var order = ordersRes.TryGetById(orderId);
            return View(order);
        }

        public IActionResult UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            ordersRes.UpdateStatus(orderId, status);
            return RedirectToAction(nameof(Index));
        }

    }
}
