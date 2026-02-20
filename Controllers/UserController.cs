using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
            return View(request);

        await _userService.RegisterAsync(request);

        return RedirectToAction("Login");
    }

    // GET: Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var user = await _userService.LoginAsync(request);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid credentials");
            return View(request);
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Role,user.Role)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimPricipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync("Cookies", claimPricipal, new AuthenticationProperties
        {
            // IsPersistent this boolean is used for deleted cookies claims when web page is closed again open user must login the wepage
            IsPersistent = request.RememberMe,
            ExpiresUtc = request.RememberMe
            ? DateTime.UtcNow.AddMinutes(2)
            : null
        });
        return RedirectToAction("Apply", "Permission");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Login");
    }

}
