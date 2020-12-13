using System;

namespace VegShop.DomainModel
{
    public interface IWarehouseStorage
    {
        void AddToStock(Guid productId, int quantity);
        void RemoveFromStock(Guid productId, int quantity);
    }
}