namespace Ecommerce.Domain.Entities;

/// <summary>
/// Represents an individual line item snapshot within an order aggregate,
/// encapsulating product identification, purchased quantities, historical unit prices,
/// and computed total pricing invariants.
/// Standards: Senior Backend Architecture (.NET / C# - 2026)
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Gets the unique identifier of the order item.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the unique identifier of the parent order.
    /// </summary>
    public Guid OrderId { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the ordered product.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Gets the historical name of the product at the time of purchase.
    /// </summary>
    public string ProductName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the purchased quantity of the product.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price of the product at the time of purchase.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Gets the calculated total price for this order item (Quantity * UnitPrice).
    /// </summary>
    public decimal TotalPrice => Quantity * UnitPrice;

    private OrderItem() { } // Required for EF Core

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderItem"/> class with product details, quantity, and unit price.
    /// </summary>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <param name="productName">The name of the product.</param>
    /// <param name="quantity">The purchased quantity (must be greater than zero).</param>
    /// <param name="unitPrice">The unit price of the product (must be non-negative).</param>
    public OrderItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = !string.IsNullOrWhiteSpace(productName) ? productName : throw new ArgumentException("Product name cannot be null or empty.", nameof(productName));
        Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        UnitPrice = unitPrice >= 0 ? unitPrice : throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));
    }
}