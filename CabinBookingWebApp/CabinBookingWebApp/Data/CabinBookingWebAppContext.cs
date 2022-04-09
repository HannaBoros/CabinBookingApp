#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CabinBookingWebApp.Models;

namespace CabinBookingWebApp.Data
{
    public class CabinBookingWebAppContext : IdentityDbContext
    {
        public CabinBookingWebAppContext (DbContextOptions<CabinBookingWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<CabinBookingWebApp.Models.User> User { get; set; }

        public DbSet<CabinBookingWebApp.Models.Booking> Booking { get; set; }

       
    }
}
