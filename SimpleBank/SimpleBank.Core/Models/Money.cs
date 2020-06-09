using System;
using SimpleBank.Core.Utils;

namespace SimpleBank.Core.Models
{
    public readonly struct Money
    {
        public bool Equals(Money other)
        {
            return Currency == other.Currency && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return obj is Money other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Currency, Amount);
        }

        public readonly CurrencyCode Currency;
        public readonly decimal Amount;

        public Money(CurrencyCode currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public static Money operator +(Money left, Money right)
        {
            var convertedRight = Fx.Exchange(right.Amount, right.Currency, left.Currency);
            return new Money(left.Currency, left.Amount + convertedRight.Amount);
        }

        public static Money operator -(Money left, Money right)
        {
            var convertedRight = Fx.Exchange(right.Amount, right.Currency, left.Currency);
            return new Money(left.Currency, left.Amount - convertedRight.Amount);
        }

        public static explicit operator decimal(Money money) => money.Amount;

        public static bool operator ==(Money left, Money right) =>
            left.Amount == right.Amount && left.Currency == right.Currency;

        public static bool operator !=(Money left, Money right) =>
            left.Amount != right.Amount || left.Currency != right.Currency;
    }
}