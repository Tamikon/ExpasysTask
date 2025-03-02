using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRes productsRes;

        public ProductController(IProductRes productRes)
        {
            this.productsRes = productRes;
        }

        public ActionResult Index()
        {
            var products = productsRes.GetProducts();
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
            productsRes.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int productId)
        {
            var product = productsRes.TryGetById(productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            productsRes.Update(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
