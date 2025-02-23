using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP_Homework_Product.Models;

namespace ASP_Homework_Product.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductList productList;
        private readonly ICartsRes cartsRes;

        public HomeController(IProductList productList, ICartsRes cartsRes)
        {
            this.productList = productList;
            this.cartsRes = cartsRes;
        }

        public IActionResult Index()
        {
            var cart = cartsRes.TryGetByUserId(Constants.UserId);
            ViewBag.ProductCount = cart?.Amount;
            var products = productList.GetProducts();
            return View(products);
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
}