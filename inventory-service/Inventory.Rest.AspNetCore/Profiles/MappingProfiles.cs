using AutoMapper;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Rest.Contracts.CreateItem;

namespace Inventory.Rest.AspNetCore.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateItemRequest, CreateProduct>();

        CreateMap<CreateProductResponse, CreateItemResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.ProductId));
    }
}
