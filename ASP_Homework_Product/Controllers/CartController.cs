using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok();
        }

        public IActionResult DecreaseAmount(int productId)
        {
            _cartsRepository.DecreaseAmount(productId, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            _cartsRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
