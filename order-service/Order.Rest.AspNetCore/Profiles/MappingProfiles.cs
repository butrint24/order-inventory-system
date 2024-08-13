using AutoMapper;
using Order.Application.Contracts.Abstractions;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.GetCustomerById;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Rest.Contracts.Abstractions;
using Order.Rest.Contracts.Purchase.CreatePurchase;
using Order.Rest.Contracts.Purchase.GetPurchases;
using Order.Rest.Contracts.User.CreateUser;
using Order.Rest.Contracts.User.GetUserById;
using Order.Rest.Contracts.User.GetUsers;
using PurchaseDetail = Order.Rest.Contracts.Abstractions.PurchaseDetail;
using UserInfo = Order.Rest.Contracts.Abstractions.User;
using ApplicationContractsOrder = Order.Application.Contracts.Abstractions.Order;

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

        CreateMap<OrderStatus, PurchaseStatus>();

        CreateMap<Customer, UserInfo>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.CustomerId))
            .ForMember(src => src.FirstName, opts => opts.MapFrom(dest => dest.GivenName))
            .ForMember(src => src.LastName, opts => opts.MapFrom(dest => dest.FamilyName))
            .AfterMap((src, dest) =>
            {
                foreach (var @event in src.Orders)
                {
                    dest.OrderDetail.Add(new PurchaseDetail
                    {
                        Id = @event.OrderId,
                        Details = @event.Details,
                        Price = @event.Price,
                        Status = (PurchaseStatus)@event.Status,
                        Quantity = @event.Quantity
                    });
                }
            });

        CreateMap<IList<Customer>, GetUsersResponse>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));

        CreateMap<GetCustomerByIdResponse, GetUserByIdResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.CustomerId))
            .ForMember(src => src.FirstName, opts => opts.MapFrom(dest => dest.GivenName))
            .ForMember(src => src.LastName, opts => opts.MapFrom(dest => dest.FamilyName))
            .AfterMap((src, dest) =>
            {
                foreach (var @event in src.Orders)
                {
                    dest.OrderDetail.Add(new PurchaseDetail
                    {
                        Id = @event.OrderId,
                        Details = @event.Details,
                        Price = @event.Price,
                        Status = (PurchaseStatus)@event.Status,
                        Quantity = @event.Quantity
                    });
                }
            });


        #endregion

        #region Purchase

        CreateMap<CreatePurchaseRequest, CreateOrder>()
            .ForMember(src => src.CustomerId, opts => opts.MapFrom(dest => dest.UserId))
            .ForMember(src => src.CustomerDetails, opts => opts.Ignore())
            .ForMember(src => src.Status, opts => opts.MapFrom(dest => dest.Status));
        
        CreateMap<CreateOrderResponse, CreatePurchaseResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.OrderId));
        
        CreateMap<ApplicationContractsOrder, Order.Rest.Contracts.Purchase.GetPurchases.PurchaseDetail>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.CustomerId))
            .ForPath(dest => dest.UserInfo.FirstName, opts => opts.MapFrom(src => src.CustomerDetails.GivenName))
            .ForPath(dest => dest.UserInfo.LastName, opts => opts.MapFrom(src => src.CustomerDetails.FamilyName))
            .ForPath(dest => dest.UserInfo.Address, opts => opts.MapFrom(src => src.CustomerDetails.Address));

        CreateMap<IList<ApplicationContractsOrder>, GetPurchasesResponse>()
            .ForMember(dest => dest.Purchases, opt => opt.MapFrom(src => src));
        
        #endregion
    }
}
