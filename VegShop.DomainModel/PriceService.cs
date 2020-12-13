using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class PriceService : IPriceService
    {
        private readonly Dictionary<Guid, SortedList<DateTime, Price>> prices = new Dictionary<Guid, SortedList<DateTime, Price>>();
        private readonly object syncObject = new object();

        public decimal GetActualPrice(Guid productId, DateTime date)
        {
            lock (syncObject)
            {
                if (!prices.ContainsKey(productId))
                {
                    throw new PriceException($"Prices for product {productId} not found");
                }

                var (startDate, price) = prices[productId].FirstOrDefault(p => p.Key >= date);
                if (price == null)
                {
                    throw new PriceException($"There is no price for specified date {date} for product {productId}");
                }

                return price.PriceValue;
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