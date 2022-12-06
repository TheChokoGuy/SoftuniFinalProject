using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        [HttpGet("/")]
        public IActionResult Index() => View();
    }
}
