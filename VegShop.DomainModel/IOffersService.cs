using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface IOffersService
    {
        IList<Offer> GetOffers(Guid productId);
    }
}