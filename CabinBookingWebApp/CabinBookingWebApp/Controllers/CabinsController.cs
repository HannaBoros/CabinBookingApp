#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CabinBookingWebApp.Data;
using CabinBookingWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CabinBookingWebApp.Controllers
{
    [Authorize(Roles = "BookingAdministrators")]
    public class CabinsController : Controller
    {
        private readonly CabinBookingWebAppContext _context;

        public CabinsController(CabinBookingWebAppContext context)
        {
            _context = context;
        }

        // GET: Cabins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cabins.ToListAsync());
        }

        // GET: Cabins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabin = await _context.Cabins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cabin == null)
            {
                return NotFound();
            }

            return View(cabin);
        }

        // GET: Cabins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cabins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Location,Rooms,Price,MainImageUrl,CabinName,PersonCapability")] Cabin cabin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cabin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cabin);
        }

        // GET: Cabins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabin = await _context.Cabins.FindAsync(id);
            if (cabin == null)
            {
                return NotFound();
            }
            return View(cabin);
        }

        // POST: Cabins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Location,Rooms,Price,MainImageUrl,CabinName,PersonCapability")] Cabin cabin)
        {
            if (id != cabin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cabin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CabinExists(cabin.Id))
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
            return View(cabin);
        }

        // GET: Cabins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cabin = await _context.Cabins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cabin == null)
            {
                return NotFound();
            }

            return View(cabin);
        }

        // POST: Cabins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cabin = await _context.Cabins.FindAsync(id);
            _context.Cabins.Remove(cabin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CabinExists(int id)
        {
            return _context.Cabins.Any(e => e.Id == id);
        }

   

    }



}
