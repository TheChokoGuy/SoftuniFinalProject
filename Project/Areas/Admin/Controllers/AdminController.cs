using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;

namespace Project.Areas.Admin.Controllers
{
    

    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AdminRoleName)]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}
