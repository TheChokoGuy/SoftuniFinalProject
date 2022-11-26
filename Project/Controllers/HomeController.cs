using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services.Home;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await service.GetHomeProductsAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}