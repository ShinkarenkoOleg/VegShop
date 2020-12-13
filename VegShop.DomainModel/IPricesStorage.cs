using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface IPricesStorage
    {
        SortedList<DateTime, Price> GetPrices(Guid productId);

        void AddPrice(Guid productId, Price price);
        void RemovePrice(Guid productId, DateTime startDate);
    }
}