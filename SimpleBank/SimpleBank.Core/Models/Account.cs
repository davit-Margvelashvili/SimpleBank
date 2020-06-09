using System;
using System.Linq;
using System.Net;
using SimpleBank.Core.Models.Abstractions;

namespace SimpleBank.Core.Models
{
    public sealed class Iban
    {
        public const int Length = 22;

        public string Value { get; }

        public string CountryCode { get; }

        public Iban(string value)
        {
            Validate(value);

            Value = value;

            CountryCode = new string(new[] { value[0], value[1] });
        }

        public static implicit operator string(Iban iban) => iban.Value;

        public static implicit operator Iban(string value) => new Iban(value);

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("value cannot be null or white space"); // TODO კონკრეტული Exception

            if (value.Length != Length || !value.Take(2).All(char.IsLetter))
                throw new Exception("Invalid Iban Structure"); // TODO კონკრეტული Exception
        }
    }

    public sealed class Account : IDomainObject<int>, IEquatable<Account>
    {
        public Account(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public Iban Iban { get; set; }

        public Money Balance { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public bool Equals(Account other)
        {
            return Id == other?.Id;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Account);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}