using Microsoft.AspNetCore.Mvc;
using MusicFestivalManagementSystem.Models;
using MusicFestivalManagementSystem.Data;
using System.Security.Cryptography;
using System.Text;

namespace MusicFestivalManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService; // Dependency for user authentication
        private readonly ApplicationDbContext _context; // For managing users in the database

        public AccountController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        // Login GET action
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Hash the entered password
            var hashedPassword = HashPassword(model.Password);
            var user = _userService.Authenticate(model.Username, hashedPassword);

            if (user == null)
            {
                // Log failed login attempts (optional)
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            // Set session for authenticated user
            HttpContext.Session.SetString("Username", user.Username);

            // Redirect to home or dashboard after successful login
            return RedirectToAction("Index", "Home");
        }

        // Register GET action
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if username or email already exists
            if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Username or Email is already taken.");
                return View(model);
            }

            // Create a new user object
            var newUser = new User
            {
                Username = model.Username,
                Password = HashPassword(model.Password), // Hash the password before saving
                Email = model.Email
            };

            // Add and save the user in the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Redirect to the login page after successful registration
            return RedirectToAction("Login");
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            // Use a library like BCrypt for real-world scenarios
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
