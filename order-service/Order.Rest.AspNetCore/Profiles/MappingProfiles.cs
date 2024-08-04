using AutoMapper;
using Order.Application.Contracts.Customer;
using Order.Rest.Contracts.User;

namespace Order.Rest.AspNetCore.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateUserRequest, CreateCustomer>()
            .ForMember(src => src.GivenName, opts => opts.MapFrom(dest => dest.Name))
            .ForMember(src => src.FamilyName, opts => opts.MapFrom(dest => dest.LastName));

        CreateMap<CreateCustomerResponse, CreateUserResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.CustomerId));
    }
}