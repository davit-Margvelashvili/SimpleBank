using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBank.Core.Data.DataAccess;
using SimpleBank.Core.Data.Repositories.Abstractions;
using SimpleBank.Core.Models;
using SimpleBank.Core.Models.Abstractions;

namespace SimpleBank.Core.Data.Repositories.Implementations
{
    public abstract class GenericCsvRepository<TObject, TId> : IRepository<TObject, TId> where TObject : IDomainObject<TId>
    {
        private readonly IDataReader<string> _dataReader;
        private readonly IDataWriter<string> _dataWriter;

        private readonly Dictionary<TId, TObject> _data;

        protected GenericCsvRepository(IDataReader<string> dataReader, IDataWriter<string> dataWriter)
        {
            _dataReader = dataReader ?? throw new ArgumentNullException(nameof(dataReader));
            _dataWriter = dataWriter ?? throw new ArgumentNullException(nameof(dataWriter));
            _data = ReadData();
        }

        #region Public Methods

        public TObject GetById(TId id)
        {
            _data.TryGetValue(id, out var entity); ;
            return entity;
        }

        public TId Add(TObject entity)
        {
            var lastId = _data.Keys.Max();
            var newId = GenerateNewId(lastId);
            _data[newId] = entity;

            SaveChanges();
            return newId;
        }

        public bool Update(TObject entity)
        {
            var id = entity.Id;
            if (!_data.TryGetValue(id, out _))
                return false;

            _data[id] = entity;

            SaveChanges();
            return true;
        }

        public bool Delete(TId id)
        {
            if (!_data.Remove(id))
                return false;

            SaveChanges();
            return true;
        }

        public IEnumerable<TObject> Query(Func<TObject, bool> condition)
        {
            return _data.Values.Where(condition);
        }

        #endregion Public Methods

        #region Protected Methods

        protected abstract TId GenerateNewId(TId lastId);

        protected abstract TObject ToObject(string s);

        protected abstract string ToCsv(TObject entity);

        #endregion Protected Methods

        #region Private Methods

        private void SaveChanges()
        {
            var objectStrings = _data.Select(pair => $"{pair.Key},{ToCsv(pair.Value)}");
            _dataWriter.WriteData(objectStrings);
        }

        private Dictionary<TId, TObject> ReadData()
        {
            return _dataReader
                .ReadData()
                .Skip(1)
                .Select(ToObject)
                .ToDictionary(a => a.Id);
        }

        #endregion Private Methods
    }
}