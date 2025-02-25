using Microsoft.AspNetCore.Mvc;
using ASP_Homework_Product.Models;

namespace ASP_Homework_Product.Controllers
{
    public class OrderController : Controller
    {

        private readonly ICartsRes cartsRes;
        private readonly IOrdersRes ordersRes;

        public OrderController(ICartsRes cartsRes, IOrdersRes ordersRes)
        {
            this.cartsRes = cartsRes;
            this.ordersRes = ordersRes;
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

            var existingCart = cartsRes.TryGetByUserId(Constants.UserId);
            var order = new Order
            {
                User = user,
                Items = existingCart.Items
            };
            ordersRes.Add(order);
            cartsRes.Clear(Constants.UserId);
            return View();
        }
    }
}
