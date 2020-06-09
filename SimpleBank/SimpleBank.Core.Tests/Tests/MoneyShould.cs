using System;
using System.Collections.Generic;
using System.Text;
using SimpleBank.Core.Models;
using SimpleBank.Core.Utils;
using Xunit;

namespace SimpleBank.Core.Tests.Tests
{
    public class MoneyShould
    {
        [Fact]
        public void ConvertOneCurrencyIntoAnotherWhenAdded()
        {
            Fx.Initialize(new Dictionary<string, decimal>
            {
                ["GEL:USD"] = 0.50m,
                ["USD:GEL"] = 2.00m,
            });

            var gel = new Money(CurrencyCode.GEL, 10);
            var usd = new Money(CurrencyCode.USD, 10);

            var total = gel + usd;
            Assert.Equal(10m + 10m * 2, total.Amount);
            Assert.Equal(CurrencyCode.GEL, total.Currency);
        }

        [Fact]
        public void ConvertOneCurrencyIntoAnotherWhenSubtracted()
        {
            Fx.Initialize(new Dictionary<string, decimal>
            {
                ["GEL:USD"] = 0.50m,
                ["USD:GEL"] = 2.00m,
            });

            var gel = new Money(CurrencyCode.GEL, 10);
            var usd = new Money(CurrencyCode.USD, 10);

            var total = gel - usd;
            Assert.Equal(10m - 10m * 2, total.Amount);
            Assert.Equal(CurrencyCode.GEL, total.Currency);
        }

        [Fact]
        public void GenerateSameHashCodeForSameValues()
        {
            var gel1 = new Money(CurrencyCode.GEL, 10);
            var gel2 = new Money(CurrencyCode.GEL, 10);

            Assert.Equal(gel1.GetHashCode(), gel2.GetHashCode());
        }

        [Fact]
        public void GenerateDifferentHashCodeForDifferentValues()
        {
            var gel1 = new Money(CurrencyCode.GEL, 20);
            var gel2 = new Money(CurrencyCode.GEL, 10);
            var usd = new Money(CurrencyCode.USD, 10);

            Assert.NotEqual(gel1.GetHashCode(), gel2.GetHashCode());
            Assert.NotEqual(gel2.GetHashCode(), usd.GetHashCode());
        }
    }
}