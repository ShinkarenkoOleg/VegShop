using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public class WarehouseService : IWarehouseService
    {
        private readonly object syncObject = new object();

        private readonly IWarehouseStorage warehouseStorage;

        public WarehouseService(IWarehouseStorage warehouseStorage)
        {
            this.warehouseStorage = warehouseStorage;
        }

        public bool AddToCart(Cart cart, Guid productId, int quantity)
        {
            lock (syncObject)
            {
                this.warehouseStorage.RemoveFromStock(productId, quantity);

                var cartItems = cart.GetCartItems();
                if (cartItems.ContainsKey(productId))
                {
                    cartItems[productId].Quantity += quantity;
                }
                else
                {
                    cartItems.Add(productId, new CartItem(productId, quantity, DateTime.UtcNow));
                }
            }

            return true;
        }

        public bool RemoveFromCart(Cart cart, Guid productId, int quantity)
        {
            lock (syncObject)
            {
                this.warehouseStorage.AddToStock(productId, quantity);

                var cartItems = cart.GetCartItems();
                if (!cartItems.ContainsKey(productId))
                {
                    throw new CartException($"Product {productId} doesn't present in a cart");
                }

                var currentQuantity = cartItems[productId].Quantity;
                if (currentQuantity < quantity)
                {
                    throw new CartException(
                        $"Can't delete {quantity} items of Product {productId} cause cart contains only {currentQuantity} items");
                }

                cartItems[productId].Quantity -= quantity;
                if (cartItems[productId].Quantity == 0)
                {
                    cartItems.Remove(productId, out var cartItem);
                }
            }

            return true;
        }

        public void AddToStock(Guid productId, int quantity)
        {
            lock (syncObject)
            {
                warehouseStorage.AddToStock(productId, quantity);
            }
        }

        public void RemoveFromStock(Guid productId, int quantity)
        {
            lock (syncObject)
            {
                warehouseStorage.RemoveFromStock(productId, quantity);
            }
        }
    }
}