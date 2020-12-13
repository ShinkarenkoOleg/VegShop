using System;

namespace VegShop.DomainModel
{
    public class PriceException : Exception
    {
        public PriceException(string message) : base(message)
        {
        }
    }
}