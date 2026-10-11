using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Products.Queries;

/**
 * Enterprise Get Products Query & Handler.
 * Implements CQRS query pattern for retrieving a complete, read-only list 
 * of all catalog product entities registered in the system.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// CQRS Query to retrieve a complete list of all products registered in the system.
/// </summary>
public record GetProductsQuery : IRequest<IReadOnlyList<Product>>;

/// <summary>
/// Handler for processing the <see cref="GetProductsQuery"/> via MediatR.
/// </summary>
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IReadOnlyList<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Executes the query and returns a read-only list of all products.
    /// </summary>
    /// <param name="request">The query to execute.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>An immutable collection of products.</returns>
    public async Task<IReadOnlyList<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync(cancellationToken);
    }
}