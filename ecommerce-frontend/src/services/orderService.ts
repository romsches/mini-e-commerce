import api from './api';

/**
 * Enterprise Order Item Data Transfer Object.
 * Represents a snapshot of an ordered product item.
 */
export interface OrderItemDto {
    readonly id: string;
    readonly productId: string;
    readonly productName: string;
    readonly quantity: number;
    readonly unitPrice: number;
    readonly totalPrice: number;
}

/**
 * Enterprise Order Data Transfer Object.
 * Represents a finalized customer purchase order.
 */
export interface OrderDto {
    readonly id: string;
    readonly userId: string;
    readonly createdAt: string;
    readonly status: string;
    readonly totalPrice: number;
    readonly items: OrderItemDto[];
}

const DEFAULT_USER_ID = 'default-user';

/**
 * Enterprise Order Service.
 * Manages order creation from active carts and administrative order retrieval.
 * Standards: Senior Full-Stack Architecture (2026)
 */
export const orderService = {
    /**
     * Creates a new order for the user based on their current shopping cart contents.
     */
    async createOrder(): Promise<OrderDto> {
        const response = await api.post<OrderDto>(`/orders/${DEFAULT_USER_ID}`);
        return response.data;
    },

    /**
     * Retrieves all system orders for administrative oversight.
     */
    async getAllOrders(): Promise<OrderDto[]> {
        const response = await api.get<OrderDto[]>('/orders');
        return response.data;
    }
};

export default orderService;