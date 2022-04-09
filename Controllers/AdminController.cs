using Microsoft.AspNetCore.Mvc;

namespace stive.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
