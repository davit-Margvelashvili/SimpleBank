using System.Collections.Generic;
using SimpleBank.Core.Data.DataAccess;

namespace SimpleBank.Core.Tests.Common
{
    internal class MockDataStore : IDataReader<string>, IDataWriter<string>
    {
        private readonly List<string> _data;

        public MockDataStore(List<string> data)
        {
            _data = data;
        }

        public IEnumerable<string> ReadData()
        {
            return _data;
        }

        public void WriteData(IEnumerable<string> data)
        {
            _data.Clear();
            _data.AddRange(data);
        }
    }
}