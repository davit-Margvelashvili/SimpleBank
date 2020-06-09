namespace SimpleBank.Core.Models.Abstractions
{
    public interface IDomainObject<out TId>
    {
        public TId Id { get; }
    }
}