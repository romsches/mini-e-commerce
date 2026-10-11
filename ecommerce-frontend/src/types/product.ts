/**
 * Enterprise Product Interface.
 * Represents a catalog item with precise financial pricing and inventory tracking.
 * Standards: Senior Full-Stack Architecture (2026)
 */
export interface Product {
    readonly id: string;
    readonly name: string;
    readonly description: string;
    readonly price: number;
    readonly stockQuantity: number;
}