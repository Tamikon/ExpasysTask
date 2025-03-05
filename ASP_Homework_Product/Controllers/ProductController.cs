using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ASP_Homework_Product.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public ActionResult Index(int id)
        {
            var product = _productRepository.TryGetById(id);
            return View(product);
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            try
            {
                var products = _productRepository.GetProducts();
                return Json(products);
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e.Message);
                return Json(new { error = "Ошибка загрузки товаров" });
            }
        }
    }
}
