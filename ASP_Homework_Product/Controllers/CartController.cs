using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASP_Homework_Product.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartsRepository _cartsRepository;

        public CartController(IProductRepository productRepository, ICartsRepository cartsRepository)
        {
            _productRepository = productRepository;
            _cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            var userId = User.Identity.Name;
            var cart = _cartsRepository.TryGetByUserId(userId);
            return View(cart);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var userId = User.Identity.Name;
            var cart = _cartsRepository.TryGetByUserId(userId);
            if (cart == null)
            {
                return Json(new { items = new List<CartItem>(), cost = 0, amount = 0 });
            }
            return Json(new { items = cart.Items, cost = cart.Cost, amount = cart.Amount });
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var userId = User.Identity.Name;
            var cart = _cartsRepository.TryGetByUserId(userId);
            return Json(new { count = cart?.Amount ?? 0 });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var userId = User.Identity.Name;
            var product = _productRepository.TryGetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            _cartsRepository.Add(product, userId);
            var cart = _cartsRepository.TryGetByUserId(userId);
            return Json(new { success = true, cart = new { items = cart.Items, cost = cart.Cost } });
        }

        [HttpPost]
        public IActionResult DecreaseAmount(int productId)
        {
            var userId = User.Identity.Name;
            _cartsRepository.DecreaseAmount(productId, userId);
            var cart = _cartsRepository.TryGetByUserId(userId);
            return Json(new { success = true, cart = new { items = cart?.Items ?? new List<CartItem>(), cost = cart?.Cost ?? 0 } });
        }

        [HttpPost]
        public IActionResult Clear()
        {
            var userId = User.Identity.Name;
            _cartsRepository.Clear(userId);
            return Json(new { success = true, cart = new { items = new List<CartItem>(), cost = 0 } });
        }
    }
}