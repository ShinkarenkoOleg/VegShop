using System;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly Dictionary<Guid, int> stock = new Dictionary<Guid, int>();

        public void AddToStock(Guid productId, int quantity)
        {
            if (stock.ContainsKey(productId))
            {
                stock[productId] += quantity;
            }
            else
            {
                stock.Add(productId, quantity);
            }
        }

        public void RemoveFromStock(Guid productId, int quantity)
        {
            if (stock.ContainsKey(productId))
            {
                if (stock[productId] < quantity)
                {
                    throw new WarehouseException($"Can't remove {quantity} items for product {productId} cause there are only {stock[productId]} items on stock");
                }

                stock[productId] -= quantity;
            }
            else
            {
                throw new WarehouseException($"Can't remove product {productId} from stock cause such product doesn't exist");
            }
        }
    }
}