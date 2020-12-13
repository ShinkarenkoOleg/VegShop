using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace VegShop.DomainModel
{
    public class Cart : ICart, ICartManagement
    {
        private readonly INomenclatureService nomenclatureService;
        private readonly IOffersCalculator offersCalculator;
        private readonly IOffersService offersService;
        private readonly IWarehouseService warehouseService;
        private readonly IPriceService priceService;

        private readonly ConcurrentDictionary<Guid, CartItem> cartItems = new ConcurrentDictionary<Guid, CartItem>();

        public Cart(
            INomenclatureService nomenclatureService,
            IOffersCalculator offersCalculator,
            IOffersService offersService,
            IPriceService priceService,
            IWarehouseService warehouseService)
        {
            this.nomenclatureService = nomenclatureService;
            this.offersCalculator = offersCalculator;
            this.offersService = offersService;
            this.priceService = priceService;
            this.warehouseService = warehouseService;
        }

        public void AddToCart(Guid productId, int quantity)
        {
            if (!nomenclatureService.DoesProductExist(productId))
            {
                throw new CartException($"Product {productId} doesn't exist");
            }

            warehouseService.AddToCart(this, productId, quantity);
        }

        public void RemoveFromCart(Guid productId, int quantity)
        {
            if (!nomenclatureService.DoesProductExist(productId))
            {
                throw new CartException($"Product {productId} doesn't exist");
            }

            warehouseService.RemoveFromCart(this, productId, quantity);
        }

        public decimal GetTotalCost()
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

                totalCost += offersCalculator.CalculateCost(offers, cartItem.Quantity, unitPrice);
            }

            return totalCost;
        }

        public void AddToCartPhysically(Guid productId, int quantity)
        {
            cartItems.AddOrUpdate(
                productId,
                new CartItem(productId, quantity, DateTime.UtcNow),
                (prId, item) => { item.Quantity += quantity; return item; });
        }

        public void RemoveFromCartPhysically(Guid productId, int quantity)
        {
            var isSuccessful = cartItems.TryGetValue(productId, out var cartItem);
            if (!isSuccessful)
            {
                throw new CartException($"Product {productId} doesn't exist in a cart");
            }

            if (cartItem.Quantity < quantity)
            {
                throw new CartException(
                    $"Can't delete {quantity} items of Product {productId} cause cart contains only {cartItem.Quantity} items");
            }

            cartItem.Quantity -= quantity;
            if (cartItem.Quantity == 0)
            {
                isSuccessful = cartItems.TryRemove(productId, out var item);

                if (!isSuccessful)
                {
                    throw new CartException($"Can't remove cart item for product {productId} cause such cart item doesn't exist");
                }
            }
        }
    }
}