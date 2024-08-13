using AutoMapper;
using Order.Application.Contracts.Customer.CreateCustomer;
using Order.Application.Contracts.Customer.DeleteCustomer;
using Order.Application.Contracts.Customer.GetCustomerById;
using Order.Application.Contracts.Order.CreateOrder;
using Order.Application.Contracts.Order.DeleteOrder;
using Order.Application.Contracts.Order.GetOrderById;

namespace Order.Application;

public class MappingRegister : Profile
{
    public MappingRegister()
    {

        #region Customer

        CreateMap<CreateCustomer, Data.Contracts.Customer>();

        CreateMap<Data.Contracts.Customer, CreateCustomerResponse>()
            .ReverseMap();
        
        CreateMap<Data.Contracts.Customer, Contracts.Abstractions.Customer>();
        
        CreateMap<GetCustomerById, Data.Contracts.Customer>();

        CreateMap<Data.Contracts.Customer, GetCustomerByIdResponse>()
            .ReverseMap();
        
        CreateMap<DeleteCustomer, Data.Contracts.Customer>();

        CreateMap<Data.Contracts.Customer, DeleteCustomerResponse>()
            .ReverseMap();

        #endregion

        #region Order

        CreateMap<CreateOrder, Data.Contracts.Order>();

        CreateMap<Data.Contracts.Order, CreateOrderResponse>()
            .ReverseMap();

        CreateMap<Data.Contracts.Order, Application.Contracts.Abstractions.Order>();
        
        CreateMap<GetOrderById, Data.Contracts.Order>();

        CreateMap<Data.Contracts.Order, GetOrderByIdResponse>()
            .ReverseMap();
        
        CreateMap<DeleteOrder, Data.Contracts.Order>();

        CreateMap<Data.Contracts.Order, DeleteOrderResponse>()
            .ReverseMap();

        #endregion

    }
}
