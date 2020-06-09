using System.Collections.Generic;
using SimpleBank.Core.Data.Repositories.Implementations;
using SimpleBank.Core.Models;
using SimpleBank.Core.Tests.Common;
using Xunit;

namespace SimpleBank.Core.Tests.Tests
{
    public class AccountsCsvRepositoryShould
    {
        private readonly AccountsCsvRepository _repo;

        public AccountsCsvRepositoryShould()
        {
            var dataStore = new MockDataStore(new List<string>
            {
                "Id,Iban,Currency,Balance,CustomerId,Name",
                "1,GE62TB9291655661344791,USD,6712,5,",
                "2,GE40TB2971676721474477,GEL,2485,6,My Account",
                "3,GE75TB3846363723366822,EUR,7354,7,",
                "4,GE85TB9214549758617342,RUB,940000,1,"
            });

            _repo = new AccountsCsvRepository(dataStore, dataStore);
        }

        [Fact]
        public void ReturnAccountByIdIfExists()
        {
            var expected = new Account(1)
            {
                Iban = "GE62TB9291655661344791",
                Balance = new Money(CurrencyCode.USD, 6712m),
                CustomerId = 5,
                Name = null
            };

            var actual = _repo.GetById(1);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Iban, actual.Iban);
            Assert.Equal(expected.Balance, actual.Balance);
            Assert.Equal(expected.CustomerId, actual.CustomerId);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void ReturnAccountByIdIfExists_WithName()
        {
            var expected = new Account(2)
            {
                Iban = "GE40TB2971676721474477",
                Balance = new Money(CurrencyCode.GEL, 2485m),
                CustomerId = 6,
                Name = "My Account"
            };

            var actual = _repo.GetById(2);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Iban, actual.Iban);
            Assert.Equal(expected.Balance, actual.Balance);
            Assert.Equal(expected.CustomerId, actual.CustomerId);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void UpdateExistingAccount()
        {
            var expected = new Account(1)
            {
                Iban = "GE40TB2971676721474477",
                Balance = new Money(CurrencyCode.GEL, 2485m),
                CustomerId = 6,
                Name = "My Account"
            };

            var account = _repo.GetById(1);

            account.Iban = "GE40TB2971676721474477";
            account.Balance = new Money(CurrencyCode.GEL, 2485m);
            account.CustomerId = 6;
            account.Name = "My Account";

            _repo.Update(account);
            var actual = _repo.GetById(1);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Iban, actual.Iban);
            Assert.Equal(expected.Balance, actual.Balance);
            Assert.Equal(expected.CustomerId, actual.CustomerId);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void DeleteAccountById()
        {
            var account = _repo.GetById(1);

            Assert.NotNull(account);

            var deleted = _repo.Delete(1);
            var deletedAccount = _repo.GetById(1);

            Assert.True(deleted);
            Assert.Null(deletedAccount);
        }

        [Fact]
        public void QueryAccounts()
        {
            var expectedAccounts = new List<Account>
            {
                new Account(1)
                {
                    Iban = "GE62TB9291655661344791",
                    Balance =new Money(CurrencyCode.USD, 6712m) ,
                    CustomerId = 5,
                    Name = null
                },

                new Account(2)
                {
                    Iban = "GE40TB2971676721474477",
                    Balance =new Money(CurrencyCode.USD, 2485m) ,
                    CustomerId = 6,
                    Name = "My Account"
                }
            };

            var actualAccounts = _repo.Query(a => a.Id < 3);

            var comparer = new GenericEqualityComparer<Account>((e, a) =>
                        e.Id == a.Id
                     && e.Iban == a.Iban
                     && e.Balance == a.Balance
                     && e.CustomerId == a.CustomerId
                     && e.Name == a.Name);

            Assert.Equal(expectedAccounts, actualAccounts, comparer);
        }
    }
}