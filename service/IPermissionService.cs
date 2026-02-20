
using WebApplication1.DTOs;
using WebApplication1.Entities;

public interface IPermissionService
{
    Task ApplyAsync(int userId, ApplyPermissionRequestDto request);
    Task<List<Permission>> PermissionListAsync(int userId, string role);
}