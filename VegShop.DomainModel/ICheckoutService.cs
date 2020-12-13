using System;

namespace VegShop.DomainModel
{
    public interface ICheckoutService
    {
        void AddToCart(ICartManagement cartManagement, Guid productId, int quantity);
        void RemoveFromCart(ICartManagement cartManagement, Guid productId, int quantity);
    }
}