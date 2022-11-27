namespace Test.Shared.Models
{
    public class TestEntityStringObjectDictionary<T> : Dictionary<string, T> where T : class
    {
        public DateOnly DateOnly { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
