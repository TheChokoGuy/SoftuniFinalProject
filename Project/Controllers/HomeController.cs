using Microsoft.AspNetCore.Mvc;
using Project.Areas.Admin;
using Project.Data;
using Project.Data.Common;
using Project.Models;
using Project.Services.Banner;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBannerService service;

        public HomeController(IBannerService _service)
        {
            this.service = _service;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(AdminConstants.AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View(await service.GetAllAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}