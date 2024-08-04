using AutoMapper;
using Order.Application.Contracts.Customer;

namespace Order.Application;

public class MappingRegister : Profile
{
    public MappingRegister()
    {
        CreateMap<CreateCustomer, Data.Contracts.Customer>();
            //.ForMember(src => src.CustomerId, opts => opts.MapFrom(dest => dest.Id));

        //TODO: Maybe not needed
        CreateMap<Data.Contracts.Customer, CreateCustomerResponse>()
            .ReverseMap();
    }
}