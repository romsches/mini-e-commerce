using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Infrastructure.Persistence.Repositories;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure;

/**
 * Enterprise Infrastructure Dependency Injection Extension.
 * Provides extension methods for centralizing the registration of infrastructure-layer services,
 * Entity Framework Core database contexts, and repository patterns into the DI container.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Provides extension methods for centralizing the registration of all infrastructure and persistence services 
/// into the dependency injection container (Clean Architecture).
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the PostgreSQL database context along with corresponding repository implementations.
    /// </summary>
    /// <param name="services">The service collection of the dependency injection container.</param>
    /// <param name="configuration">The application configuration used to retrieve connection strings.</param>
    /// <returns>The modified service collection for method chaining.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        // Register the Entity Framework Core DbContext with PostgreSQL (Npgsql)
        services.AddDbContext<EcommerceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Register the Product repository for domain-specific data access
        services.AddScoped<IProductRepository, ProductRepository>();

        // Register the Cart repository for domain-specific data access
        services.AddScoped<ICartRepository, CartRepository>();

        // Register the Order repository for domain-specific data access
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}