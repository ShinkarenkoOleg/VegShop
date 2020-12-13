using System;

namespace VegShop.DomainModel
{
    public interface INomenclatureService
    {
        Product GetProduct(Guid productId);
        bool DoesProductExist(Guid productId);

        void AddProduct(Product product);
        void RemoveProduct(Guid productId);
    }
}