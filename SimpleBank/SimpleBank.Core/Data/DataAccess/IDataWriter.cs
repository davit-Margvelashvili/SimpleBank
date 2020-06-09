using System.Collections.Generic;

namespace SimpleBank.Core.Data.DataAccess
{
    public interface IDataWriter<in T>
    {
        void WriteData(IEnumerable<T> data);
    }
}