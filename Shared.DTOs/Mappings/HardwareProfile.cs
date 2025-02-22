using AutoMapper;
using Core.Models.Equipment;
using Shared.DTOs.Equipment;

namespace Shared.DTOs.Mappings;

public class HardwareProfile : Profile
{
    public HardwareProfile()
    {
        CreateMap<Hardware, HardwareObject>();
        CreateMap<HardwareObject, Hardware>();
    }
}
