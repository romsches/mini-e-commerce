namespace Ecommerce.Domain.Entities;

/**
 * Enterprise Domain Product Aggregate Root.
 * Represents a product entity at the core of the e-commerce domain model (Domain-Driven Design).
 * Encapsulates business state and enforces invariant rules across all updates and stock operations.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Represents a product entity at the core of the e-commerce domain model (Domain-Driven Design).
/// Encapsulates business state and ensures that invariants are always maintained.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the detailed product description.
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the valid selling price of the product.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Gets the current stock quantity available in the warehouse.
    /// </summary>
    public int StockQuantity { get; private set; }

    /// <summary>
    /// Gets the timestamp when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the timestamp when the product was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Parameterless private constructor for materialization-compliant usage by Entity Framework Core.
    /// </summary>
    private Product() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> entity with initial values.
    /// </summary>
    /// <param name="name">The product name.</param>
    /// <param name="description">The detailed product description.</param>
    /// <param name="price">The valid selling price.</param>
    /// <param name="stockQuantity">The initial stock quantity.</param>
    public Product(string name, string description, decimal price, int stockQuantity)
    {
        Id = Guid.NewGuid();
        UpdateDetails(name, description, price);
        UpdateStock(stockQuantity);
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the core details of the product while validating business invariants.
    /// </summary>
    /// <param name="name">The new product name.</param>
    /// <param name="description">The new description.</param>
    /// <param name="price">The new selling price.</param>
    public void UpdateDetails(string name, string description, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Product price cannot be negative.");
        }

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adjusts the current stock quantity of the product.
    /// </summary>
    /// <param name="stockQuantity">The new item count in stock.</param>
    public void UpdateStock(int stockQuantity)
    {
        if (stockQuantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stock quantity cannot be negative.");
        }

        StockQuantity = stockQuantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates all product details and stock quantity in a single operation.
    /// </summary>
    /// <param name="name">The new product name.</param>
    /// <param name="description">The new description.</param>
    /// <param name="price">The new selling price.</param>
    /// <param name="stockQuantity">The new stock quantity.</param>
    public void Update(string name, string description, decimal price, int stockQuantity)
    {
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.", nameof(name));
        Description = description ?? string.Empty;
        Price = price >= 0 ? price : throw new ArgumentException("Price cannot be negative.", nameof(price));
        StockQuantity = stockQuantity >= 0 ? stockQuantity : throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Decreases the stock quantity during an order placement.
    /// </summary>
    /// <param name="quantity">The quantity to subtract.</param>
    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        if (StockQuantity < quantity)
        {
            throw new InvalidOperationException($"Insufficient stock for product '{Name}' (Available: {StockQuantity}, Requested: {quantity}).");
        }

        StockQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}