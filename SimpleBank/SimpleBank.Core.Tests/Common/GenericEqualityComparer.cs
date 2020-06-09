using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleBank.Core.Tests.Common
{
    internal class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equalityFunction;

        public GenericEqualityComparer(Func<T, T, bool> equalityFunction)
        {
            _equalityFunction = equalityFunction;
        }

        public bool Equals(T x, T y)
        {
            return _equalityFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}