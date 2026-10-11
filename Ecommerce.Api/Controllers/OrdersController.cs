using Ecommerce.Application.DTOs;
using Ecommerce.Application.Orders.Commands;
using Ecommerce.Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

/**
 * Enterprise Orders Controller.
 * Manages customer purchase order creation from active carts and administrative order tracking.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Retrieves all orders within the system for administrative oversight.
    /// </summary>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>A collection of order data transfer objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders(CancellationToken cancellationToken)
    {
        var query = new GetOrdersQuery();
        var orders = await _mediator.Send(query, cancellationToken);
        return Ok(orders);
    }

    /// <summary>
    /// Creates a new purchase order based on the user's current shopping cart contents.
    /// </summary>
    /// <param name="userId">The unique identifier of the user checking out.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>An object containing the newly generated order identifier.</returns>
    [HttpPost("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Guid>> CreateOrder(string userId, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(userId);
        var orderId = await _mediator.Send(command, cancellationToken);

        return Ok(new { OrderId = orderId });
    }
}