using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
