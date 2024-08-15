using AutoMapper;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Application.Contracts.GetProductById;

namespace Inventory.Application;

public class MappingRegister : Profile
{
    public MappingRegister()
    {
        CreateMap<CreateProduct, Data.Contracts.Product>();

        CreateMap<Data.Contracts.Product, CreateProductResponse>()
            .ReverseMap();
        
        CreateMap<Data.Contracts.Product, Contracts.Abstractions.Product>();
        
        CreateMap<GetProductById, Data.Contracts.Product>();

        CreateMap<Data.Contracts.Product, GetProductByIdResponse>()
            .ReverseMap();
    }
}
