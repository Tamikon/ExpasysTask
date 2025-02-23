using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}
