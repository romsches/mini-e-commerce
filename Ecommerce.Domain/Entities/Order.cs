namespace Ecommerce.Domain.Entities;

/**
 * Enterprise Domain Order Aggregate Root.
 * Represents a completed purchase order adhering to Domain-Driven Design (DDD) principles,
 * encapsulating order lifecycle status, state invariants, and calculated aggregate prices.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Repräsentiert eine Bestellung im E-Commerce-System (Domain-Driven Design).
/// </summary>
public class Order
{
    /// <summary>
    /// Gets the unique identifier of the order.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the unique user identifier associated with the order.
    /// </summary>
    public string UserId { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the order was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the current status of the order (e.g., Pending, Completed, Cancelled).
    /// </summary>
    public string Status { get; private set; } = "Pending";

    private readonly List<OrderItem> _items = new();

    /// <summary>
    /// Gets a read-only collection of order line items.
    /// </summary>
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Gets the calculated aggregate total price for all items in the order.
    /// </summary>
    public decimal TotalPrice => _items.Sum(i => i.TotalPrice);

    private Order() { } // Required for EF Core

    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class with a user identifier and line items.
    /// </summary>
    /// <param name="userId">The unique identifier of the ordering user.</param>
    /// <param name="items">The collection of order items.</param>
    public Order(string userId, IEnumerable<OrderItem> items)
    {
        UserId = !string.IsNullOrWhiteSpace(userId) ? userId : throw new ArgumentException("UserId cannot be null or empty.");
        _items = items?.ToList() ?? throw new ArgumentNullException(nameof(items));

        if (!_items.Any())
        {
            throw new InvalidOperationException("Order must contain at least one item.");
        }
    }
}