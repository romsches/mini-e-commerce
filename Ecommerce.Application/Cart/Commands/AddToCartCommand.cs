using Ecommerce.Application.Common.Interfaces;
using MediatR;
using DomainCart = Ecommerce.Domain.Entities.Cart;

namespace Ecommerce.Application.Cart.Commands;

/**
 * Enterprise Add To Cart Command & Handler.
 * Implements CQRS command pattern for adding products to a user's shopping cart,
 * enforcing domain invariants, entity creation if non-existent, and persistence synchronization.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public record AddToCartCommand(string UserId, Guid ProductId, int Quantity) : IRequest<Unit>;

public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public AddToCartCommandHandler(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Handles the AddToCart command by verifying product existence, retrieving or initializing the cart,
    /// adding the item according to domain rules, and persisting changes.
    /// </summary>
    /// <param name="request">The command payload containing user ID, product ID, and quantity.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>Unit value upon successful execution.</returns>
    public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.UserId);

        if (request.Quantity <= 0)
        {
            throw new ArgumentException("Menge muss größer als Null sein.", nameof(request.Quantity));
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new KeyNotFoundException($"Produkt mit der ID '{request.ProductId}' wurde nicht gefunden.");

        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (cart == null)
        {
            cart = new DomainCart(request.UserId);
            cart.AddItem(product.Id, request.Quantity, product.Price);
            await _cartRepository.AddAsync(cart, cancellationToken);
        }
        else
        {
            cart.AddItem(product.Id, request.Quantity, product.Price);
            await _cartRepository.UpdateAsync(cart, cancellationToken);
        }

        return Unit.Value;
    }
}