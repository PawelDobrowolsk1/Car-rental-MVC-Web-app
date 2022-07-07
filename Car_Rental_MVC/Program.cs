using Car_Rental_MVC.Models;
using Car_Rental_MVC.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
    });

// Add Database
var connectionString = builder.Configuration.GetConnectionString("CarRentalDatabase"); //?? throw new Exception();
    builder.Services.AddDbContext<CarRentalManagerContext>(options => options.UseSqlServer(connectionString));

// Dependency injection
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICarRepository, CarRepository>();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
