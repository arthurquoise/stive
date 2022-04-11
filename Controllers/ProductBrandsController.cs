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
    public class ProductBrandsController : Controller
    {
        private readonly stiveContext _context;

        public ProductBrandsController(stiveContext context)
        {
            _context = context;
        }

        // GET: ProductBrands
        public async Task<IActionResult> Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(await _context.ProductBrands.ToListAsync());
            }
        }

        // GET: ProductBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(productBrand);
            }
        }

        // GET: ProductBrands/Create
        public IActionResult Create()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View();
            }
        }

        // POST: ProductBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,Name,Description")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productBrand);
        }

        // GET: ProductBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrands.FindAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(productBrand);
            }
        }

        // POST: ProductBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,Name,Description")] ProductBrand productBrand)
        {
            if (id != productBrand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBrandExists(productBrand.BrandId))
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
            return View(productBrand);
        }

        // GET: ProductBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var productBrand = await _context.ProductBrands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(productBrand);
            }
        }

        // POST: ProductBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productBrand = await _context.ProductBrands.FindAsync(id);
            _context.ProductBrands.Remove(productBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBrandExists(int id)
        {
            return _context.ProductBrands.Any(e => e.BrandId == id);
        }
    }
}
