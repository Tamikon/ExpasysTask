using Microsoft.AspNetCore.Mvc;

namespace ASP_Homework_Product.Views.Shared.ViewComponents.CartViewComponents
{
    public class CartViewComponent : ViewComponent
    {

        private readonly ICartsRepository cartsRes;

        public CartViewComponent(ICartsRepository cartsRes)
        {
            this.cartsRes = cartsRes;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsRes.TryGetByUserId(Constants.UserId);
            var productCounts = cart?.Amount ?? 0;

            return View("Cart", productCounts);
        }
    }
}
