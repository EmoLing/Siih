using AutoMapper;
using Core.Models.Departments;
using Shared.DTOs.Departments;

namespace Shared.DTOs.Mappings;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentObject>();
        CreateMap<DepartmentObject, Department>();
    }
}
