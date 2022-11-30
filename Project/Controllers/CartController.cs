using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService service;

        public CartController(ICartService service)
        {
            this.service = service;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId)
        {
            await service.AddToCartAsync(productId);
            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var products = await service.GetCartProducts();

            return View(products);
        }
    }
}
