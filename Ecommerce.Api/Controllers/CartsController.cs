using Ecommerce.Application.Cart.Commands;
using Ecommerce.Application.Cart.Queries;
using Ecommerce.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

/**
 * Enterprise Carts Controller.
 * Manages shopping cart lifecycle, item manipulation, and CQRS message dispatching via MediatR.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CartsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Retrieves the shopping cart for a specific user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>The cart data transfer object if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartDto>> GetCart(string userId, CancellationToken cancellationToken)
    {
        var query = new GetCartByUserIdQuery(userId);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound($"Warenkorb für Benutzer '{userId}' nicht gefunden.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Adds a product item to the user's cart or increments its quantity.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="dto">The payload containing product identifier and quantity.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>NoContent upon successful operation.</returns>
    [HttpPost("{userId}/items")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItem(string userId, [FromBody] AddToCartDto dto, CancellationToken cancellationToken)
    {
        var command = new AddToCartCommand(userId, dto.ProductId, dto.Quantity);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Removes a specific product item from the user's shopping cart.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="productId">The unique identifier of the product to remove.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>NoContent upon successful operation.</returns>
    [HttpDelete("{userId}/items/{productId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItem(string userId, Guid productId, CancellationToken cancellationToken)
    {
        var command = new RemoveCartItemCommand(userId, productId);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}