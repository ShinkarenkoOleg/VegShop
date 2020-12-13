using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface ICart
    {
        void AddProduct(Guid productId, int quantity);

        void RemoveProduct(Guid productId, int quantity);

        decimal TotalCost();

        IDictionary<Guid, CartItem> GetCartItems();
    }
}