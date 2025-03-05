using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productsRepository;

        public ProductController(IProductRepository _productRepository)
        {
            this._productsRepository = _productRepository;
        }

        public ActionResult Index()
        {
            var products = _productsRepository.GetProducts();
            return View(products);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _productsRepository.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int productId)
        {
            var product = _productsRepository.TryGetById(productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _productsRepository.Update(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productsRepository.TryGetById(id);
            if (product != null)
            {
                _productsRepository.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
