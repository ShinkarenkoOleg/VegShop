using System;
using Xunit;

namespace VegShop.DomainModel.Tests
{
    public class PriceServiceTests
    {
        [Fact]
        public void GetActualPrice()
        {
            var target = new PriceService();
            var actualPrice = target.GetActualPrice(Identifiers.AppleId, DateTime.Now.AddDays(1));
        }
    }
}