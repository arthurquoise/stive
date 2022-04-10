using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stive.Models;

namespace stive.Controllers
{
    public class ProductsController : Controller
    {
        private readonly stiveContext _context;

        public ProductsController(stiveContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            var stiveContext = _context.Products.Include(p => p.Brand).Include(p => p.ProductCategory).Include(p => p.Vendor);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(await stiveContext.ToListAsync());
            }
        }

        public async Task<IActionResult> CheckStocks()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            var stiveContext = _context.Products.Where(quantity => quantity.Quantity <= 5).Include(p => p.Brand).Include(p => p.ProductCategory).Include(p => p.Vendor);

            Console.WriteLine(stiveContext);
            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(await stiveContext.ToListAsync());
            }
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategory)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(product);
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            ViewData["BrandId"] = new SelectList(_context.ProductBrands, "BrandId", "Name");
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name");

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View();
            }
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Price,Image,ProductDate,Quantity,VendorId,ProductCategoryId,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.ProductBrands, "BrandId", "Name", product.BrandId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", product.VendorId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.ProductBrands, "BrandId", "Name", product.BrandId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", product.VendorId);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(product);
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Price,Image,ProductDate,Quantity,VendorId,ProductCategoryId,BrandId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.ProductBrands, "BrandId", "Description", product.BrandId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Adress", product.VendorId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategory)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(product);
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
