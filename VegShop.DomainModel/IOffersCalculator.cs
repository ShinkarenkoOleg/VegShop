using System;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public interface IOffersCalculator
    {
        decimal CalculateCost(IEnumerable<Offer> offers, int quantity, decimal unitPrice);
    }
}