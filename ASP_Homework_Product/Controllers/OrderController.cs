using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP_Homework_Product.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository)
        {
            _cartsRepository = cartsRepository;
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = User.Identity.Name;
            var orders = _ordersRepository.GetAll(userId);
            return Json(new { success = true, orders = orders.Select(o => new { o.Id, o.Cost, o.Status, o.CreateDateTime }) });
        }

        public IActionResult Details(Guid orderId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrderDetails(Guid orderId)
        {
            var userId = User.Identity.Name;
            var order = _ordersRepository.TryGetById(orderId, userId); // Исправлено с TryGetByIdAdmin
            if (order == null)
            {
                return Json(new { success = false, error = "Заказ не найден или вам не принадлежит" });
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

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserDeliveryInfo user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            var userId = User.Identity.Name;
            var existingCart = _cartsRepository.TryGetByUserId(userId);
            if (existingCart == null || !existingCart.Items.Any())
            {
                return Json(new { success = false, errors = new[] { "Корзина пуста" } });
            }

            user.Id = Guid.NewGuid();
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                UserDeliveryInfoId = user.Id,
                User = user,
                Items = new List<CartItem>(),
                Status = OrderStatus.Created,
                CreateDateTime = DateTime.UtcNow
            };

            foreach (var cartItem in existingCart.Items)
            {
                var newItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = cartItem.ProductId,
                    Product = cartItem.Product,
                    Amount = cartItem.Amount,
                    OrderId = order.Id,
                    CartId = null
                };
                order.Items.Add(newItem);
            }

            _ordersRepository.Add(order);
            _cartsRepository.Clear(userId);
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Order") });
        }
    }
}