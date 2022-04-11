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

        // GET api/user/firstname/lastname/address
        [HttpGet("{productId}/{quantity}")]
        public async Task<IActionResult> addItem(int? id)
        {
            cart = SessionHelper.GetObjectFromJson<List<SalesOrder>>(HttpContext.Session, "cart");
            return View();
        }

    }
}
