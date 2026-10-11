using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Implementierung des <see cref="ICartRepository"/> für den relationalen Datenzugriff
/// auf Warenkörbe mittels Entity Framework Core und PostgreSQL.
/// </summary>
public class CartRepository : ICartRepository
{
    private readonly EcommerceDbContext _context;

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

        // Очищаем трекер от предыдущих отслеживаемых сущностей, чтобы избежать конфликтных UPDATE
        _context.ChangeTracker.Clear();

        // Загружаем актуальную корзину из базы заново в чистый контекст
        var existingCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cart.Id, cancellationToken);

        if (existingCart != null)
        {
            _context.CartItems.RemoveRange(existingCart.Items);
            _context.Carts.Remove(existingCart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Сохраняем новое актуальное состояние корзины
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