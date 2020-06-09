using System.Collections.Generic;
using System.IO;

namespace SimpleBank.Core.Data.DataAccess
{
    public sealed class FileDataStore : IDataReader<string>, IDataWriter<string>
    {
        private readonly string _filePath;

        public FileDataStore(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<string> ReadData()
        {
            return File.ReadAllLines(_filePath);
        }

        public void WriteData(IEnumerable<string> data)
        {
            File.WriteAllLines(_filePath, data);
        }
    }
}