using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Common.Interfaces;

/// <summary>
/// Definiert die Datenzugriffsschnittstelle (Repository-Pattern) für Produkt-Entitäten 
/// innerhalb der Application-Schicht gemäß den Prinzipien der Clean Architecture.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Ruft ein spezifisches Produkt anhand seiner eindeutigen ID asynchron ab.
    /// </summary>
    /// <param name="id">Eindeutige Produkt-ID (GUID).</param>
    /// <param name="cancellationToken">Token zur Unterbrechung des asynchronen Vorgangs.</param>
    /// <returns>Die gefundene Produkt-Entität oder null, falls nicht existent.</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ruft eine unveränderbare Liste aller verfügbaren Produkte asynchron ab.
    /// </summary>
    /// <param name="cancellationToken">Token zur Unterbrechung des asynchronen Vorgangs.</param>
    /// <returns>Eine schreibgeschützte Auflistung von Produkten.</returns>
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Fügt ein neues Produkt dem Persistenzkontext hinzu.
    /// </summary>
    /// <param name="product">Die zu erstellende Produkt-Entität.</param>
    /// <param name="cancellationToken">Token zur Unterbrechung des asynchronen Vorgangs.</param>
    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Aktualisiert die Daten eines bestehenden Produkts im Persistenzkontext.
    /// </summary>
    /// <param name="product">Die zu aktualisierende Produkt-Entität.</param>
    /// <param name="cancellationToken">Token zur Unterbrechung des asynchronen Vorgangs.</param>
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Entfernt ein Produkt aus dem Persistenzkontext.
    /// </summary>
    /// <param name="product">Die zu löschende Produkt-Entität.</param>
    /// <param name="cancellationToken">Token zur Unterbrechung des asynchronen Vorgangs.</param>
    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
}