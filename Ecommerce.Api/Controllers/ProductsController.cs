using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Products.Commands;
using Ecommerce.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

/**
 * Enterprise Products Controller.
 * Manages product catalog lifecycle operations (Full CRUD) using CQRS via MediatR.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Retrieves all available products from the catalog.
    /// </summary>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>A collection of product data transfer objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a specific product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique GUID identifier of the product.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>The product data transfer object if found; otherwise, 404 Not Found.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound($"Produkt mit der ID '{id}' wurde nicht gefunden.");
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new product in the catalog.
    /// </summary>
    /// <param name="command">The command payload containing product details.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>A 201 Created response containing the new product identifier and location header.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = productId }, new { Id = productId });
    }

    /// <summary>
    /// Updates an existing product's details.
    /// </summary>
    /// <param name="id">The unique GUID identifier from the route URL.</param>
    /// <param name="command">The command payload containing updated product data.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>NoContent upon successful update; otherwise, 400 Bad Request if identifiers mismatch.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest("Die ID in der URL stimmt nicht mit der ID im Body überein.");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes a product from the system catalog.
    /// </summary>
    /// <param name="id">The unique GUID identifier of the product to delete.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>NoContent upon successful deletion.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}