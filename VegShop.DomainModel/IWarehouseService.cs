using System;

namespace VegShop.DomainModel
{
    public interface IWarehouseService
    {
        void AddToCart(ICartManagement cart, Guid productId, int quantity);
        void RemoveFromCart(ICartManagement cart, Guid productId, int quantity);

        void AddToStock(Guid productId, int quantity);
        void RemoveFromStock(Guid productId, int quantity);
    }
}