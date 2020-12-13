using System;

namespace VegShop.DomainModel
{
    public class WarehouseException : Exception
    {
        public WarehouseException(string message) : base(message)
        {
        }
    }
}