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
    public class PurchaseOrdersController : Controller
    {
        private readonly stiveContext _context;

        public PurchaseOrdersController(stiveContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        public async Task<IActionResult> Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");
            var stiveContext = _context.PurchaseOrders.Include(p => p.Person).Include(p => p.Product).Include(p => p.Status).Include(p => p.Vendor);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(await stiveContext.ToListAsync());
            }
        }

        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Person)
                .Include(p => p.Product)
                .Include(p => p.Status)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(purchaseOrder);
            }

        }

        // GET: PurchaseOrders/Create
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                ViewBag.productQuantity = product.Quantity;
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", product.ProductId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name");
                ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", product.VendorId);
                ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "FirstName");
            }
            else
            {
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
                ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name");
                ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name");
                ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "FirstName");
            }



            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View();
            }
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseOrderId,OrderDate,Quantity,SubTotal,ProductId,StatusId,VendorId,PersonId")] PurchaseOrder purchaseOrder)
        {

            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "FirstName", purchaseOrder.PersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrder.ProductId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", purchaseOrder.StatusId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", purchaseOrder.VendorId);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "FirstName", purchaseOrder.PersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrder.ProductId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", purchaseOrder.StatusId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", purchaseOrder.VendorId);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(purchaseOrder);
            }

        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderId,OrderDate,Quantity,SubTotal,ProductId,StatusId,VendorId,PersonId")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.PurchaseOrderId))
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
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "FirstName", purchaseOrder.PersonId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", purchaseOrder.ProductId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", purchaseOrder.StatusId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", purchaseOrder.VendorId);
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(p => p.Person)
                .Include(p => p.Product)
                .Include(p => p.Status)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(purchaseOrder);
            }
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.PurchaseOrderId == id);
        }
    }
}
