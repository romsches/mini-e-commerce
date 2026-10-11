using Ecommerce.Application.Common.Interfaces;
using MediatR;

namespace Ecommerce.Application.Cart.Commands;

/**
 * Enterprise Remove Cart Item Command & Handler.
 * Implements CQRS command pattern for removing specific product line items from a user's shopping cart.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public record RemoveCartItemCommand(string UserId, Guid ProductId) : IRequest<Unit>;

public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, Unit>
{
    private readonly ICartRepository _cartRepository;

    public RemoveCartItemCommandHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    /// <summary>
    /// Handles the RemoveCartItem command by retrieving the user's cart, 
    /// executing item removal via domain logic, and persisting the updated state.
    /// </summary>
    /// <param name="request">The command payload containing user ID and product ID to remove.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>Unit value upon successful execution.</returns>
    public async Task<Unit> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.UserId);

        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (cart != null)
        {
            cart.RemoveItem(request.ProductId);
            await _cartRepository.UpdateAsync(cart, cancellationToken);
        }

        return Unit.Value;
    }
}