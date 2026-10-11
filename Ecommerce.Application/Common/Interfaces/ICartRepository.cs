using DomainCart = Ecommerce.Domain.Entities.Cart;

namespace Ecommerce.Application.Common.Interfaces;

/**
 * Enterprise Cart Repository Interface.
 * Defines the persistence contract for shopping cart data access, lifecycle management,
 * and state synchronization.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
public interface ICartRepository
{
    /// <summary>
    /// Retrieves a shopping cart associated with the specified user or session identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    /// <returns>The cart domain entity if found; otherwise, null.</returns>
    Task<DomainCart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persists a newly created shopping cart into storage.
    /// </summary>
    /// <param name="cart">The cart entity to add.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    Task AddAsync(DomainCart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing shopping cart's state in the underlying database.
    /// </summary>
    /// <param name="cart">The cart entity with updated state.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    Task UpdateAsync(DomainCart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a shopping cart from storage (e.g., following a successful checkout transaction).
    /// </summary>
    /// <param name="cart">The cart entity to delete.</param>
    /// <param name="cancellationToken">Propagation token for async operations.</param>
    Task DeleteAsync(DomainCart cart, CancellationToken cancellationToken = default);
}