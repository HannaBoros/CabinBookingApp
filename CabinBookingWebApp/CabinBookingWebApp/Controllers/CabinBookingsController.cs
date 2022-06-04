using CabinBookingWebApp.Data;
using CabinBookingWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CabinBookingWebApp.Controllers
{
    [Authorize]
    public class CabinBookingsController : Controller
    {
        private readonly CabinBookingWebAppContext _context;
        private IAuthorizationService _authorizationService { get; }
        private UserManager<ApplicationUser> _userManager { get; }


        public CabinBookingsController
            (CabinBookingWebAppContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;

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
        // GET: Bookings/Create
        //[Authorize(Roles = "BookingAdministrators")]
        public async Task<IActionResult> Create(string cabinId)
        {
            var userId= _userManager.GetUserId(User);

            var sliUser = new SelectListItem()
            {
                Text = userId,
                Value = userId
            };
            var sliCabin = new SelectListItem()
            {
                Text = cabinId,
                Value = cabinId
            };
            var sliStatus = new SelectListItem()
            {
                Text = BookingStatus.Submitted.ToString(),
                Value = BookingStatus.Submitted.ToString()
            };
            List<SelectListItem> slu = new List<SelectListItem>();
           List<SelectListItem> slc = new List<SelectListItem>();
            List<SelectListItem> sls = new List<SelectListItem>();
            slu.Add(sliUser);
            slc.Add(sliCabin);
            sls.Add(sliStatus);
            ViewData["UserId"] = new SelectList(slu.AsEnumerable(), "Value", "Text");
            ViewData["CabinId"] = new SelectList(slc.AsEnumerable(), "Value", "Text");
            ViewData["Status"] = new SelectList(sls.AsEnumerable(), "Value", "Text");
            var cabin = await _context.Cabins
              .FirstOrDefaultAsync(m => m.Id.ToString() == cabinId);
            ViewData["Price"] = "";
            ViewData["PricePerNight"] = cabin.Price;

            return View();
        }
        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckInDate,CheckOutDate,Price,UserId,CabinId,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
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
                Text = s.ToString(),
                Value = s.ToString()
            });
            ViewData["Status"] = new SelectList(sl, "Value", "Text", booking.Status);
            return View(booking);
        }
    }
}
