using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface ICart
    {
        void AddToCart(Guid productId, int quantity);
        void RemoveFromCart(Guid productId, int quantity);

        decimal GetTotalCost();
    }
}