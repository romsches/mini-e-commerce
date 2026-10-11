using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Common.Interfaces;

/**
 * Enterprise Order Repository Interface.
 * Defines persistence contract for order management, retrieval, and administrative queries.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public interface IOrderRepository
{
    /// <summary>
    /// Retrieves a specific order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique GUID of the order.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>The order entity if found; otherwise, null.</returns>
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all orders associated with a specific user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>A collection of orders belonging to the user.</returns>
    Task<IEnumerable<Order>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persists a new order entity into storage.
    /// </summary>
    /// <param name="order">The order entity to add.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all orders across the entire system for administrative oversight.
    /// </summary>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>A collection of all system orders.</returns>
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
}