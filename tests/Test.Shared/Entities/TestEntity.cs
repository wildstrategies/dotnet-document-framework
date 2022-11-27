using System.ComponentModel.DataAnnotations;
using WildStrategies.DocumentFramework;

namespace Test.Shared.Models
{
    public class TestEntity : TestDictionaryEntity
    {
        [Required] public string Title { get; set; } = null!;
        public int Value { get; set; }
        public DateTime LocalDate { get; set; } = DateTime.UtcNow;
        public TestEntity? Child { get; set; }
        [Required] public TestSubentity SubEntity { get; set; } = null!;
        public ValueObjectCollection<TestValueObject> ValueObjects { get; private set; } = new ValueObjectCollection<TestValueObject>();
        public void ResetChilds()
        {
            ValueObjects = new ValueObjectCollection<TestValueObject>();
            Child = null;
        }

        public TestObject? NotValidatableObject { get; set; }

        public IEnumerable<TestSubentity> Subentities { get; set; } = Array.Empty<TestSubentity>();

        public IEnumerable<string> Enumerable { get; set; } = Array.Empty<string>();
        public IDictionary<string, string> Dictionary { get; set; } = new Dictionary<string, string>();

        [Required] public DateOnly dateOnly { get; init; } = DateOnly.MinValue;
        [Required] public TimeOnly timeOnly { get; init; } = TimeOnly.MinValue;
        public DateOnly? NullableDateOnly { get; init; }
        public TimeOnly? NullableTimeOnly { get; init; }

        public DateOnly? NullableDateOnlyWithValue { get; init; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly? NullableTimeOnlyWithValue { get; init; } = TimeOnly.FromDateTime(DateTime.Now);

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = base.Validate(validationContext);
            return results;
        }
    }
}
