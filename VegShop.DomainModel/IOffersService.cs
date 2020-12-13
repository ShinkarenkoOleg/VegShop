using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface IOffersService
    {
        IList<Offer> GetOffers(Guid productId);
        decimal CalculateCost(IEnumerable<Offer> offers, int quantity, decimal unitPrice);

        void AddOffer(Guid productId, Offer offer);
        void RemoveOffer(Guid productId, int itemsCount);
    }
}