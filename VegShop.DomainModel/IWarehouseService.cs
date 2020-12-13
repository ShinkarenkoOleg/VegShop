using System;

namespace VegShop.DomainModel
{
    public interface IWarehouseService
    {
        void AddToStock(Guid productId, int quantity);
        void RemoveFromStock(Guid productId, int quantity);

        bool AddToCart(Cart cart, Guid productId, int quantity);
        bool RemoveFromCart(Cart cart, Guid productId, int quantity);
    }
}