using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            string cookie = Request.Cookies["Cart"];

            CookieOptions option = new CookieOptions();

            option.Expires = DateTime.Now.AddDays(7);

            if (cookie == null)
            {
                var cart = new List<int>();

                cart.Add(productId);

                var jsonCart = JsonConvert.SerializeObject(cart);

               
                Response.Cookies.Append("Cart", jsonCart, option);


            }
            else
            {
                string newCookie = await service.AddToCartAsync(cookie, productId);
                Response.Cookies.Append("Cart", newCookie, option);
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            string cookie = Request.Cookies["Cart"];

            CookieOptions option = new CookieOptions();
            option.IsEssential = true;
            option.Expires = DateTime.Now.AddDays(7);

            if (cookie == null)
            {
                var cart = new List<int>();

                var jsonCart = JsonConvert.SerializeObject(cart);


                Response.Cookies.Append("Cart", jsonCart, option);
            }

            var products = await service.GetCartProducts(cookie);

            return View(products);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(int productId)
        {
            string cookie = Request.Cookies["Cart"];
            List<int> list = JsonConvert.DeserializeObject<List<int>>(cookie);
            CookieOptions option = new CookieOptions();
            option.IsEssential = true;
            option.Expires = DateTime.Now.AddDays(7);
            list.Remove(productId);

            cookie = JsonConvert.SerializeObject(list);

            Response.Cookies.Append("Cart", cookie, option);

            return RedirectToAction(nameof(Cart));
        }
    }
}
