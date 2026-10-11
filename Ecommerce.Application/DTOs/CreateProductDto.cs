namespace Ecommerce.Application.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for creating a new product within the e-commerce system.
/// Represents the immutable payload object for the product catalog.
/// </summary>
/// <param name="Name">The official designation of the product.</param>
///<param name="Description">A detailed description of the product features.</param>
/// <param name="Price">The valid retail price of the product.</param>
/// <param name="StockQuantity">The initial stock quantity in inventory.</param>
public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity);