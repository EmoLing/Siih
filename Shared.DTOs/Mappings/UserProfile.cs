using AutoMapper;
using Core.Models.Users;
using Shared.DTOs.Users;

namespace Shared.DTOs.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserObject>();
        CreateMap<UserObject, User>();
    }
}
