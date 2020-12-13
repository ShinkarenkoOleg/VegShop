using System;

namespace VegShop.DomainModel
{
    public interface IWarehouseService
    {
        void AddToStock(Guid productId, int quantity);
        void RemoveFromStock(Guid productId, int quantity);
    }
}