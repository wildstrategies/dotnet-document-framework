using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework.Models
{
    public class TestEntity : Entity
    {
        public string Title { get; init; } = null!;
        public int Value { get; init; }
        public DateTime LocalDate { get; init; } = DateTime.UtcNow;
        public TestEntity? Child { get; set; }
        public ValueObjectCollection<TestValueObject> ValueObjects { get; private set; } = new ValueObjectCollection<TestValueObject>();
        public void ResetChilds()
        {
            ValueObjects = new ValueObjectCollection<TestValueObject>();
            Child = null;
        }

        public IEnumerable<string> Enumerable { get; init; } = Array.Empty<string>();
        public IDictionary<string, string> Dictionary { get; init; } = new Dictionary<string, string>();
    }

    public class TestValueObject : ValueObject
    {
        public string? ValueTitle { get; init; }
    }
}
