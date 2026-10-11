using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Orders.Queries;

/// <summary>
/// Query to retrieve all customer orders within the e-commerce system.
/// </summary>
public record GetOrdersQuery : IRequest<IEnumerable<OrderDto>>;

/// <summary>
/// Handler for processing the <see cref="GetOrdersQuery"/> request and mapping domain entities to DTOs.
/// </summary>
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<IEnumerable<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        return orders.Select(o =>
        {
            var items = o.Items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.Quantity * i.UnitPrice
            }).ToList();

            return new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalPrice = items.Sum(i => i.TotalPrice),
                Items = items
            };
        });
    }
}