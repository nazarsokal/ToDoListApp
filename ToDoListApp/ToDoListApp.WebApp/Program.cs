using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Common.Models;
using ToDoListApp.IdentityDb;
using ToDoListApp.WebApp.Services;
using ToDoListApp.WebApp.Services.ServiceContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionUsers")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureApplicationCookie(options =>
{
    // Redirects
    options.LoginPath = "/Account/Login";        // where to redirect if not logged in
    options.AccessDeniedPath = "/Account/AccessDenied"; // for forbidden pages

    // Expiration
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // cookie lifetime
    options.SlidingExpiration = true; // refresh cookie on each request
    options.Cookie.HttpOnly = true;   // prevents JS access (security)
    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // set to Always in production
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();