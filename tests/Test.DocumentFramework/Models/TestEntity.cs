using NodaTime;
using WildStrategies.DocumentFramework;

namespace Test.DocumentFramework.Models
{
    public class TestEntity : Entity
    {
        public string Title { get; set; }
        public int Value { get; set; }
        public LocalDate LocalDate { get; set; } = new LocalDate();
        public LocalDateTime LocalDateTime { get; set; } = new LocalDateTime();
        public Instant Instant { get; set; } = new Instant();
        public TestEntity Child { get; set; }
        public ValueObjectCollection<TestValueObject> ValueObjects { get; private set; } = new ValueObjectCollection<TestValueObject>();

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public class TestValueObject : ValueObject
    {
        public string ValueTitle { get; set; }
    }
}
