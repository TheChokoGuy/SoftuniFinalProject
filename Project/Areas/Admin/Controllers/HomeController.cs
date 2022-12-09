using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;

namespace Project.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        [HttpGet("/Index")]
        public IActionResult Index() => View();
    }
}
