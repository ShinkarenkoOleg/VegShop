using System;

namespace VegShop.DomainModel
{
    public class CartItem
    {
        public Guid ProductId { get; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; }

        public CartItem(Guid productId, int quantity, DateTime dateCreated)
        {
            ProductId = productId;
            Quantity = quantity;
            DateCreated = dateCreated;
        }
    }
}