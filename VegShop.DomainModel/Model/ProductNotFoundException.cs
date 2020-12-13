using System;

namespace VegShop.DomainModel
{
    public class ProductNotFoundException : NomenclatureException
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}