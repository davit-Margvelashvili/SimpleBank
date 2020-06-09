using System;
using System.Collections.Generic;
using SimpleBank.Core.Models;

namespace SimpleBank.Core.Utils
{
    public static class Fx
    {
        public static Dictionary<string, decimal> Rates { get; private set; }

        public static void Initialize(Dictionary<string, decimal> currencyRate)
        {
            Rates = currencyRate;
        }

        public static Money Exchange(decimal amount, CurrencyCode from, CurrencyCode to)
        {
            var currency = $"{from}:{to}";
            if (!Rates.TryGetValue(currency, out var rate))
                throw new Exception("Not Found");

            return new Money(to, amount * rate);
        }
    }
}