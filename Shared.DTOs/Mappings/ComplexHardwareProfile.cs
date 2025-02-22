using AutoMapper;
using Core.Models.Equipment;
using Shared.DTOs.Equipment;

namespace Shared.DTOs.Mappings;

public class ComplexHardwareProfile : Profile
{
    public ComplexHardwareProfile()
    {
        CreateMap<ComplexHardware, ComplexHardwareObject>();
        CreateMap<ComplexHardwareObject, ComplexHardware>();
    }
}
