using AutoMapper;
using Order.Application.Contracts.Customer.CreateCustomer;

namespace Order.Application;

public class MappingRegister : Profile
{
    public MappingRegister()
    {
        CreateMap<CreateCustomer, Data.Contracts.Customer>();

        CreateMap<Data.Contracts.Customer, CreateCustomerResponse>()
            .ReverseMap();
        
        CreateMap<Data.Contracts.Customer, Contracts.Abstractions.Customer>();
    }
}
