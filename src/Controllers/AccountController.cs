using Microsoft.AspNetCore.Mvc;
using MusicFestivalManagementSystem.Models;
using MusicFestivalManagementSystem.Data;


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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }

            // Redirect to the home page or a dashboard after successful login
            return RedirectToAction("Index", "Home");
        }

        // Add the registration feature
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if the username or email already exists
            if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Username or Email is already taken");
                return View(model);
            }

            // Create a new user object
            var newUser = new User
            {
                Username = model.Username,
                Password = model.Password, // In a real-world app, make sure to hash the password
                Email = model.Email
            };

            // Add and save the user in the database
            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Redirect to the login page after successful registration
            return RedirectToAction("Login");
        }
    }
}
