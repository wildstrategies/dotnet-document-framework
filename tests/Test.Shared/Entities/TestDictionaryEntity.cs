using WildStrategies.DocumentFramework;

namespace Test.Shared.Models
{
    public class TestDictionaryEntity : Entity
    {
        public Dictionary<string, string> MetadataSimple { get; init; } = null!;

        public TestEntityMetadata MetadataObject { get; init; } = null!;

        public TestEntityStringObjectDictionary<TestObject> StringObjectDictionary { get; init; } = null!;

        public Dictionary<Guid, int> GuidIntDictionary { get; init; } = null!;

        public Dictionary<Guid, TestObject> GuidObjectDictionary { get; init; } = null!;
    }
}
