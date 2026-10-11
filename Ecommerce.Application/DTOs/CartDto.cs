namespace Ecommerce.Application.DTOs;

/**
 * Enterprise Cart Data Transfer Object.
 * Represents the structured client-facing payload for a user's active shopping cart,
 * including calculated aggregate totals.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public class CartDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the shopping cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique user identifier associated with the cart.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of line items currently contained in the cart.
    /// </summary>
    public List<CartItemDto> Items { get; set; } = new();

    /// <summary>
    /// Gets the calculated aggregate total price of all items in the cart.
    /// </summary>
    public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
}

/**
 * Enterprise Cart Item Data Transfer Object.
 * Represents an individual product line item inside the shopping cart.
 */
public class CartItemDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart line item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the display name of the product snapshot.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product ordered.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the individual unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the calculated total price for this line item (Quantity multiplied by UnitPrice).
    /// </summary>
    public decimal TotalPrice => Quantity * UnitPrice;
}

/**
 * Enterprise Add To Cart Request Data Transfer Object.
 * Represents the client payload required to add a product item to the shopping cart.
 */
public class AddToCartDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the product to add.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity to add to the cart.
    /// </summary>
    public int Quantity { get; set; }
}