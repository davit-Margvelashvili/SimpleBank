using System;
using System.Collections.Generic;
using System.Text;
using SimpleBank.Core.Models;
using Xunit;

namespace SimpleBank.Core.Tests.Tests
{
    public class IbanShould
    {
        [Fact]
        public void ThrowIfIbanIsNullOrWhitespace()
        {
            Assert.Throws<Exception>(() => new Iban(null));
            Assert.Throws<Exception>(() => new Iban("           "));
        }

        [Fact]
        public void ThrowIfIbanLengthIsNot22()
        {
            Assert.Throws<Exception>(() => new Iban("GE123"));
            Assert.Throws<Exception>(() => new Iban("GE123456789123456789123456789123456789"));
        }

        [Fact]
        public void ThrowIfIbanDoesNotStartWithLetters()
        {
            Assert.Throws<Exception>(() => new Iban("1234567891234567891234"));
        }

        [Fact]
        public void CreateIban()
        {
            var iban = new Iban("GE34567891234567891234");

            Assert.Equal("GE34567891234567891234", iban);
        }

        [Fact]
        public void HaveCountryCode()
        {
            var iban = new Iban("GE34567891234567891234");

            Assert.Equal("GE", iban.CountryCode);
        }
    }
}