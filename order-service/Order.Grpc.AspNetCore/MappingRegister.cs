using AutoMapper;
using Inventory.Grpc.Contracts;
using Order.Grpc.Contracts;

namespace Order.Grpc.AspNetCore;

public class MappingRegister : Profile
{
    public MappingRegister()
    {
        CreateMap<GrpcProductModel, Product>();
    }
}
