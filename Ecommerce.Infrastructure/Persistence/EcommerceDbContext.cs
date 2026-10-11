using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence;

/**
 * Enterprise Entity Framework Core Database Context.
 * Manages database connections, change tracking, and entity-relational mapping (ORM) 
 * for the e-commerce system database schema.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Represents the Entity Framework Core database context for the e-commerce system.
/// Manages connections, change tracking, and mapping rules of entities to the relational database schema.
/// </summary>
public class EcommerceDbContext : DbContext
{
    /// <summary>
    /// Gets the DbSet for product entities.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Gets the DbSet for shopping cart entities.
    /// </summary>
    public DbSet<Cart> Carts => Set<Cart>();

    /// <summary>
    /// Gets the DbSet for items within the shopping cart.
    /// </summary>
    public DbSet<CartItem> CartItems => Set<CartItem>();

    /// <summary>
    /// Gets the DbSet for order entities.
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Gets the DbSet for items within an order.
    /// </summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <summary>
    /// Initializes a new instance of the <see cref="EcommerceDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {
        ArgumentNullException.ThrowIfNull(options);
    }

    /// <summary>
    /// Configures the relational database schema and applies entity mapping rules and configurations.
    /// </summary>
    /// <param name="modelBuilder">The builder used to define the model mapping.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.StockQuantity)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(e => e.Items)
                .WithOne(i => i.Cart)
                .HasForeignKey(i => i.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.Quantity)
                .IsRequired();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.Quantity)
                .IsRequired();
        });
    }
}