using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public sealed class ValueObjectCollection<TValue> : DocumentFrameworkObject, IReadOnlyCollection<TValue>
        where TValue : ValueObject
    {
        private readonly List<TValue> Items = new();

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

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var item in Items)
            {
                List<ValidationResult> errors = new List<ValidationResult>();
                var results = Validator.TryValidateObject(item, new ValidationContext(item, null, null), errors);
                foreach (var result in errors)
                {
                    yield return result;
                }
            }
        }
    }
}
