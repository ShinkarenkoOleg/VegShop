using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class Cart : ICart
    {
        private readonly INomenclatureService nomenclatureService;
        private readonly IOffersService offersService;
        private readonly IWarehouseService warehouseService;
        private readonly IPriceService priceService;

        private readonly ConcurrentDictionary<Guid, CartItem> cartItems = new ConcurrentDictionary<Guid, CartItem>();

        public Cart(
            INomenclatureService nomenclatureService,
            IOffersService offersService,
            IPriceService priceService,
            IWarehouseService warehouseService)
        {
            this.nomenclatureService = nomenclatureService;
            this.offersService = offersService;
            this.priceService = priceService;
            this.warehouseService = warehouseService;
        }

        public void AddProduct(Guid productId, int quantity)
        {
            if (!nomenclatureService.DoesProductExists(productId))
            {
                throw new CartException($"Product {productId} doesn't exist");
            }

            warehouseService.AddToCart(this, productId, quantity);
        }

        public void RemoveProduct(Guid productId, int quantity)
        {
            if (!nomenclatureService.DoesProductExists(productId))
            {
                throw new CartException($"Product {productId} doesn't exist");
            }

            warehouseService.RemoveFromCart(this, productId, quantity);
        }

        public decimal TotalCost()
        {
            decimal totalCost = 0;

            var pairs = cartItems.ToList();
            foreach (var (productId, cartItem) in pairs)
            {
                var unitPrice = priceService.GetActualPrice(productId, cartItem.DateCreated);

                var offers = offersService.GetOffers(productId);

                if (!offers.Any())
                {
                    totalCost += unitPrice * cartItem.Quantity;
                    continue;
                }

                totalCost += offersService.CalculateCost(offers, cartItem.Quantity, unitPrice);
            }

            return totalCost;
        }

        public IDictionary<Guid, CartItem> GetCartItems()
        {
            return cartItems;
        }
    }
}