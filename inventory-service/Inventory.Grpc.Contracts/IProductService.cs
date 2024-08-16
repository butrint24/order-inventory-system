namespace Inventory.Grpc.Contracts;

public interface IProductService
{
    Task<GetProductResponse> GetProduct(GetProductRequest request);
}
