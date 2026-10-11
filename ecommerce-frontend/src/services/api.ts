import axios, { type AxiosInstance, type AxiosError } from 'axios';

/**
 * Enterprise-grade Axios HTTP client instance.
 * Configured with centralized base URL, headers, and interceptor-ready architecture.
 * Standards: Senior Full-Stack Architecture (2026)
 */
const api: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5083/api',
    headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
    },
    timeout: 10000, // 10 seconds timeout for enterprise stability
});

// Request Interceptor: Optional telemetry, auth headers, or request logging
api.interceptors.request.use(
    (config) => {
        // Future extension: Inject Bearer tokens here if Auth is enabled
        return config;
    },
    (error: AxiosError) => {
        return Promise.reject(error);
    }
);

// Response Interceptor: Centralized error logging and response formatting
api.interceptors.response.use(
    (response) => response,
    (error: AxiosError) => {
        // Centralized error handling structure for enterprise applications
        const statusCode = error.response?.status;
        const errorMessage = error.message || 'Unknown network error occurred.';

        console.error(`[API Error] Status: ${statusCode || 'N/A'} - Message: ${errorMessage}`);

        return Promise.reject(error);
    }
);

export default api;