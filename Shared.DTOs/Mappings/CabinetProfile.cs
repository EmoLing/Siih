using AutoMapper;
using Core.Models.Departments;
using Shared.DTOs.Departments;

namespace Shared.DTOs.Mappings;

public class CabinetProfile : Profile
{
    public CabinetProfile()
    {
        CreateMap<Cabinet, CabinetObject>();
        CreateMap<CabinetObject, Cabinet>();
    }
}
