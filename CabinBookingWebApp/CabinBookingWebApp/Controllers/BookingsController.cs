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
using Microsoft.AspNetCore.Identity;
using CabinBookingWebApp.Authorization;

namespace CabinBookingWebApp
{
    [Authorize(Roles = "BookingAdministrators")]
    public class BookingsController : Controller
    {
        private readonly CabinBookingWebAppContext _context;
        private IAuthorizationService _authorizationService { get; }
        private UserManager<ApplicationUser> _userManager { get;  }


        public BookingsController
            (CabinBookingWebAppContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager; 
           
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var cabinBookingWebAppContext = _context.Booking.Include(b => b.ApplicationUser).Include(b => b.Cabin);
            return View(await cabinBookingWebAppContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.ApplicationUser)
                .Include(b => b.Cabin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        //[Authorize(Roles = "BookingAdministrators")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Email");
            ViewData["CabinId"] = new SelectList(_context.Cabins, "Id", "Description");
            var sl = Enum.GetValues(typeof(BookingStatus)).Cast<BookingStatus>().Select(s => new SelectListItem()
            {
                Text = s.ToString(),
                Value = s.ToString()
            });
            ViewData["Status"] = new SelectList(sl, "Value", "Text");
           

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckInDate,CheckOutDate,Price,UserId,CabinId,Status")] Booking booking)
        {
              if (ModelState.IsValid) { 
                //booking.UserId = _userManager.GetUserId(User);
                //var isAuthorized = await _authorizationService.AuthorizeAsync(User, booking, BookingOperations.Create);
                //if (!isAuthorized.Succeeded)
                //{
                //    return Forbid();
                //}
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Email", booking.UserId);
            ViewData["CabinId"] = new SelectList(_context.Cabins, "Id", "Description", booking.CabinId);
            var sl = Enum.GetValues(typeof(BookingStatus)).Cast<BookingStatus>().Select(s => new SelectListItem()
            {
                Text= s.ToString(),
                Value = s.ToString()
            });
            ViewData["Status"] = new SelectList(sl, "Value", "Text", booking.Status);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Email", booking.UserId);
            ViewData["CabinId"] = new SelectList(_context.Cabins, "Id", "Description", booking.CabinId);
            var sl = Enum.GetValues(typeof(BookingStatus)).Cast<BookingStatus>().Select(s => new SelectListItem()
            {
                Text = s.ToString(),
                Value = s.ToString()
            });
            ViewData["Status"] = new SelectList(sl, "Value", "Text", booking.Status);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckInDate,CheckOutDate,Price,UserId,CabinId,Status")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Email", booking.UserId);
            ViewData["CabinId"] = new SelectList(_context.Cabins, "Id", "Description", booking.CabinId);
            var sl = Enum.GetValues(typeof(BookingStatus)).Cast<BookingStatus>().Select(s => new SelectListItem()
            {
                Text = s.ToString(),
                Value = s.ToString()
            });
            ViewData["Status"] = new SelectList(sl, "Value", "Text", booking.Status);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.ApplicationUser)
                .Include(b => b.Cabin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'CabinBookingWebAppContext.Booking'  is null.");
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
          return (_context.Booking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
