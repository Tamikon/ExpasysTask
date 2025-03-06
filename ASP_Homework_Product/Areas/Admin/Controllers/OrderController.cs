using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

[Area("Admin")]
public class OrderController : Controller
{
    private readonly IOrdersRepository ordersRes;

    public OrderController(IOrdersRepository ordersRes)
    {
        this.ordersRes = ordersRes;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = ordersRes.GetAll();
        return Json(new { success = true, orders = orders.Select(o => new { o.Id, o.Cost, o.Status, o.CreateDateTime }) });
    }

    [HttpGet]
    public IActionResult Details(Guid orderId)
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetOrderDetails(Guid orderId)
    {
        var order = ordersRes.TryGetById(orderId);
        if (order == null)
        {
            return Json(new { success = false, error = "Заказ не найден" });
        }
        return Json(new
        {
            success = true,
            order = new
            {
                order.Id,
                order.CreateDateTime,
                User = new { order.User.Name, order.User.Address, order.User.Phone },
                order.Cost,
                order.Status
            }
        });
    }

    [HttpPost]
    public IActionResult UpdateOrderStatus(Guid orderId, OrderStatus status)
    {
        var order = ordersRes.TryGetById(orderId);
        if (order == null)
        {
            return Json(new { success = false, error = "Заказ не найден" });
        }
        ordersRes.UpdateStatus(orderId, status);
        return Json(new { success = true });
    }
}