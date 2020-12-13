using System;

namespace VegShop.DomainModel
{
    public class Offer
    {
        public Guid ProductId { get; }

        public int ItemsCount { get; }

        public decimal TotalPrice { get;  }

        public Offer(Guid productId, int itemsCount, decimal totalPrice)
        {
            ProductId = productId;
            ItemsCount = itemsCount;
            TotalPrice = totalPrice;
        }
    }
}