using System;

namespace VegShop.DomainModel
{
    public class CartException : Exception
    {
        public CartException(string message) : base(message)
        {
        }
    }
}