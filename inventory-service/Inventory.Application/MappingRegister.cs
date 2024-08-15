using AutoMapper;
using Inventory.Application.Contracts.CreateProduct;
using Inventory.Application.Contracts.DeleteProduct;
using Inventory.Application.Contracts.GetProductById;
using Inventory.Application.Contracts.UpdateProduct;

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
        
        CreateMap<UpdateProduct, Data.Contracts.Product>();

        CreateMap<Data.Contracts.Product, UpdateProductResponse>()
            .ReverseMap();
        
        CreateMap<DeleteProduct, Data.Contracts.Product>();

        CreateMap<Data.Contracts.Product, DeleteProductResponse>()
            .ReverseMap();
    }
}
