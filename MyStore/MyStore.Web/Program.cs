using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Application.Services;
using MyStore.Domain.Entities;
using MyStore.Infrastructure.Data;
using MyStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies; // <-- NEW: For Cookie Authentication

var builder = WebApplication.CreateBuilder(args);

// --- 1. Add Services (Dependency Injection) ---

// A. Add Authentication Services (Manual Cookie Setup)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // <-- NEW
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// B. Add Infrastructure Layer
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// C. Register the Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// D. Add Application Layer Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>(); // <-- NEW: Register our custom UserService

// E. Add Presentation Layer (Web)
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// F. Add Swagger (API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. Build the Application ---
var app = builder.Build();

// --- 3. Configure the HTTP Request Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use Authentication MUST be before UseAuthorization
app.UseAuthentication(); // <-- This Middleware is crucial for reading the cookie
app.UseAuthorization();

// Map MVC routes (e.g., /Home/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}"); // <-- Changed default to Store/Index

// Map API routes (e.g., /api/Products)
app.MapControllers();

// --- 4. Run the Application ---
app.Run();