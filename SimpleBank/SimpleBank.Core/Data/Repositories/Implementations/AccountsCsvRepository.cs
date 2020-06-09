using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBank.Core.Data.DataAccess;
using SimpleBank.Core.Data.Repositories.Abstractions;
using SimpleBank.Core.Models;

namespace SimpleBank.Core.Data.Repositories.Implementations
{
    public sealed class AccountsCsvRepository : GenericCsvRepository<Account, int>
    {
        public AccountsCsvRepository(IDataReader<string> dataReader, IDataWriter<string> dataWriter)
            : base(dataReader, dataWriter)
        {
        }

        protected override int GenerateNewId(int lastId) => lastId + 1;

        protected override Account ToObject(string s)
        {
            var data = s.Split(",");
            var idx = 0;

            var account = new Account(int.Parse(data[idx++]))
            {
                Iban = data[idx++],
                Balance = new Money(Enum.Parse<CurrencyCode>(data[idx++]), decimal.Parse(data[idx++])),
                CustomerId = int.Parse(data[idx++]),
                Name = data[idx],
            };

            if (string.IsNullOrWhiteSpace(account.Name))
                account.Name = null;

            return account;
        }

        protected override string ToCsv(Account account) =>
            $"{account.Iban},{account.Balance.Currency},{account.Balance.Amount},{account.CustomerId},{account.Name ?? string.Empty}";
    }
}