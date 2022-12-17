using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Data.Models;
using Project.Models;
using Project.Services;
using System.Security.Claims;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService service;
        private readonly UserManager<User> userManager;
        public CartController(ICartService service, UserManager<User> um)
        {
            this.service = service;
            userManager = um;
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

                cookie = Request.Cookies["Cart"];
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

        [HttpGet]
        [Authorize]
        public IActionResult Information()
        {
            return View(new Order());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order(Order info)
        {
            string cookie = Request.Cookies["Cart"];
            IEnumerable<CartProductViewModel> list = await service.GetCartProducts(cookie);
            info.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            info.Price = list.Sum(p => p.Price);
            info.Date = DateTime.UtcNow.ToShortDateString();
            await service.AddOrderAsync(info);

            Response.Cookies.Delete("Cart");

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Orders()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await service.GetOrdersAsync(userId));
        }

    }
}
