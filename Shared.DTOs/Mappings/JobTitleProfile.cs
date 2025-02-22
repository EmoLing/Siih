using AutoMapper;
using Core.Models.Users;
using Shared.DTOs.Users;

namespace Shared.DTOs.Mappings;

public class JobTitleProfile : Profile
{
    public JobTitleProfile()
    {
        CreateMap<JobTitle, JobTitleObject>();
        CreateMap<JobTitleObject, JobTitle>();
    }
}
