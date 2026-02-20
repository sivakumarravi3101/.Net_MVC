using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Entities;

public class UserService : IUserService
{
    private readonly EfDbContext _context;
    private readonly IMapper _mapper;

    public UserService(EfDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
            return null;
        var computedHash = StringHelper.GetPasswordHash(
            request.Password,
            user.PasswordSalt);
        if (user.PasswordHash != computedHash)
        {
            Console.WriteLine("password wrong");
            return null;
        }


        return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> RegisterAsync(RegisterRequestDto request)
    {
        var exists = await _context.Users
        .AnyAsync(u => u.Email == request.Email);

        if (exists)
            throw new Exception("User already exists");
        var passwordSalt = StringHelper.GetRandomToken(8);
        var passwordHash = StringHelper.GetPasswordHash(request.Password, passwordSalt);
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }
}

