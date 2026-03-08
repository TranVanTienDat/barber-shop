using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BarberBookingWeb.Data;
using BarberBookingWeb.ViewModels;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BarberBookingWeb.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            Console.WriteLine($"--- DEBUG LOGIN ---");
            Console.WriteLine($"Username: '{model.Username}'");
            Console.WriteLine($"Password Length: {model.Password?.Length}");

            var admin = await _context.AdminAccounts
                .FirstOrDefaultAsync(a => a.Username == model.Username);

            if (admin != null)
            {
                Console.WriteLine($"Admin found in DB. DB Hash: '{admin.PasswordHash}'");
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(model.Password, admin.PasswordHash);
                Console.WriteLine($"BCrypt Verify Result: {isPasswordCorrect}");

                if (isPasswordCorrect)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, admin.Username),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("AdminId", admin.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                Console.WriteLine("Admin NOT found in DB!");
            }

            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
        }
        else
        {
            Console.WriteLine("ModelState is INVALID!");
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
