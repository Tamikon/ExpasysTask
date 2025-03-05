using Microsoft.AspNetCore.Mvc;
using ASP_Homework_Product.Models;

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
                return RedirectToAction("Index", user);
            }

            var existingCart = _cartsRepository.TryGetByUserId(Constants.UserId);
            var order = new Order
            {
                User = user,
                Items = existingCart.Items
            };
            _ordersRepository.Add(order);
            _cartsRepository.Clear(Constants.UserId);
            return View();
        }
    }
}
