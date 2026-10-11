namespace Ecommerce.Application.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();

    public OrderDto() { }

    public OrderDto(Guid id, string userId, DateTime createdAt, string status, decimal totalPrice, List<OrderItemDto> items)
    {
        Id = id;
        UserId = userId;
        CreatedAt = createdAt;
        Status = status;
        TotalPrice = totalPrice;
        Items = items;
    }
}

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public OrderItemDto() { }

    public OrderItemDto(Guid id, Guid productId, string productName, int quantity, decimal unitPrice, decimal totalPrice)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
    }
}