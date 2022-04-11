using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stive.Models;
using System.Linq;
using System.Threading.Tasks;

namespace stive.Controllers
{
    public class ShoppingController : Controller
    {

        private readonly stiveContext _context;

        public ShoppingController(stiveContext stiveContext)
        {
            _context = stiveContext;
        }

        [HttpGet]
        //GET Index/categoryId
        public async Task<IActionResult> Index(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            ViewBag.categories = await _context.ProductCategories.ToListAsync();

            var stiveContext = await _context.Products.Include(p => p.Brand).Include(p => p.ProductCategory).Include(p => p.Vendor).ToListAsync();

            if (id != null)
            {
                    stiveContext = await _context.Products.Include(p => p.Brand)
                    .Include(p => p.ProductCategory)
                    .Include(p => p.Vendor)
                    .Where(p => p.ProductCategoryId == id)
                    .ToListAsync();
            }
            
            return View(stiveContext);
        }


        //GET the detail of a product
        public async Task<IActionResult> ProductDetail(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategory)
                .Include(p =>p.Vendor)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
