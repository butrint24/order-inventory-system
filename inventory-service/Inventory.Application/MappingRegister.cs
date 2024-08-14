using AutoMapper;
using Inventory.Application.Contracts.CreateProduct;

namespace Inventory.Application;

public class MappingRegister : Profile
{
    public MappingRegister()
    {
        CreateMap<CreateProduct, Data.Contracts.Product>();

        CreateMap<Data.Contracts.Product, CreateProductResponse>()
            .ReverseMap();
    }
}
