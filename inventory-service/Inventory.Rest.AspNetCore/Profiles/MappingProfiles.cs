using AutoMapper;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Application.Contracts.GetProductById;
using Inventory.Rest.Contracts.CreateItem;
using Inventory.Rest.Contracts.GetItemById;

namespace Inventory.Rest.AspNetCore.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateItemRequest, CreateProduct>();

        CreateMap<CreateProductResponse, CreateItemResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.ProductId));

        CreateMap<GetProductByIdResponse, GetItemByIdResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.ProductId));
    }
}
