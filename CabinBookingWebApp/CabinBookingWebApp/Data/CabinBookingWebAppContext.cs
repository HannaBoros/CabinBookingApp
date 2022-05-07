#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CabinBookingWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CabinBookingWebApp.Data
{
    public class CabinBookingWebAppContext : IdentityDbContext<ApplicationUser>
    {
        public CabinBookingWebAppContext (DbContextOptions<CabinBookingWebAppContext> options)
            : base(options)
        {
        }

       public DbSet<Booking> Booking { get; set; }


       public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Cabin> Cabins { get; set; }
        

       
    }
}
