using AutoMapper;
using Inventory.Application.Contracts.Abstractions;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Application.Contracts.GetProductById;
using Inventory.Application.Contracts.UpdateProduct;
using Inventory.Rest.Contracts.Abstractions;
using Inventory.Rest.Contracts.CreateItem;
using Inventory.Rest.Contracts.GetItemById;
using Inventory.Rest.Contracts.GetItems;
using Inventory.Rest.Contracts.UpdateItem;

namespace Inventory.Rest.AspNetCore.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateItemRequest, CreateProduct>();

        CreateMap<CreateProductResponse, CreateItemResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.ProductId));
        
        CreateMap<Product, Item>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ProductId));
        
        CreateMap<IList<Product>, GetItemsResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

        CreateMap<GetProductByIdResponse, GetItemByIdResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.ProductId));
        
        CreateMap<UpdateItemRequest, UpdateProduct>();

        CreateMap<UpdateProductResponse, UpdateItemResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.ProductId));
    }
}
