import api from './api';
import type { Product } from '../types/product';

/**
 * Enterprise Create Product Payload Interface.
 * Defines the contract for product creation requests.
 */
export interface CreateProductDto {
    readonly name: string;
    readonly description: string;
    readonly price: number;
    readonly stockQuantity: number;
}

/**
 * Enterprise Update Product Payload Interface.
 * Extends creation data with a unique identifier.
 */
export interface UpdateProductDto extends CreateProductDto {
    readonly id: string;
}

/**
 * Enterprise Product Service.
 * Provides full CRUD operations for product catalogs with strictly typed responses.
 * Standards: Senior Full-Stack Architecture (2026)
 */
export const productService = {
    /**
     * Retrieves all products available in the catalog.
     */
    async getAll(): Promise<Product[]> {
        const response = await api.get<Product[]>('/products');
        return response.data;
    },

    /**
     * Creates a new product in the catalog.
     */
    async create(data: CreateProductDto): Promise<Product> {
        const response = await api.post<Product>('/products', data);
        return response.data;
    },

    /**
     * Updates an existing product by its unique identifier.
     */
    async update(id: string, data: CreateProductDto): Promise<void> {
        await api.put(`/products/${id}`, { id, ...data });
    },

    /**
     * Deletes a product from the system by its identifier.
     */
    async delete(id: string): Promise<void> {
        await api.delete(`/products/${id}`);
    }
};

export default productService;