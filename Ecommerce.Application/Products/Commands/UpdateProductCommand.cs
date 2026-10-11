using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Products.Commands;

/**
 * Enterprise Update Product Command & Handler.
 * Implements CQRS command pattern for modifying existing catalog product entities
 * with domain-driven invariant validation and persistence synchronization.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// CQRS Command to update an existing product in the e-commerce catalog.
/// </summary>
/// <param name="Id">The unique identifier of the product to update.</param>
/// <param name="Name">The updated product name.</param>
/// <param name="Description">The updated product description.</param>
/// <param name="Price">The updated valid selling price.</param>
/// <param name="StockQuantity">The updated stock quantity.</param>
public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity) : IRequest<Unit>;

/// <summary>
/// Handler for processing the <see cref="UpdateProductCommand"/>, fetching the aggregate root,
/// invoking domain update logic, and persisting changes.
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Handles the UpdateProduct command by retrieving the product, applying updates through domain methods, and saving changes.
    /// </summary>
    /// <param name="request">The command payload containing updated product fields.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>Unit value upon successful completion.</returns>
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Produkt mit ID '{request.Id}' wurde nicht gefunden.");

        product.Update(request.Name, request.Description, request.Price, request.StockQuantity);

        await _productRepository.UpdateAsync(product, cancellationToken);

        return Unit.Value;
    }
}