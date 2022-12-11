using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;
using Project.Services.Banner;

namespace Project.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerService service;

        public BannerController(IBannerService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> All()
        {
            var products = await service.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add()
        {
            var model = new Banner();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Banner model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await service.AddBannerAsync(model);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong");

                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int productId)
        {
            await service.DeleteAsync(productId);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Banner model = await service.GetBannerAsync(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(Banner model)
        {
            await service.EditBannerAsync(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
