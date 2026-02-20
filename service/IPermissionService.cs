
using WebApplication1.DTOs;

public interface IPermissionService
{
    Task ApplyAsync(int userId, ApplyPermissionRequestDto request);
}