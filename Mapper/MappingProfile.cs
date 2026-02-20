using AutoMapper;
using WebApplication1.Entities;
using WebApplication1.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
