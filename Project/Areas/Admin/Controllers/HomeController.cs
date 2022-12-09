using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        [HttpGet("/Index")]
        public IActionResult Index() => View();
    }
}
