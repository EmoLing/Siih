using AutoMapper;
using Core.Models.Equipment;
using DTO = Shared.DTOs.Equipment;

namespace Shared.DTOs.Mappings;

public class ComplexHardwareTypeProfile : Profile
{
    public ComplexHardwareTypeProfile()
    {
        CreateMap<ComplexHardwareType, DTO.ComplexHardwareType>();
        CreateMap<DTO.ComplexHardwareType, ComplexHardwareType> ();
    }
}
