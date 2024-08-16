using AutoMapper;
using Order.Application.Contracts.Abstractions;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.GetCustomerById;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Application.Contracts.Order.GetOrderById;
using Order.Rest.Contracts.Abstractions;
using Order.Rest.Contracts.Purchase.CreatePurchase;
using Order.Rest.Contracts.Purchase.GetPurchaseByIdResponse;
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
            .ForMember(src => src.ProductId, opts => opts.MapFrom(dest => dest.ItemId))
            .ForMember(src => src.Status, opts => opts.MapFrom(dest => dest.Status));
        
        CreateMap<CreateOrderResponse, CreatePurchaseResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.OrderId));

        CreateMap<ApplicationContractsOrder, Order.Rest.Contracts.Purchase.GetPurchases.PurchaseDetail>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ItemId, opts => opts.MapFrom(src => src.ProductId))
            .ForPath(dest => dest.UserInfo.FirstName, opts => opts.MapFrom(src => src.CustomerDetails.GivenName))
            .ForPath(dest => dest.UserInfo.LastName, opts => opts.MapFrom(src => src.CustomerDetails.FamilyName))
            .ForPath(dest => dest.UserInfo.Address, opts => opts.MapFrom(src => src.CustomerDetails.Address))
            .ForPath(dest => dest.ItemInfo.Name, opts => opts.MapFrom(src => src.ProductInfo.Name))
            .ForPath(dest => dest.ItemInfo.Description, opts => opts.MapFrom(src => src.ProductInfo.Details));

        CreateMap<IList<ApplicationContractsOrder>, GetPurchasesResponse>()
            .ForMember(dest => dest.Purchases, opt => opt.MapFrom(src => src));

        CreateMap<GetOrderByIdResponse, GetPurchaseByIdResponse>()
            .ForMember(src => src.Id, opts => opts.MapFrom(dest => dest.OrderId))
            .ForMember(src => src.ItemId, opts => opts.MapFrom(dest => dest.ProductId))
            .ForPath(src => src.UserInfo.FirstName, opts => opts.MapFrom(dest => dest.CustomerDetails.GivenName))
            .ForPath(src => src.UserInfo.LastName, opts => opts.MapFrom(dest => dest.CustomerDetails.FamilyName))
            .ForPath(src => src.UserInfo.Address, opts => opts.MapFrom(dest => dest.CustomerDetails.Address))
            .ForPath(src => src.ItemInfo.Name, opts => opts.MapFrom(dest => dest.ProductInfo.Name))
            .ForPath(src => src.ItemInfo.Description, opts => opts.MapFrom(dest => dest.ProductInfo.Details));

        #endregion

    }
}
