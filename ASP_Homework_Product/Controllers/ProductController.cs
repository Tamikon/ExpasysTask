using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRes productList;

        public ProductController(IProductRes productList)
        {
            this.productList = productList;
        }

        public ActionResult Index(int id)
        {
            var product = productList.TryGetById(id);
            return View(product);
        }
    }
}
