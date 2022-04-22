using System.ComponentModel.DataAnnotations;
using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework.Models
{
    public class TestEntity : Entity
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

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = base.Validate(validationContext);
            return results;
        }
    }
}
