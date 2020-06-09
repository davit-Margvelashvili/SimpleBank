using System.Collections.Generic;

namespace SimpleBank.Core.Data.DataAccess
{
    public interface IDataReader<out T>
    {
        IEnumerable<T> ReadData();
    }
}