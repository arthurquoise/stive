using Microsoft.AspNetCore.Mvc;
using stive.Helpers;
using System.Threading.Tasks;
using stive.Models;
using System.Collections.Generic;

namespace stive.Controllers
{
    public class CartController : Controller
    {
        private List<SalesOrder> cart;

        [HttpPost]
        public async Task<IActionResult> addItem(int? id)
        {
            cart = SessionHelper.GetObjectFromJson<List<SalesOrder>>(HttpContext.Session, "cart");
            return View();
        }

    }
}
