using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Application.Services;
using MyStore.Domain.Entities;
using MyStore.Infrastructure.Data;
using MyStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog; 


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();


    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

    //  Add Infrastructure Layer
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    //  Register the Unit of Work
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    //  Add Application Layer Services
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IUserService, UserService>(); 

    //  Add Presentation Layer (Web)
    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers();

    //  Add Swagger (API)
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    // Use Authentication MUST be before UseAuthorization
    app.UseAuthentication();
    app.UseAuthorization();

    // Map MVC routes
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Map API routes
    app.MapControllers();


    Log.Information("program is running"); 

    app.Run();
}
catch (Exception ex)
{
    // ۶. خطاهای زمان راه‌اندازی را لاگ کن
    Log.Fatal(ex, "Bad Error");
}
finally
{
    Log.CloseAndFlush();
}