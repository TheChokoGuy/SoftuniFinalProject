using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Services.Liked;

namespace Project.Controllers
{
    public class LikedController : Controller
    {
        private readonly ILikedService service;

        public LikedController(ILikedService service)
        {
            this.service = service;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToLiked(int productId)
        {
            string cookie = Request.Cookies["Liked"];

            CookieOptions option = new CookieOptions();

            option.IsEssential = true;

            option.Expires = DateTime.Now.AddDays(7);

            if (cookie == null)
            {
                var cart = new List<int>();

                cart.Add(productId);

                var jsonCart = JsonConvert.SerializeObject(cart);


                Response.Cookies.Append("Liked", jsonCart, option);


            }
            else
            {
                string newCookie = await service.AddToLikedAsync(cookie, productId);
                Response.Cookies.Append("Liked", newCookie, option);
            }

            return RedirectToAction(nameof(Liked));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Liked()
        {
            string cookie = Request.Cookies["Liked"];

            CookieOptions option = new CookieOptions();
            option.IsEssential = true;

            option.Expires = DateTime.Now.AddDays(7);

            if (cookie == null)
            {
                var liked = new List<int>();

                var jsonLiked = JsonConvert.SerializeObject(liked);


                Response.Cookies.Append("Liked", jsonLiked, option);
            }

            cookie = Request.Cookies["Liked"];

            var products = await service.GetLikedAsync(cookie);

            return View(products);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Delete(int productId)
        {
            string cookie = Request.Cookies["Liked"];
            List<int> list = JsonConvert.DeserializeObject<List<int>>(cookie);
            CookieOptions option = new CookieOptions();
            option.IsEssential = true;
            option.Expires = DateTime.Now.AddDays(7);
            list.Remove(productId);

            cookie = JsonConvert.SerializeObject(list);

            Response.Cookies.Append("Liked", cookie, option);

            return RedirectToAction(nameof(Liked));
        }
    }
}
