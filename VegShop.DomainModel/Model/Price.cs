using System;

namespace VegShop.DomainModel
{
    public class Price
    {
        public decimal PriceValue { get; }

        public DateTime StartDate { get; }

        public Price(DateTime startDate, decimal price)
        {
            StartDate = startDate;
            PriceValue = price;
        }
    }
}