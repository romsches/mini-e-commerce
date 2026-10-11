using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Products.Queries;

/**
 * Enterprise Get Product By ID Query & Handler.
 * Implements CQRS query pattern for retrieving a specific catalog product entity 
 * by its unique identifier.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// CQRS Query to retrieve a specific product by its unique identifier.
/// </summary>
/// <param name="Id">The unique GUID of the product to retrieve.</param>
public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;

/// <summary>
/// Handler for processing the <see cref="GetProductByIdQuery"/> via MediatR.
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Executes the query and searches for the product in the data source.
    /// </summary>
    /// <param name="request">The query containing the requested product ID.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The found product entity, or null if no entry exists.</returns>
    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}