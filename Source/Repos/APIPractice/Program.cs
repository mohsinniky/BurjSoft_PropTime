using APIPractice.Data;
using APIPractice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using APIPractice.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register services
builder.Services.AddScoped<IUserService, UserService>();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-3OT71VJ;Database=IdentityCoreDB;User Id=sa;Password=123;TrustServerCertificate=true;Encrypt=false;"));

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC", policy =>
    {
        policy.WithOrigins("https://localhost:7091", "http://localhost:5177") // Your MVC app URLs
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowMVC"); // Add this line

app.UseAuthentication(); // Add this before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
