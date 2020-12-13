using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class OffersService : IOffersService
    {
        private readonly IOffersStorage offersStorage;

        public OffersService(IOffersStorage offersStorage, IPriceService priceService)
        {
            this.offersStorage = offersStorage;
        }

        public IList<Offer> GetOffers(Guid productId)
        {
            return offersStorage.GetProductOffers(productId);
        }

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

        public void AddOffer(Guid productId, Offer offer)
        {
            offersStorage.AddOffer(productId, offer);
        }

        public void RemoveOffer(Guid productId, int itemsCount)
        {
            offersStorage.RemoveOffer(productId, itemsCount);
        }
    }
}