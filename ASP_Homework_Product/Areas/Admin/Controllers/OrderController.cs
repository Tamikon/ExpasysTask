using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

[Area("Admin")]
public class OrderController : Controller
{
    private readonly IOrdersRepository _ordersRepository;

    public OrderController(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = _ordersRepository.GetAllAdmin();
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
        var order = _ordersRepository.TryGetByIdAdmin(orderId); // Исправлено с TryGetById
        if (order == null)
        {
            return Json(new { success = false, error = "Заказ не найден" });
        }
        return Json(new
        {
            success = true,
            order = new
            {
                order.CreateDateTime,
                user = new { order.User.Name, order.User.Address, order.User.Phone },
                order.Cost,
                Status = order.Status.ToString(),
                items = order.Items.Select(i => new
                {
                    i.Product.Name,
                    i.Amount,
                    ProductCost = i.Product.Cost,
                    ItemCost = i.Cost
                })
            }
        });
    }

    [HttpPost]
    public IActionResult UpdateOrderStatus(Guid orderId, string status)
    {
        if (!Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
        {
            return Json(new { success = false, error = "Неверный статус" });
        }
        var order = _ordersRepository.TryGetByIdAdmin(orderId);
        if (order == null)
        {
            return Json(new { success = false, error = "Заказ не найден" });
        }
        Console.WriteLine($"UpdateOrderStatus: OrderId={orderId}, NewStatus={parsedStatus}");
        _ordersRepository.UpdateStatus(orderId, parsedStatus);
        return Json(new { success = true });
    }
}