using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductList productList;
        private readonly ICartsRes cartsRes;

        public CartController(IProductList productList, ICartsRes cartsRes)
        {
            this.productList = productList;
            this.cartsRes = cartsRes;
        }

        public IActionResult Index()
        {
            var cart = cartsRes.TryGetByUserId(Constants.UserId);
            return View(cart);
        }
        
        public IActionResult Add(int productId)
        {
            var product = productList.TryGetById(productId);
            cartsRes.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseAmount(int productId)
        {
            cartsRes.DecreaseAmount(productId, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            cartsRes.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
