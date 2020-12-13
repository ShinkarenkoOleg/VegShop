using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class PricesStorage : IPricesStorage
    {
        private readonly Dictionary<Guid, SortedList<DateTime, Price>> prices = new Dictionary<Guid, SortedList<DateTime, Price>>();
        private readonly object syncObject = new object();

        public SortedList<DateTime, Price> GetPrices(Guid productId)
        {
            lock (syncObject)
            {
                if (prices.ContainsKey(productId))
                {
                    return prices[productId];
                }

                throw new PriceException($"Prices for product {productId} not found");
            }
        }

        public void AddPrice(Guid productId, Price price)
        {
            lock (syncObject)
            {
                if (prices.ContainsKey(productId))
                {
                    var (startDate, priceObject) = prices[productId].FirstOrDefault(p => p.Key == price.StartDate);
                    if (priceObject != null)
                    {
                        prices[productId].Remove(startDate);
                    }

                    prices[productId].Add(price.StartDate, price);
                }
                else
                {
                    prices.Add(productId, new SortedList<DateTime, Price> { {price.StartDate, price} });
                }
            }
        }

        public void RemovePrice(Guid productId, DateTime startDate)
        {
            lock (syncObject)
            {
                if (prices.ContainsKey(productId))
                {
                    var (startDateValue, price) = prices[productId].FirstOrDefault(p => p.Key == startDate);
                    if (price != null)
                    {
                        prices[productId].Remove(startDate);
                    }
                    else
                    {
                        throw new PriceException($"Can't remove price with start date {startDate} for product {productId} cause such price doesn't exist");
                    }
                }
                else
                {
                    throw new PriceException($"Can't remove price for product {productId} cause prices for this products don't exist");
                }
            }
        }
    }
}