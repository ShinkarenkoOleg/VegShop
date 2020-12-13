using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class OffersCalculator : IOffersCalculator
    {
        public decimal CalculateCost(IEnumerable<Offer> offers, int quantity, decimal unitPrice)
        {
            var mostProfitableOffer = offers.Where(o => o.ItemsCount < quantity).OrderByDescending(o => o.ItemsCount).FirstOrDefault();
            if (mostProfitableOffer == null)
            {
                return unitPrice * quantity;
            }

            int offerBatchesCount = mostProfitableOffer.ItemsCount / quantity;
            int standardPriceItemsCount = mostProfitableOffer.ItemsCount % quantity;
            return offerBatchesCount * mostProfitableOffer.TotalPrice + standardPriceItemsCount * unitPrice;
        }
    }
}