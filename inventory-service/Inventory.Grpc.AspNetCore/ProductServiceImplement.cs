using Inventory.Data.Contracts;
using Inventory.Grpc.Contracts;
using Product = Inventory.Grpc.Contracts.Product;

namespace Inventory.Grpc.AspNetCore;

public class ProductServiceImplement : IProductService
{
    private readonly IDataRepository _dataRepository;

    public ProductServiceImplement(IDataRepository dataRepository) => _dataRepository = dataRepository;

    public async Task<GetProductResponse> GetProduct(GetProductRequest request)
    {
        if (string.IsNullOrEmpty(request.ProductId))
        {
            throw new ArgumentException("Product ID cannot be empty");
        }

        if (!Guid.TryParse(request.ProductId, out var productId))
        {
            throw new ArgumentException("Invalid Product ID format");
        }

        var product = await _dataRepository.GetProductById(productId);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");
        }

        return new GetProductResponse
        {
            Product = new Product
            {
                ProductId = product.ProductId.ToString(),
                Name = product.Name,
                Price = (double)product.Price,
                Stock = product.Stock,
                Details = product.Details ?? string.Empty
            }
        };
    }
}
