using AutoMapper;
using Core.Models;

namespace Shared.DTOs.Mappings;

public class TransferObjectProfile : Profile
{
    public TransferObjectProfile()
    {
        CreateMap<DatabaseObject, TransferObject>();
        CreateMap<TransferObject, DatabaseObject>();
    }
}
