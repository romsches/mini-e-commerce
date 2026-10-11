using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Orders.Commands;

/**
 * Enterprise Create Order Command & Handler.
 * Implements CQRS command pattern for transforming an active shopping cart into a verified purchase order,
 * executing atomic stock depletion, inventory validation, order persistence, and cart cleanup.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public record CreateOrderCommand(string UserId) : IRequest<Guid>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderCommandHandler(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    /// <summary>
    /// Handles the CreateOrder command by validating cart contents, checking and decreasing stock invariants,
    /// generating a purchase order entity, persisting the order, and deleting the processed cart.
    /// </summary>
    /// <param name="request">The command payload containing the checkout user identifier.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>The unique GUID identifier of the newly created order.</returns>
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.UserId);

        var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new InvalidOperationException($"Warenkorb für Benutzer '{request.UserId}' nicht gefunden.");

        if (!cart.Items.Any())
        {
            throw new InvalidOperationException("Kann keine Bestellung aus einem leeren Warenkorb erstellen.");
        }

        var orderItems = new List<OrderItem>();

        foreach (var cartItem in cart.Items)
        {
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId, cancellationToken)
                ?? throw new KeyNotFoundException($"Produkt mit ID {cartItem.ProductId} wurde nicht gefunden.");

            // Decrease stock quantity and enforce domain invariants
            product.DecreaseStock(cartItem.Quantity);
            await _productRepository.UpdateAsync(product, cancellationToken);

            orderItems.Add(new OrderItem(cartItem.ProductId, product.Name, cartItem.Quantity, cartItem.UnitPrice));
        }

        var order = new Order(request.UserId, orderItems);

        await _orderRepository.AddAsync(order, cancellationToken);
        await _cartRepository.DeleteAsync(cart, cancellationToken);

        return order.Id;
    }
}