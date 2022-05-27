using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace CabinBookingWebApp.Controllers
{
    [Authorize(Roles = "BookingAdministrators")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users(string name, int numTimes = 1)
        {
            //Don't use this, just an example'
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
        
    }
}
