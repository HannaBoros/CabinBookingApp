using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CabinBookingWebApp.Data;
using CabinBookingWebApp.Models;
using System.Web;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CabinBookingWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CabinBookingWebAppContext")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CabinBookingWebAppContext>();builder.Services.AddDbContext<CabinBookingWebAppContext>(options =>
    options.UseSqlServer(connectionString));//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCoreAdmin();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseCoreAdminCustomUrl("SuperAdmin");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

