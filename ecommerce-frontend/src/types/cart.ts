/**
 * Enterprise Shopping Cart Item Interface.
 * Represents an individual line item inside a shopping cart.
 * Standards: Senior Full-Stack Architecture (2026)
 */
export interface CartItem {
    readonly id: string;
    readonly productId: string;
    readonly productName: string;
    readonly quantity: number;
    readonly unitPrice: number;
    readonly totalPrice: number;
}

/**
 * Enterprise Shopping Cart Interface.
 * Represents the aggregate shopping cart entity containing line items and total pricing.
 */
export interface Cart {
    readonly id: string;
    readonly items: CartItem[];
    readonly totalPrice: number;
}