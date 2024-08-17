namespace Order.Grpc.Contracts;

public interface IProductDataClient
{
    Product GetProduct(string productId);
}
