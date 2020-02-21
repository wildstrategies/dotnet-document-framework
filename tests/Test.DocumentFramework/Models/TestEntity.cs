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
    }

    public class TestValueObject : ValueObject
    {
        public string ValueTitle { get; set; }
    }
}
