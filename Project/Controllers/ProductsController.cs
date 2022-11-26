using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;

        public ProductsController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await service.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductViewModel()
            {
                Categories = await service.GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await service.AddProductAsync(model);

                return RedirectToAction(nameof(All));
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

            return RedirectToAction(nameof(All));
        }

        [HttpGet] 
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Item model = await service.GetForEditAsync(id);
            return View(model);
        }
    
        [HttpPost]
        [Authorize(Roles = "Admin")]
        
        public async Task<IActionResult> Edit(Item model)
        {
            await service.EditProductAsync(model);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Product(int productId)
        {
            Item product = await service.GetProductAsync(productId);

            return View(product);
        }
    }
}
