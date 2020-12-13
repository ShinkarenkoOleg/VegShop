using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class PriceService : IPriceService
    {
        private readonly IPricesStorage pricesStorage;

        public PriceService(IPricesStorage pricesStorage)
        {
            this.pricesStorage = pricesStorage;
        }

        public decimal GetActualPrice(Guid productId, DateTime date)
        {
            var (startDate, price) = pricesStorage.GetPrices(productId).FirstOrDefault(p => p.Key >= date);

            if (price == null)
            {
                throw new PriceException($"There is no price for specified date {date} for product {productId}");
            }

            return price.PriceValue;
        }

        public void AddPrice(Guid productId, Price price)
        {
            pricesStorage.AddPrice(productId, price);
        }

        public void RemovePrice(Guid productId, DateTime startDate)
        {
            pricesStorage.RemovePrice(productId, startDate);
        }
    }
}