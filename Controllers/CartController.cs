using Microsoft.AspNetCore.Mvc;
using stive.Helpers;
using System.Threading.Tasks;
using stive.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace stive.Controllers
{
    public class CartController : Controller
    {
        private readonly stiveContext _context;
        private List<SalesOrder> cart;

        public CartController(stiveContext context)
        {
            _context = context;
        }

        /*        // GET api/user/firstname/lastname/address
                [HttpPost]
                public async Task<IActionResult> addItem(int? id, int? quantity)
                {
                    if(id != null && quantity != null)
                    {
                        var product = await _context.Products
                          .Include(p => p.Brand)
                          .Include(p => p.ProductCategory)
                          .Include(p => p.Vendor)
                          .FirstOrDefaultAsync(m => m.ProductId == id);



                        SalesOrder sale;
                        sale = new SalesOrder();
                        SalesOrderDetail saleDetail;
                        saleDetail = new SalesOrderDetail();
                        saleDetail.Product = product;
                        sale.ProductQuantity = (int)quantity;

                    }

                    cart = SessionHelper.GetObjectFromJson<List<SalesOrder>>(HttpContext.Session, "cart");
                    return View();
                }*/


        // GET: Cart/AddProduct/5
        public async Task<IActionResult> AddProduct(int? id)
        {

            if (SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart") == null)
            {
                List<Product> products = new List<Product>();
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                {
                    products.Add(product);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", products);
                }
            } else
            {
                List<Product> products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if(product != null)  { 
                    products.Add(product);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", products);
                }
                
                
            }

            List<Product> productsList = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart"); 
            double total = (double)productsList.Sum(p => p.Price);

            ViewBag.total = total;

            return View("Index", SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart"));
        }



    }
}
