using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stive.Models;
using System.Linq;

namespace stive.Controllers
{
    public class AdminController : Controller
    {

        private readonly stiveContext _context;

        public AdminController(stiveContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPost(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = password;
                // Checking the 
                var data = _context.Admin.Where(s => s.Email.Equals(email) && s.Password.Equals(password));
                if (data.Count() > 0)
                {
                    HttpContext.Session.SetString("Connected", data.FirstOrDefault().Email);
                    return Redirect("/Home/Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            ViewBag.error = "Login failed : ModelState Invalid";
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Connected");
            return Redirect("/Home/Index");
        }

    }
}


