using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stive.Models;

namespace stive.Controllers
{
    public class SalesOrderDetailsController : Controller
    {
        private readonly stiveContext _context;

        public SalesOrderDetailsController(stiveContext context)
        {
            _context = context;
        }

        // GET: SalesOrderDetails
        public async Task<IActionResult> Index()
        {
            var stiveContext = _context.SalesOrderDetails.Include(s => s.Product).Include(s => s.SalesOrder);
            return View(await stiveContext.ToListAsync());
        }

        // GET: SalesOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetails
                .Include(s => s.Product)
                .Include(s => s.SalesOrder)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Image");
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrders, "SalesOrderId", "SalesOrderId");
            return View();
        }

        // POST: SalesOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,SalesOrderId,LineTotal,OrderQuantity,UnitPrice")] SalesOrderDetail salesOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Image", salesOrderDetail.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrders, "SalesOrderId", "SalesOrderId", salesOrderDetail.SalesOrderId);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Image", salesOrderDetail.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrders, "SalesOrderId", "SalesOrderId", salesOrderDetail.SalesOrderId);
            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,SalesOrderId,LineTotal,OrderQuantity,UnitPrice")] SalesOrderDetail salesOrderDetail)
        {
            if (id != salesOrderDetail.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderDetailExists(salesOrderDetail.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Image", salesOrderDetail.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrders, "SalesOrderId", "SalesOrderId", salesOrderDetail.SalesOrderId);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetails
                .Include(s => s.Product)
                .Include(s => s.SalesOrder)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(id);
            _context.SalesOrderDetails.Remove(salesOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderDetailExists(int id)
        {
            return _context.SalesOrderDetails.Any(e => e.ProductId == id);
        }
    }
}
