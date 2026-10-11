using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Implementation of the <see cref="ICartRepository"/> interface for relational data access 
/// of shopping carts using Entity Framework Core and PostgreSQL.
/// </summary>
public class CartRepository : ICartRepository
{
    private readonly EcommerceDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartRepository"/> class.
    /// </summary>
    /// <param name="context">The database context for the e-commerce system.</param>
    public CartRepository(EcommerceDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<Cart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cart);

        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cart);

        // Clear the change tracker from previously tracked entities to prevent conflicting UPDATE statements
        _context.ChangeTracker.Clear();

        // Load the current cart from the database into a clean context
        var existingCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cart.Id, cancellationToken);

        if (existingCart != null)
        {
            _context.CartItems.RemoveRange(existingCart.Items);
            _context.Carts.Remove(existingCart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Save the new up-to-date state of the cart
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cart);

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
    }
}