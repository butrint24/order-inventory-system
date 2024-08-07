using AutoMapper;
using Order.Application.Contracts.Abstractions;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.GetCustomerById;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Rest.Contracts.Product;
using Order.Rest.Contracts.User.CreateUser;
using Order.Rest.Contracts.User.GetUserById;
using Order.Rest.Contracts.User.GetUsers;
using UserInfo = Order.Rest.Contracts.Abstractions.User;

namespace Order.Rest.AspNetCore.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        #region User

        CreateMap<CreateUserRequest, CreateCustomer>()
            .ForMember(src => src.GivenName, opts => opts.MapFrom(dest => dest.Name))
            .ForMember(src => src.FamilyName, opts => opts.MapFrom(dest => dest.LastName));

        CreateMap<CreateCustomerResponse, CreateUserResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.CustomerId));

        CreateMap<Customer, UserInfo>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.CustomerId))
            .ForMember(src => src.FirstName, opts => opts.MapFrom(dest => dest.GivenName))
            .ForMember(src => src.LastName, opts => opts.MapFrom(dest => dest.FamilyName));

        CreateMap<IList<Customer>, GetUsersResponse>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));

        CreateMap<GetCustomerByIdResponse, GetUserByIdResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.CustomerId))
            .ForMember(src => src.FirstName, opts => opts.MapFrom(dest => dest.GivenName))
            .ForMember(src => src.LastName, opts => opts.MapFrom(dest => dest.FamilyName));


        #endregion

        #region Purchase

        CreateMap<CreatePurchaseRequest, CreateOrder>()
            .ForMember(src => src.CustomerId, opts => opts.MapFrom(dest => dest.UserId))
            .ForMember(src => src.CustomerDetails, opts => opts.Ignore())
            .ForMember(src => src.Status, opts => opts.MapFrom(dest => dest.Status));
        
        CreateMap<CreateOrderResponse, CreatePurchaseResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.OrderId));

        #endregion
    }
}
