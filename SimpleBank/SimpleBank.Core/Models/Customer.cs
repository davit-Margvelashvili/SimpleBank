using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using SimpleBank.Core.Models.Abstractions;

namespace SimpleBank.Core.Models
{
    public sealed class Customer : IDomainObject<int>, IEquatable<Customer>
    {
        public Customer(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public MailAddress Email { get; set; }
        public CustomerType Type { get; set; }

        public bool Equals(Customer other)
        {
            return Id == other?.Id;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Customer);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}