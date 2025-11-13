//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();


// File: MyStore.Web/Program.cs
using Microsoft.EntityFrameworkCore;
using MyStore.Application.Interfaces;
using MyStore.Application.Services;
using MyStore.Infrastructure.Data;
using MyStore.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Add Services (Dependency Injection) ---

// A. Add Infrastructure Layer
// Register the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the Unit of Work. 
// We use AddScoped, meaning one instance per HTTP request.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// B. Add Application Layer
// Register the services. Also AddScoped.
builder.Services.AddScoped<IProductService, ProductService>();
// (We can add ICategoryService, IOrderService here later)

// C. Add Presentation Layer (Web)
builder.Services.AddControllersWithViews(); // For MVC Pages
builder.Services.AddControllers();         // For API Controllers

// D. Add Swagger (as requested for API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 2. Build the Application ---
var app = builder.Build();

// --- 3. Configure the HTTP Request Pipeline ---
if (app.Environment.IsDevelopment())
{
    // Use Swagger UI only in the development environment
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // For serving CSS, JavaScript, and images

app.UseRouting();

app.UseAuthorization();

// Map MVC routes (e.g., /Home/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map API routes (e.g., /api/Products)
app.MapControllers();

// --- 4. Run the Application ---
app.Run();

