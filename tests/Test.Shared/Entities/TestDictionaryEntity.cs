using WildStrategies.DocumentFramework;

namespace Test.Shared.Models
{
    public class TestDictionaryEntity : Entity
    {
        public Dictionary<string, string> MetadataSimple { get; init; } = new Dictionary<string, string>()
        {
            { "Test1", "Value1" },
            { "Test2", "Value2" },
            { "Test3", "Value3" }
        };

        public TestEntityMetadata MetadataObject { get; init; } = new TestEntityMetadata()
        {
            { "Test1", "Value1" },
            { "Test2", "Value2" },
            { "Test3", "Value3" }
        };

        public TestEntityStringObjectDictionary<TestObject> StringObjectDictionary = new TestEntityStringObjectDictionary<TestObject>()
        {
            { "Object1", new TestObject() },
            { "Object2", new TestObject() },
            { "Object3", new TestObject() },
        };

        public Dictionary<Guid, int> GuidIntDictionary = new Dictionary<Guid, int>()
        {
            { Guid.NewGuid(), 1 },
            { Guid.NewGuid(), 2 },
            { Guid.NewGuid(), 3 },
        };

        public Dictionary<Guid, TestObject> GuidObjectDictionary = new Dictionary<Guid, TestObject>()
        {
            { Guid.NewGuid(), new TestObject(){ Name="Name1" } },
            { Guid.NewGuid(), new TestObject(){ Name="Name2" } },
            { Guid.NewGuid(), new TestObject(){ Name="Name3" } },
            { Guid.NewGuid(), new TestObject(){ Name="Name4" } },
            { Guid.NewGuid(), new TestObject(){ Name="Name5" } }
        };
    }
}
