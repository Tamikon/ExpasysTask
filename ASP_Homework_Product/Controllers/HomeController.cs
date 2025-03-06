using ASP_Homework_Product.Models;
using ASP_Homework_Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

public class HomeController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ICartsRepository _cartsRepository;

    public HomeController(IProductRepository productRepository, ICartsRepository cartsRepository)
    {
        this._productRepository = productRepository;
        this._cartsRepository = cartsRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productRepository.GetProducts();
        return Json(new { success = true, products = products.Select(p => new { p.Id, p.Name, p.Cost, p.ImagePath }) });
    }

    [HttpGet]
    public IActionResult GetCartCount()
    {
        var cart = _cartsRepository.TryGetByUserId(Constants.UserId);
        return Json(new { success = true, count = cart?.Amount ?? 0 });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}