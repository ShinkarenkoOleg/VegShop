using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface IOffersStorage
    {
        IList<Offer> GetProductOffers(Guid productId);

        void AddOffer(Guid productId, Offer offer);
        void RemoveOffer(Guid productId, int itemsCount);
    }
}