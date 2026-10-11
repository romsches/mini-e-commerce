namespace Ecommerce.Domain.Entities;

/**
 * Enterprise Domain Cart Item Entity.
 * Represents an individual line item within a customer shopping cart,
 * enforcing item-level invariants such as positive quantities and non-negative unit prices.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Repräsentiert eine einzelne Position (einen Artikel) innerhalb des Warenkorbs.
/// </summary>
public class CartItem
{
    /// <summary>
    /// Gets the unique identifier of the cart item.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the parent shopping cart.
    /// </summary>
    public Guid CartId { get; private set; }

    /// <summary>
    /// Gets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Gets the quantity of the product ordered in this line item.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price of the product at the time it was added to the cart.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    // Navigation Property
    /// <summary>
    /// Gets the parent cart navigation property.
    /// </summary>
    public Cart? Cart { get; private set; }

    private CartItem() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="CartItem"/> class.
    /// </summary>
    /// <param name="cartId">The parent cart identifier.</param>
    /// <param name="productId">The product identifier.</param>
    /// <param name="quantity">The initial quantity.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    public CartItem(Guid cartId, Guid productId, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        CartId = cartId;
        ProductId = productId;
        UpdateQuantity(quantity);
        UnitPrice = unitPrice >= 0 ? unitPrice : throw new ArgumentOutOfRangeException(nameof(unitPrice), "Price cannot be negative.");
    }

    /// <summary>
    /// Updates the quantity of the cart item, verifying that it remains greater than zero.
    /// </summary>
    /// <param name="quantity">The new quantity to set.</param>
    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }
        Quantity = quantity;
    }
}