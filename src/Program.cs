using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicFestivalManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using MusicFestivalManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();

// Register the ApplicationDbContext with a connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register SignalR
builder.Services.AddSignalR();

// Enable sessions for login management
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout (adjustable)
    options.Cookie.HttpOnly = true; // Ensure cookie is accessible only by the server
    options.Cookie.IsEssential = true; // Necessary for GDPR compliance
});

// Configure Cookie Policy (Optional: GDPR Compliance)
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
});

// Add HttpContextAccessor for accessing user identity in views
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Ensure database is created on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Applies migrations to the database
}

// Configure the middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();

// Enable cookie policy
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication(); // Add this line if authentication is enabled
app.UseAuthorization();
app.UseSession(); // Enable session management

// Map controllers and SignalR hub
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<SignalRService>("/hub");

// Add a default route for login (optional)
app.MapGet("/", context =>
{
    context.Response.Redirect("/Account/Login");
    return Task.CompletedTask;
});

app.Run();
