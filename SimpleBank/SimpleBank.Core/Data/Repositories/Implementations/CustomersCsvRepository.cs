using System.Net.Mail;
using SimpleBank.Core.Data.DataAccess;
using SimpleBank.Core.Data.Repositories.Abstractions;
using SimpleBank.Core.Models;

namespace SimpleBank.Core.Data.Repositories.Implementations
{
    public class CustomersCsvRepository : GenericCsvRepository<Customer, int>
    {
        public CustomersCsvRepository(IDataReader<string> dataReader, IDataWriter<string> dataWriter) : base(dataReader, dataWriter)
        {
        }

        protected override int GenerateNewId(int lastId) => lastId + 1;

        protected override Customer ToObject(string s)
        {
            // Id,Name,IdentityNumber,PhoneNumber,Email,Type

            var data = s.Split(',');
            var idx = 0;

            return new Customer(int.Parse(data[idx++]))
            {
                Name = data[idx++],
                IdentityNumber = data[idx++],
                PhoneNumber = data[idx++],
                Email = new MailAddress(data[idx++]),
                Type = (CustomerType)byte.Parse(data[idx])
            };
        }

        protected override string ToCsv(Customer customer) =>
            $"{customer.Name},{customer.IdentityNumber},{customer.PhoneNumber},{customer.Email},{customer.Type}";
    }
}