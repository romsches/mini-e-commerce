using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Products.Commands;

/**
 * Enterprise Delete Product Command & Handler.
 * Implements CQRS command pattern for removing a product from the catalog 
 * with validation checks ensuring the entity exists before deletion.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// CQRS Command to delete an existing product from the e-commerce catalog by its unique identifier.
/// </summary>
/// <param name="Id">The unique GUID of the product to delete.</param>
public record DeleteProductCommand(Guid Id) : IRequest<Unit>;

/// <summary>
/// Handler for processing the <see cref="DeleteProductCommand"/>, verifying existence, and persisting deletion.
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Handles the DeleteProduct command by fetching the product, ensuring it exists, and deleting it from storage.
    /// </summary>
    /// <param name="request">The command payload containing the product identifier.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>Unit value upon successful completion.</returns>
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Produkt mit ID '{request.Id}' wurde nicht gefunden.");

        await _productRepository.DeleteAsync(product, cancellationToken);

        return Unit.Value;
    }
}