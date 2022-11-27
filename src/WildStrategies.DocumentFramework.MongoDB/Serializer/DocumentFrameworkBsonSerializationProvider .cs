using MongoDB.Bson.Serialization;
using System.Collections.Concurrent;

namespace WildStrategies.DocumentFramework.Serializer
{
    internal class DocumentFrameworkBsonSerializationProvider : IBsonSerializationProvider
    {
        private static readonly ConcurrentDictionary<Type, IBsonSerializer> _cache = new ConcurrentDictionary<Type, IBsonSerializer>();

        public IBsonSerializer? GetSerializer(Type type)
        {
            IBsonSerializer? output = null;
            if (_cache.TryGetValue(type, out var cachedSerializer))
                return cachedSerializer;

            if (type == typeof(DateOnly))
                output = new DateOnlySerializer();
            if (type == typeof(TimeOnly))
                output = new TimeOnlySerializer();

            if (
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                && type.GetGenericArguments().Length == 2
                && type.GetGenericArguments()[0] == typeof(Guid)
            )
            {
                Type constructed = typeof(GuidKeyDictionarySerializer<>).MakeGenericType(type.GetGenericArguments()[1]);
                output = ((IBsonSerializer?)Activator.CreateInstance(constructed)) ?? throw new NullReferenceException();
            }

            if (output != null)
                _cache.TryAdd(type, output);
            return output;
        }
    }
}
