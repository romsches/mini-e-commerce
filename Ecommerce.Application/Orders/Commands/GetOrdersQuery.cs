using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Orders.Queries;

public record GetOrdersQuery : IRequest<IEnumerable<OrderDto>>;

public class GetOrdersQueryHandler
    : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository
            ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<IEnumerable<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        return orders.Select(o =>
        {
            var items = o.Items.Select(i => new OrderItemDto(
                i.Id,
                i.ProductId,
                i.ProductName,
                i.Quantity,
                i.UnitPrice,
                i.Quantity * i.UnitPrice
            )).ToList();

            return new OrderDto(
                o.Id,
                o.UserId,
                o.CreatedAt,
                o.Status,
                items.Sum(i => i.TotalPrice),
                items
            );
        });
    }
}