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
    public class SalesOrdersController : Controller
    {
        private readonly stiveContext _context;

        public SalesOrdersController(stiveContext context)
        {
            _context = context;
        }

        // GET: SalesOrders
        public async Task<IActionResult> Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");
            var stiveContext = _context.SalesOrders.Include(s => s.Person).Include(s => s.Status);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(await stiveContext.ToListAsync());
            }
        }

        // GET: SalesOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(s => s.Person)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(salesOrder);
            }

        }

        // GET: SalesOrders/Create
        public IActionResult Create()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "LastName");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name");

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View();
            }

        }

        // POST: SalesOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesOrderId,SubTotal,OrderDate,ProductQuantity,StatusId,PersonId")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "LastName", salesOrder.PersonId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", salesOrder.StatusId);
            return View(salesOrder);
        }

        // GET: SalesOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders.FindAsync(id);
            if (salesOrder == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "LastName", salesOrder.PersonId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", salesOrder.StatusId);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(salesOrder);
            }
        }

        // POST: SalesOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,SubTotal,OrderDate,ProductQuantity,StatusId,PersonId")] SalesOrder salesOrder)
        {
            if (id != salesOrder.SalesOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderExists(salesOrder.SalesOrderId))
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
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "LastName", salesOrder.PersonId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "Name", salesOrder.StatusId);
            return View(salesOrder);
        }

        // GET: SalesOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(s => s.Person)
                .Include(s => s.Status)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
            if (salesOrder == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(salesOrder);
            }
        }

        // POST: SalesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrder = await _context.SalesOrders.FindAsync(id);
            _context.SalesOrders.Remove(salesOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderExists(int id)
        {
            return _context.SalesOrders.Any(e => e.SalesOrderId == id);
        }
    }
}
