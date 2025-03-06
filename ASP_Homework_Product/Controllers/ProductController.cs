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

        [HttpGet]
        public IActionResult Index(int id)
        {
            var product = _productRepository.TryGetById(id);
            if (product == null)
            {
                return Json(new { success = false, error = "Товар не найден" });
            }
            return Json(new { success = true, product = new { product.Id, product.Name, product.Description, product.Cost, product.ImagePath } });
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
