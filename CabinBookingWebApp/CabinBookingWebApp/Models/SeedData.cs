using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CabinBookingWebApp.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using CabinBookingWebApp.Authorization;


namespace CabinBookingWebApp.Models
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            using (var context = new CabinBookingWebAppContext(
               serviceProvider.GetRequiredService<
                   DbContextOptions<CabinBookingWebAppContext>>()))
            {
                var testUserPw = "AdMiN7$#";
                //var testManagerPw = "Hello8$";
                var user = await EnsureUser(serviceProvider, testUserPw, "superadmin@contoso.com");
                //Console.WriteLine(adminID);
                await EnsureRole(serviceProvider, user.Email, Constants.BookingAdministratorsRole);

                // allowed user can create and edit contacts that they create
                //var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                //await EnsureRole(serviceProvider, managerID, ApplicationRole.ManagersRole);

                //var userID = await EnsureUser(serviceProvider, testUserPw, "user@contoso.com");
                //await EnsureRole(serviceProvider, userID, ApplicationRole.UsersRole);

                SeedDB(context, user.Id);
            }
            using (var context = new CabinBookingWebAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CabinBookingWebAppContext>>()))
            {

                // Look for any users.
                //if (context.User.Any())
                //{
                //    return;   // DB has been seeded
                //}

                //context.User.AddRange(
                //user_with_booking,

                //new User
                //{

                //    Name = "Hanni",
                //    EmailAddress = "hanni@gmail.com",
                //    PhoneNumber = "0773786321",
                //},

                //new User
                //{

                //    Name = "Andreea",
                //    EmailAddress = "andreea@gmail.com",
                //    PhoneNumber = "0771236458",
                //},

                //new User
                //{

                //    Name = "Kiara",
                //    EmailAddress = "kiarasmith@gmail.com",
                //    PhoneNumber = "0773234858",
                //}
                //); 

                //context.Booking.AddRange(
                //    new Booking
                //    {
                //         CheckInDate = DateTime.Now,
                //         CheckOutDate = DateTime.Now,
                //         User = user_with_booking,
                //         Price = 300

                //    }
                //);

                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Booking ON;");
                

            }

        }
        private static async Task<ApplicationUser> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = UserName,
                    Email= UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (userManager == null)
            {
                throw new Exception("userManager is null");
            }

            //var user = await userManager.FindByIdAsync(uid);
            var user = await userManager.FindByEmailAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(CabinBookingWebAppContext context, string adminID)
        {
            if (context.Booking.Any())
            {
                return;   // DB has been seeded
            }

            context.Booking.AddRange(
                new Booking
                {
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now,
                    Price = 100,
                    CabinId = 2,
                    Status = BookingStatus.Approved,
                    UserId = adminID,
                    
                });

            context.SaveChanges();
        }   
    }   
}
