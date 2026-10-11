using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Products.Commands;

/**
 * Enterprise Create Product Command & Handler.
 * Implements CQRS command pattern for instantiating and persisting new catalog product entities
 * with validated domain rules.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// CQRS Command to create a new product in the e-commerce system.
/// Returns the unique identifier (GUID) of the product upon successful creation.
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Description">The detailed description.</param>
/// <param name="Price">The valid selling price.</param>
/// <param name="StockQuantity">The initial stock quantity.</param>
public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
) : IRequest<Guid>;

/// <summary>
/// Handler for processing the <see cref="CreateProductCommand"/> and persisting product data.
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Instantiates a new product entity based on command data and saves it.
    /// </summary>
    /// <param name="request">The command payload containing product details.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The ID of the newly created product.</returns>
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity
        );

        await _productRepository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}