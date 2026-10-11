import { useEffect, useState } from 'react';
import type { FormEvent } from 'react';

import api from './services/api';
import { cartService } from './services/cartService';
import { orderService } from './services/orderService';
import type { OrderDto } from './services/orderService';
import { productService } from './services/productService';
import type { CreateProductDto } from './services/productService';
import type { Product } from './types/product';
import type { Cart } from './types/cart';

import './App.css';

function App() {
  const [view, setView] = useState<'shop' | 'admin'>('shop');
  const [products, setProducts] = useState<Product[]>([]);
  const [cart, setCart] = useState<Cart | null>(null);
  const [, setOrders] = useState<OrderDto[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [orderSuccessMessage, setOrderSuccessMessage] = useState<string | null>(null);

  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [formData, setFormData] = useState<CreateProductDto>({
    name: '',
    description: '',
    price: 0,
    stockQuantity: 0,
  });

  const loadData = async () => {
    try {
      setLoading(true);
      const [productsRes, cartRes, ordersRes] = await Promise.all([
        api.get<Product[]>('/products'),
        cartService.getCart(),
        orderService.getAllOrders().catch(() => []),
      ]);
      setProducts(productsRes.data);
      setCart(cartRes);
      setOrders(ordersRes);
      setLoading(false);
    } catch (err) {
      console.error('Failed to load system data:', err);
      setError('Could not establish connection to the server.');
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleAddToCart = async (productId: string) => {
    try {
      setOrderSuccessMessage(null);
      await cartService.addToCart(productId, 1);
      const updatedCart = await cartService.getCart();
      setCart(updatedCart);
    } catch (err) {
      console.error('Failed to add item to cart:', err);
      alert('Error adding item to cart.');
    }
  };

  const handleRemoveFromCart = async (productId: string) => {
    try {
      setOrderSuccessMessage(null);
      await cartService.removeFromCart(productId);
      const updatedCart = await cartService.getCart();
      setCart(updatedCart);
    } catch (err) {
      console.error('Failed to remove item from cart:', err);
      alert('Error removing item from cart.');
    }
  };

  const handleCheckout = async () => {
    try {
      await orderService.createOrder();
      setOrderSuccessMessage('Order placed successfully! Thank you for your purchase.');
      await loadData();
    } catch (err) {
      console.error('Checkout error:', err);
      alert('Error placing the order.');
    }
  };

    const handleFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      if (editingProduct) {
        await productService.update(editingProduct.id, formData);
        alert('Product successfully updated!');
      } else {
        await productService.create(formData);
        alert('Product successfully created!');
      }
      setFormData({ name: '', description: '', price: 0, stockQuantity: 0 });
      setEditingProduct(null);
      await loadData();
    } catch (err) {
      console.error('Failed to save product:', err);
      alert('Error saving product.');
    }
  };

  const handleEditClick = (product: Product) => {
    setEditingProduct(product);
    setFormData({
      name: product.name,
      description: product.description,
      price: product.price,
      stockQuantity: product.stockQuantity,
    });
  };

  const handleDeleteClick = async (id: string) => {
    // eslint-disable-next-line no-restricted-globals
    if (confirm('Are you sure you want to delete this product?')) {
      try {
        await productService.delete(id);
        await loadData();
      } catch (err) {
        console.error('Failed to delete product:', err);
        alert('Error deleting product.');
      }
    }
  };

  const handleCancelEdit = () => {
    setEditingProduct(null);
    setFormData({ name: '', description: '', price: 0, stockQuantity: 0 });
  };

  const totalItemsCount = cart?.items?.reduce((sum, item) => sum + item.quantity, 0) || 0;

  return (
    <div
      style={{
        padding: '20px',
        fontFamily: 'Arial, sans-serif',
        maxWidth: '1200px',
        margin: '0 auto',
      }}
    >
      <header
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          borderBottom: '1px solid #444',
          paddingBottom: '15px',
          marginBottom: '20px',
          flexWrap: 'wrap',
          gap: '15px',
        }}
      >
        <div style={{ display: 'flex', alignItems: 'center', gap: '20px', flexWrap: 'wrap' }}>
          <h1 style={{ margin: 0, fontSize: '28px' }}>E - Commerce Store</h1>

          <div>
            <button
              onClick={() => setView('shop')}
              style={{
                padding: '8px 16px',
                marginRight: '10px',
                cursor: 'pointer',
                background: view === 'shop' ? '#3498db' : '#333',
                color: '#fff',
                border: '1px solid #555',
                borderRadius: '4px',
                fontWeight: 'bold',
              }}
            >
              🛒 Shop
            </button>

            <button
              onClick={() => setView('admin')}
              style={{
                padding: '8px 16px',
                cursor: 'pointer',
                background: view === 'admin' ? '#2c3e50' : '#333',
                color: '#fff',
                border: '1px solid #555',
                borderRadius: '4px',
                fontWeight: 'bold',
              }}
            >
              ⚙️ Admin Panel
            </button>
          </div>
        </div>

        {view === 'shop' && (
          <div
            style={{
              background: '#222',
              padding: '10px 18px',
              borderRadius: '8px',
              border: '1px solid #444',
              color: '#fff',
            }}
          >
            🛒 <strong>Cart: </strong> {totalItemsCount} pcs. | <strong>Total:</strong> {cart?.totalPrice || 0} €
          </div>
        )}
      </header>

      {view === 'shop' && (
        <>
          {orderSuccessMessage && (
            <div
              style={{
                background: '#d4edda',
                color: '#155724',
                padding: '15px',
                borderRadius: '8px',
                border: '1px solid #c3e6cb',
                marginBottom: '20px',
                fontWeight: 'bold',
              }}
            >
              {orderSuccessMessage}
            </div>
          )}

          <div
            style={{
              background: '#fdfdfe',
              border: '1px solid #e1e4e8',
              padding: '20px',
              borderRadius: '8px',
              marginBottom: '30px',
              color: '#333',
            }}
          >
            <h3 style={{ marginTop: 0 }}>Cart Contents</h3>

            {(!cart?.items || cart.items.length === 0) ? (
              <p style={{ color: '#6c757d' }}>The cart is empty.</p>
            ) : (
              <div>
                <ul style={{ listStyle: 'none', padding: 0 }}>
                  {cart.items.map(item => (
                    <li
                      key={item.id}
                      style={{
                        display: 'flex',
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        padding: '10px 0',
                        borderBottom: '1px solid #eee',
                      }}
                    >
                      <div>
                        <strong>{item.productName}</strong>
                        <span style={{ color: '#666', marginLeft: '10px' }}>
                          ({item.quantity} pcs. @{item.unitPrice} € = {item.totalPrice} €)
                        </span>
                      </div>

                      <button
                        style={{ background: '#e74c3c', color: 'white', border: 'none', padding: '6px 12px', borderRadius: '4px', cursor: 'pointer' }}
                        onClick={() => handleRemoveFromCart(item.productId)}
                      >
                        Remove
                      </button>
                    </li>
                  ))}
                </ul>

                <div style={{ marginTop: '15px', textAlign: 'right' }}>
                  <strong>Total Sum: {cart.totalPrice} €</strong>
                  <div style={{ marginTop: '10px' }}>
                    <button
                      style={{ background: '#2ecc71', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '4px', cursor: 'pointer', fontWeight: 'bold', fontSize: '16px' }}
                      onClick={handleCheckout}
                    >
                      Checkout (Place Order)
                    </button>
                  </div>
                </div>
              </div>
            )}
          </div>

          {loading && <p>Loading data...</p>}
          {error && <p style={{ color: 'red' }}>{error}</p>}

          <h2>Product Catalog</h2>
          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gap: '20px' }}>
            {products.map(product => (
              <div key={product.id} style={{ border: '1px solid #ccc', padding: '15px', borderRadius: '8px', boxShadow: '0 2px 4px rgba(0,0,0,0.1)', background: '#fff', color: '#333' }}>
                <h3 style={{ marginTop: 0 }}>{product.name}</h3>
                <p style={{ color: '#555', minHeight: '40px' }}>{product.description}</p>
                <p style={{ fontWeight: 'bold', fontSize: '18px', color: '#2c3e50' }}>{product.price} €</p>
                <p style={{ fontSize: '12px', color: '#7f8c8d' }}>Stock: {product.stockQuantity}</p>
                <button
                  style={{ background: '#3498db', color: 'white', border: 'none', padding: '10px 15px', borderRadius: '4px', cursor: 'pointer', width: '100%' }}
                  onClick={() => handleAddToCart(product.id)}
                >
                  Add to Cart
                </button>
              </div>
            ))}
          </div>
        </>
      )}

      {view === 'admin' && (
        <div>
          <h2>⚙️ Product Management (Admin)</h2>

          <form onSubmit={handleFormSubmit} style={{ background: '#f8f9fa', padding: '20px', borderRadius: '8px', border: '1px solid #ccc', marginBottom: '30px', color: '#333' }}>
            <h3>{editingProduct ? 'Edit Product' : 'Add New Product'}</h3>

            <div style={{ marginBottom: '10px' }}>
              <label style={{ display: 'block', fontWeight: 'bold' }}>Name:</label>
              <input
                type="text"
                required
                value={formData.name}
                onChange={e => setFormData({ ...formData, name: e.target.value })}
                style={{ width: '100%', padding: '8px', marginTop: '5px' }}
              />
            </div>

            <div style={{ marginBottom: '10px' }}>
              <label style={{ display: 'block', fontWeight: 'bold' }}>Description:</label>
              <textarea
                value={formData.description}
                onChange={e => setFormData({ ...formData, description: e.target.value })}
                style={{ width: '100%', padding: '8px', marginTop: '5px' }}
              />
            </div>

            <div style={{ display: 'flex', gap: '20px', marginBottom: '15px' }}>
              <div style={{ flex: 1 }}>
                <label style={{ display: 'block', fontWeight: 'bold' }}>Price(€):</label>
                <input
                  type="number"
                  step="0.01"
                  required
                  value={formData.price}
                  onChange={e => setFormData({ ...formData, price: Number(e.target.value) })}
                  style={{ width: '100%', padding: '8px', marginTop: '5px' }}
                />
              </div>

              <div style={{ width: 160 }}>
                <label style={{ display: 'block', fontWeight: 'bold' }}>Stock qty:</label>
                <input
                  type="number"
                  required
                  value={formData.stockQuantity}
                  onChange={e => setFormData({ ...formData, stockQuantity: Number(e.target.value) })}
                  style={{ width: '100%', padding: '8px', marginTop: '5px' }}
                />
              </div>
            </div>

            <div style={{ display: 'flex', gap: '10px' }}>
              <button type="submit" style={{ background: '#3498db', color: 'white', border: 'none', padding: '10px 16px', borderRadius: '4px', cursor: 'pointer' }}>
                {editingProduct ? 'Save' : 'Create'}
              </button>

              {editingProduct && (
                <button type="button" onClick={handleCancelEdit} style={{ background: '#95a5a6', color: 'white', border: 'none', padding: '10px 16px', borderRadius: '4px', cursor: 'pointer' }}>
                  Cancel
                </button>
              )}
            </div>
          </form>

          <div>
            <h3>Existing Products</h3>
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(220px, 1fr))', gap: '16px' }}>
              {products.map(p => (
                <div key={p.id} style={{ border: '1px solid #ddd', padding: '12px', borderRadius: '6px', background: '#fff' }}>
                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <strong>{p.name}</strong>
                    <div style={{ display: 'flex', gap: '8px' }}>
                      <button onClick={() => handleEditClick(p)} style={{ padding: '6px 10px', cursor: 'pointer' }}>
                        Edit
                      </button>
                      <button onClick={() => handleDeleteClick(p.id)} style={{ padding: '6px 10px', cursor: 'pointer', background: '#e74c3c', color: '#fff', border: 'none' }}>
                        Delete
                      </button>
                    </div>
                  </div>
                  <div style={{ marginTop: 8, color: '#666' }}>{p.description}</div>
                </div>
              ))}
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default App;