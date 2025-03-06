using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IProductRepository _productsRepository;

    public ProductController(IProductRepository _productRepository)
    {
        this._productsRepository = _productRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productsRepository.GetProducts();
        return Json(new { success = true, products = products.Select(p => new { p.Id, p.Name, p.Cost }) });
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }
        _productsRepository.Add(product);
        return Json(new { success = true });
    }

    public IActionResult Edit(int productId)
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetProduct(int productId)
    {
        var product = _productsRepository.TryGetById(productId);
        if (product == null) return Json(new { success = false, error = "Продукт не найден" });
        return Json(new { success = true, product = new { product.Id, product.Name, product.Description, product.Cost } });
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, errors });
        }
        _productsRepository.Update(product);
        return Json(new { success = true });
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var product = _productsRepository.TryGetById(id);
        if (product == null) return Json(new { success = false, error = "Продукт не найден" });
        _productsRepository.Delete(id);
        return Json(new { success = true });
    }
}