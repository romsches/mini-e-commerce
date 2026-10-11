using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Application;

/**
 * Enterprise Application Dependency Injection Extension.
 * Provides extension methods for centralizing the registration of application-layer services 
 * into the dependency injection container following Clean Architecture principles.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */

/// <summary>
/// Provides extension methods for centralizing the registration of all application layer services 
/// into the dependency injection container (Clean Architecture).
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers cross-cutting application services such as MediatR handlers and validators.
    /// </summary>
    /// <param name="services">The service collection of the dependency injection container.</param>
    /// <returns>The modified service collection for method chaining.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}