using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Cart.Queries;

/**
 * Enterprise Get Cart By User ID Query & Handler.
 * Implements CQRS query pattern for retrieving and projecting a user's shopping cart 
 * along with enriched product names and calculated totals.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public record GetCartByUserIdQuery(string UserId) : IRequest<CartDto?>;

public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, CartDto?>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public GetCartByUserIdQueryHandler(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Handles the GetCartByUserId query by fetching the cart, mapping domain items 
    /// with current product catalog information, and returning a fully populated DTO.
    /// </summary>
    /// <param name="request">The query payload containing the user identifier.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>The cart data transfer object, or an empty cart DTO if none exists.</returns>
    public async Task<CartDto?> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.UserId);

        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        // Fallback: If no cart exists in storage, return an empty cart DTO to maintain frontend stability.
        if (cart == null)
        {
            return new CartDto
            {
                Id = Guid.Empty,
                UserId = request.UserId,
                Items = new List<CartItemDto>()
            };
        }

        var itemDtos = new List<CartItemDto>();

        foreach (var item in cart.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);

            itemDtos.Add(new CartItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = product?.Name ?? "Unbekanntes Produkt",
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = itemDtos
        };
    }
}