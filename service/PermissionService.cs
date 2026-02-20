using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;

public class PermissionService : IPermissionService
{
    private readonly EfDbContext _context;

    public PermissionService(EfDbContext context)
    {
        _context = context;
    }

    public async Task ApplyAsync(int userId, ApplyPermissionRequestDto request)
    {
        var utcDate = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);
        var permission = new Permission
        {
            UserId = userId,
            PermissionDate = utcDate,
            HoursOfPermission = request.Hours,
            CreatedAt = DateTime.UtcNow
        };

        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
    }
}
