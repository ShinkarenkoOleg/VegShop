using System;

namespace VegShop.DomainModel
{
    public interface INomenclatureService
    {
        Product GetProduct(Guid productId);
        void AddProduct(Product product);
        void RemoveProduct(Guid productId);
        bool DoesProductExists(Guid productId);
    }
}