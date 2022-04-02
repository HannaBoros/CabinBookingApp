﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CabinBookingWebApp.Data;
using System;
using System.Linq;
namespace CabinBookingWebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CabinBookingWebAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CabinBookingWebAppContext>>()))
            {
                // Look for any users.
                //if (context.User.Any())
                //{
                //    return;   // DB has been seeded
                //}
                
                context.User.AddRange(
                    new User
                    {
                        Name = "Hanna",
                        EmailAddress = "hannaboros@gmail.com",
                        PhoneNumber = "0773786458",
                    },

                    new User
                    {
                        Name = "Hanni",
                        EmailAddress = "hanni@gmail.com",
                        PhoneNumber = "0773786321",
                    },

                    new User
                    {
                        Name = "Andreea",
                        EmailAddress = "andreea@gmail.com",
                        PhoneNumber = "0771236458",
                    },

                    new User
                    {
                        Name = "Kiara",
                        EmailAddress = "kiarasmith@gmail.com",
                        PhoneNumber = "0773234858",
                    }
                );

                context.Booking.AddRange(
                    new Booking
                    {
                        CheckInDate = DateTime.Now,
                        CheckOutDate = DateTime.Now,
                        UserId=3,
                        Price = 300

                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
