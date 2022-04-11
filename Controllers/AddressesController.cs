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
    public class AddressesController : Controller
    {
        private readonly stiveContext _context;

        public AddressesController(stiveContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            var stiveContext = _context.Addresses.Include(a => a.Person);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(await stiveContext.ToListAsync());
            }
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(address);
            }
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Email");

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View();
            }
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,City,ZipCode,Address1,FirstName,LastName,PersonId")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Email", address.PersonId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Email", address.PersonId);

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(address);
            }
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,City,ZipCode,Address1,FirstName,LastName,PersonId")] Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressId))
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
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Email", address.PersonId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            if (ViewBag.sessionv == null)
            {
                return View("AccessForbiden");
            }
            else
            {
                return View(address);
            }
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
