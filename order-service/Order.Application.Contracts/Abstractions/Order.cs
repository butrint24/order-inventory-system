﻿using System.Net.Http.Headers;

namespace Order.Application.Contracts.Abstractions;

public class Order
{
    public Guid OrderId { get; set; }

    public string? Details { get; set; }

    public int Quantity { get; set; }

    public OrderStatus Status { get; set; }

    public decimal Price { get; set; }

    public Guid CustomerId { get; set; }

    public Customer CustomerDetails { get; set; }

    public Guid ProductId { get; set; }

    public ProductInfo ProductInfo { get; set; }
}
