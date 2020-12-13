using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class OffersService : IOffersService
    {
        private readonly Dictionary<Guid, IList<Offer>> offers = new Dictionary<Guid, IList<Offer>>();
        private readonly object syncObject = new object();

        public IList<Offer> GetOffers(Guid productId)
        {
            lock (syncObject)
            {
                return offers.ContainsKey(productId) ? offers[productId] : new List<Offer>();
            }
        }

        public void AddOffer(Guid productId, Offer offer)
        {
            lock (syncObject)
            {
                if (offers.ContainsKey(productId))
                {
                    if (offers[productId].Any(o => o.ItemsCount == offer.ItemsCount))
                    {
                        throw new OfferException($"Can't add offer for product {productId} cause offer with items count {offer.ItemsCount} already exists");
                    }
                    offers[productId].Add(offer);
                }
                else
                {
                    offers.Add(productId, new List<Offer> {offer});
                }
            }
        }

        public void RemoveOffer(Guid productId, int itemsCount)
        {
            lock (syncObject)
            {
                if (offers.ContainsKey(productId))
                {
                    var offer = offers[productId].FirstOrDefault(o => o.ItemsCount == itemsCount);
                    if (offer == null)
                    {
                        throw new OfferException($"Can't remove offer for product {productId} and items count {itemsCount} cause such offer doesn't exist");
                    }
                    offers[productId].Remove(offer);
                }
                else
                {
                    throw new OfferException($"Can't remove offer for product {productId} cause offers for this products don't exist");
                }
            }
        }
    }
}