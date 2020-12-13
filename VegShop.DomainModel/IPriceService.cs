using System;

namespace VegShop.DomainModel
{
    public interface IPriceService
    {
        void AddPrice(Guid productId, Price price);
        void RemovePrice(Guid productId, DateTime startDate);
        decimal GetActualPrice(Guid productId, DateTime date);
    }
}