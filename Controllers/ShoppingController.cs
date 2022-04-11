﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stive.Models;
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

        public async Task<IActionResult> Index(int categoryId)
        {
            ViewBag.sessionv = HttpContext.Session.GetString("Connected");

            ViewBag.categories = await _context.ProductCategories.ToListAsync();
            var stiveContext = _context.Products.Include(p => p.Brand).Include(p => p.ProductCategory).Include(p => p.Vendor);
            return View(await stiveContext.ToListAsync());
        }

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
