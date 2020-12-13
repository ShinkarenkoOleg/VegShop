using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace VegShop.DomainModel
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly ConcurrentDictionary<Guid, Product> nomenclature = new ConcurrentDictionary<Guid, Product>();

        public Product GetProduct(Guid productId)
        {
            var isSuccessful = nomenclature.TryGetValue(productId, out var product);
            if (!isSuccessful)
            {
                throw new ProductNotFoundException($"Product with id {productId}' not found");
            }

            return product;
        }

        public void AddProduct(Product product)
        {
            var isSuccessful = nomenclature.TryAdd(product.Id, product);

            if (!isSuccessful)
            {
                throw new NomenclatureException($"Product with id {product.Id} already exists");
            }
        }

        public void RemoveProduct(Guid productId)
        {
            var isSuccessful = nomenclature.TryRemove(productId, out var value);
            if (!isSuccessful)
            {
                throw new NomenclatureException($"Product with id {productId} not found");
            }

            return;
        }

        public bool DoesProductExist(Guid productId)
        {
            return nomenclature.ContainsKey(productId);
        }
    }
}