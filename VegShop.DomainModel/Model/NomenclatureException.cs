using System;

namespace VegShop.DomainModel
{
    public class NomenclatureException : Exception
    {
        public NomenclatureException(string message) : base(message)
        {
        }
    }
}