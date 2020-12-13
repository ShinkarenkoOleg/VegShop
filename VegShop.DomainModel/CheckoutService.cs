using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public class CheckoutService : ICheckoutService
    {
        private readonly object syncObject = new object();

        private readonly IWarehouseService warehouseService;

        public CheckoutService(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        public void AddToCart(ICartManagement cartManagement, Guid productId, int quantity)
        {
            lock (syncObject)
            {
                warehouseService.RemoveFromStock(productId, quantity);
                cartManagement.AddToCartPhysically(productId, quantity);
            }
        }

        public void RemoveFromCart(ICartManagement cartManagement, Guid productId, int quantity)
        {
            lock (syncObject)
            {
                warehouseService.AddToStock(productId, quantity);
                cartManagement.RemoveFromCartPhysically(productId, quantity);
            }
        }
    }
}