using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CabinBookingWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Booking>? Bookings { get; set; }
    }
}
