using System;

namespace VegShop.DomainModel
{
    public class OfferException : Exception
    {
        public OfferException(string message) : base(message)
        {
        }
    }
}