using Microsoft.AspNetCore.Mvc;
using ASP_Homework_Product.Models;
using System.Linq;

namespace ASP_Homework_Product.Controllers
{
    public class OrderController : Controller
    {

        private readonly ICartsRepository _cartsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public OrderController(ICartsRepository cartsRepository, IOrdersRepository ordersRepository)
        {
            this._cartsRepository = cartsRepository;
            this._ordersRepository = ordersRepository;
        }

        public IActionResult Index()
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

            var existingCart = _cartsRepository.TryGetByUserId(Constants.UserId);
            if (existingCart == null || !existingCart.Items.Any())
            {
                return Json(new { success = false, errors = new[] { "Корзина пуста" } });
            }

            var order = new Order
            {
                User = user,
                Items = existingCart.Items
            };
            _ordersRepository.Add(order);
            _cartsRepository.Clear(Constants.UserId);
            return Json(new { success = true });
        }
    }
}
