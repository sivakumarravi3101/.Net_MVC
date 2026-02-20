using WebApplication1.DTOs;

public interface IUserService
{
    Task<UserDto?> LoginAsync(LoginRequestDto request);
    Task<UserDto> RegisterAsync(RegisterRequestDto request);
}
