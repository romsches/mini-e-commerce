import api from './api';
import type { Cart } from '../types/cart';

/**
 * Enterprise Cart Service.
 * Handles shopping cart synchronization, item manipulation, and graceful 404 fallbacks.
 * Standards: Senior Full-Stack Architecture (2026)
 */
const DEFAULT_USER_ID = 'default-user';

export const cartService = {
    /**
     * Retrieves the current user's shopping cart.
     * Returns an empty cart structure gracefully if the server returns 404 (Not Found).
     */
    async getCart(): Promise<Cart> {
        try {
            const response = await api.get<Cart>(`/carts/${DEFAULT_USER_ID}`);
            return response.data;
        } catch (error: any) {
            if (error.response && error.response.status === 404) {
                return { id: '', items: [], totalPrice: 0 };
            }
            throw error;
        }
    },

    /**
     * Adds a specified quantity of a product to the cart.
     */
    async addToCart(productId: string, quantity: number = 1): Promise<void> {
        await api.post(`/carts/${DEFAULT_USER_ID}/items`, {
            productId,
            quantity,
        });
    },

    /**
     * Removes an item from the cart by its product identifier.
     */
    async removeFromCart(productId: string): Promise<void> {
        await api.delete(`/carts/${DEFAULT_USER_ID}/items/${productId}`);
    }
};

export default cartService;