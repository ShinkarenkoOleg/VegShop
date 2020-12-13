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

        public void AddToCart(ICartManagement cart, Guid productId, int quantity)
        {
            lock (syncObject)
            {
                this.warehouseStorage.RemoveFromStock(productId, quantity);
                cart.AddToCartPhysically(productId, quantity);
            }
        }

        public void RemoveFromCart(ICartManagement cart, Guid productId, int quantity)
        {
            lock (syncObject)
            {
                this.warehouseStorage.AddToStock(productId, quantity);
                cart.RemoveFromCartPhysically(productId, quantity);
            }
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