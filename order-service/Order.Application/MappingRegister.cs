using AutoMapper;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.GetCustomerById;

namespace Order.Application;

public class MappingRegister : Profile
{
    public MappingRegister()
    {
        CreateMap<CreateCustomer, Data.Contracts.Customer>();

        CreateMap<Data.Contracts.Customer, CreateCustomerResponse>()
            .ReverseMap();
        
        CreateMap<Data.Contracts.Customer, Contracts.Abstractions.Customer>();
        
        CreateMap<GetCustomerById, Data.Contracts.Customer>();

        CreateMap<Data.Contracts.Customer, GetCustomerByIdResponse>()
            .ReverseMap();
    }
}
