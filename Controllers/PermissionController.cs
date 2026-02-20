using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.DTOs;

[Authorize]
public class PermissionController : Controller
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public IActionResult Apply()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Apply(ApplyPermissionRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        await _permissionService.ApplyAsync(userId, request);

        return RedirectToAction("Apply");
    }
}
