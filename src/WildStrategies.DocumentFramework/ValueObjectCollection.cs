using System.Collections;
using System.Collections.Generic;

namespace WildStrategies.DocumentFramework
{
    public sealed class ValueObjectCollection<TValue> : BaseDocumentFrameworkObject, IReadOnlyCollection<TValue>
        where TValue : ValueObject
    {
        private readonly List<TValue> Items = new List<TValue>();

        public int Count => Items.Count;

        public IEnumerator<TValue> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public ValueObjectCollection<TValue> Add(TValue value)
        {
            Items.Add(value);
            return this;
        }
    }
}
