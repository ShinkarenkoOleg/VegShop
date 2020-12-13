using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface IPriceService
    {
        decimal GetActualPrice(Guid productId, DateTime date);

        void AddPrice(Guid productId, Price price);
        void RemovePrice(Guid productId, DateTime startDate);
    }
}