using System;
using System.Collections;
using System.Collections.Generic;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture
{
    public class ConvertedEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<object> _enumerable;

        public ConvertedEnumerable(IEnumerable<object> enumerable)
        {
            _enumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _enumerable)
            {
                if (item is T variable) yield return variable;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}