using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;

public class PermissionService : IPermissionService
{
    private readonly EfDbContext _context;
    private readonly IMapper _mapper;
    public PermissionService(EfDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

    public async Task<List<Permission>> PermissionListAsync(int userId, string role)
    {
        IQueryable<Permission> query = _context.Permissions.Include(p => p.User);

        if (role != "Admin")
        {
            query = query.Where(p => p.UserId == userId);
        }
        var list = await query.ToListAsync();
        return _mapper.Map<List<Permission>>(list);

    }
}
