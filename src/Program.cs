using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicFestivalManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using MusicFestivalManagementSystem.Services;


var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews();

// Register the ApplicationDbContext with a connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Map controllers and SignalR hub
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<SignalRService>("/hub");

app.Run();
