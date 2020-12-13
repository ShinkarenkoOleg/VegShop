using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface ICartManagement
    {
        void AddToCartPhysically(Guid productId, int quantity);
        void RemoveFromCartPhysically(Guid productId, int quantity);
    }
}