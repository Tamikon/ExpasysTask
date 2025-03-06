using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASP_Homework_Product.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartsRepository _cartsRepository;

        public CartController(IProductRepository productRepository, ICartsRepository cartsRepository)
        {
            this._productRepository = productRepository;
            this._cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            return View(cart);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            if (cart == null)
            {
                return Json(new { items = new List<CartItem>(), cost = 0, amount = 0 });
            }
            return Json(new { items = cart.Items, cost = cart.Cost, amount = cart.Amount });
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            return Json(new { count = cart?.Amount ?? 0 });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var product = _productRepository.TryGetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            _cartsRepository.Add(product, Constants.UserId);
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            return Json(new { success = true, cart = new { items = cart.Items, cost = cart.Cost } });
        }

        [HttpPost]
        public IActionResult DecreaseAmount(int productId)
        {
            _cartsRepository.DecreaseAmount(productId, Constants.UserId);
            var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
            return Json(new { success = true, cart = new { items = cart?.Items ?? new List<CartItem>(), cost = cart?.Cost ?? 0 } });
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartsRepository.Clear(Constants.UserId);
            return Json(new { success = true, cart = new { items = new List<CartItem>(), cost = 0 } });
        }
    }
}
