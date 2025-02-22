using AutoMapper;
using Core.Models.Equipment;
using Shared.DTOs.Equipment;

namespace Shared.DTOs.Mappings;

public class SoftwareProfile : Profile
{
    public SoftwareProfile()
    {
        CreateMap<Software, SoftwareObject>();
        CreateMap<SoftwareObject, Software>();
    }
}
