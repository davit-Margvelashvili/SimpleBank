using System;
using System.Collections.Generic;
using SimpleBank.Core.Models;
using SimpleBank.Core.Models.Abstractions;

namespace SimpleBank.Core.Data.Repositories.Abstractions
{
    public interface IRepository<TObject, TId> where TObject : IDomainObject<TId>
    {
        TObject GetById(TId id);

        TId Add(TObject entity);

        bool Update(TObject entity);

        bool Delete(TId id);

        IEnumerable<TObject> Query(Func<TObject, bool> where);
    }
}