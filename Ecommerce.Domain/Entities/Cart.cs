namespace Ecommerce.Domain.Entities;

/**
 * Enterprise Domain Cart Aggregate Root.
 * Represents a customer shopping cart adhering to Domain-Driven Design (DDD) principles,
 * encapsulating business logic, invariant enforcement, and state transitions.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Repräsentiert den Warenkorb eines Kunden im E-Commerce-System (Domain-Driven Design).
/// </summary>
public class Cart
{
    /// <summary>
    /// Gets the unique identifier of the shopping cart.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the unique user identifier or session identifier associated with the cart.
    /// </summary>
    public string UserId { get; private set; } = string.Empty;

    private readonly List<CartItem> _items = new();

    /// <summary>
    /// Gets a read-only collection of items currently in the cart.
    /// </summary>
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Gets the timestamp when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the timestamp when the cart was last modified.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    private Cart() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cart"/> class for a specific user.
    /// </summary>
    /// <param name="userId">The user identifier associated with the cart.</param>
    public Cart(string userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId, nameof(userId));
        Id = Guid.NewGuid();
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a product item to the cart or updates the quantity if the product already exists.
    /// </summary>
    /// <param name="productId">The unique identifier of the product.</param>
    /// <param name="quantity">The quantity to add.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            _items.Add(new CartItem(Id, productId, quantity, unitPrice));
        }

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Removes an item from the cart by its product identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to remove.</param>
    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            _items.Remove(item);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Clears all items from the cart.
    /// </summary>
    public void Clear()
    {
        _items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }
}