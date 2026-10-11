import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

/**
 * Vite Configuration File
 * Optimized for production-grade React & TypeScript single-page application.
 * Standards: Senior Frontend Architecture (2026)
 */
export default defineConfig({
    plugins: [react()],
    server: {
        port: 5173,
        host: true,
    },
    build: {
        outDir: 'dist',
        sourcemap: false,
        minify: 'esbuild',
    },
});