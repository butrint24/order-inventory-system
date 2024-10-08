﻿namespace Order.Messaging.Contracts;

public class PublishOrder
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string Event { get; set; }
}
