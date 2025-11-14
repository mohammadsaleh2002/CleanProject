using Microsoft.AspNetCore.Mvc;
using MyStore.Application.Interfaces;
using MyStore.Web.ViewModels; 
using MyStore.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace MyStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // --- REGISTER ---

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existingUser = await _userService.GetUserByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "کاربری با این ایمیل قبلاً ثبت نام کرده است.");
                    return View(model);
                }

                // Create the new user object
                var user = new User
                {
                    Email = model.Email,
                    Address = model.Address
                    // Username & PasswordHash will be set by the service
                };

                // Register the user (this hashes the password)
                var result = await _userService.RegisterUserAsync(user, model.Password);

                if (result)
                {
                    // Automatically log the user in after registration
                    await LoginUser(user); // Call our helper method
                    return RedirectToAction("Index", "Store");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // --- LOGIN ---

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if credentials are valid
                var user = await _userService.AuthenticateUserAsync(model.Email, model.Password);

                if (user != null)
                {
                    // Credentials are valid, log the user in
                    await LoginUser(user); // Call our helper method
                    return RedirectToAction("Index", "Store");
                }

                // Invalid credentials
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور نامعتبر است.");
            }
            return View(model);
        }

        // --- LOGOUT ---

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // This method clears the authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Store");
        }

        // --- Helper Method ---
        private async Task LoginUser(User user)
        {
            // These "claims" are pieces of information about the user
            // that are stored in the authentication cookie.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                // We can add custom claims
                new Claim("Address", user.Address ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // You can set properties like "Remember Me" here
                IsPersistent = true
            };

            // This is the line that creates and sets the cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}