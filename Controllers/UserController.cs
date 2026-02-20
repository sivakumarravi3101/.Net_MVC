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
            new Claim(ClaimTypes.Email,user.Email)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimPricipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync("Cookies", claimPricipal);
        return RedirectToAction("Apply", "Permission");
    }
}
