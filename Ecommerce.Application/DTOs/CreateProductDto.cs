namespace Ecommerce.Application.DTOs;

/**
 * Enterprise Create Product Data Transfer Object.
 * Represents the immutable record payload for creating a new product in the catalog.
 * Standards: Senior Backend Architecture (.NET / C# - 2026)
 */
/// <summary>
/// Data Transfer Object (DTO) für die Erstellung eines neuen Produkts im E-Commerce-System.
/// Dient als typsichere Kapselung der eingehenden HTTP-Request-Daten.
/// </summary>
/// <param name="Name">Die offizielle Bezeichnung des Produkts.</param>
/// <param name="Description">Eine detaillierte Beschreibung der Produkteigenschaften.</param>
/// <param name="Price">Der gültige Verkaufspreis des Produkts.</param>
/// <param name="StockQuantity">Die initiale Stückzahl im Lagerbestand.</param>
public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity);