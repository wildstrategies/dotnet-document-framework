using System.Text.Json;

namespace WildStrategies.DocumentFramework
{
    public abstract class JsonDocumentSerializerBase
    {
        private static JsonSerializerOptions? _serializerOptions;

        protected static JsonSerializerOptions SerializerOptions
        {
            get
            {
                if (_serializerOptions == null)
                {
                    _serializerOptions = new JsonSerializerOptions()
                    {
                        IgnoreReadOnlyProperties = false,
                        PropertyNameCaseInsensitive = false
                    };

                    //_serializerOptions.Converters.Add(new JsonDocumentConverterFactory());
                    _serializerOptions.Converters.Add(new JsonValueObjectCollectionConverterFactory());
                    _serializerOptions.Converters.Add(new JsonDocumentFrameworkConverterFactory());
                }

                return _serializerOptions;
            }
        }
    }

    /// <summary>
    ///     Serialize and deserialize documents in JSON format
    /// </summary>
    public sealed class JsonDocumentSerializer : JsonDocumentSerializerBase, IEntitySerializer<string>
    {
        public TEntity Deserialize<TEntity>(string serialized) where TEntity : Entity
        {
            return JsonSerializer.Deserialize<TEntity>(serialized, SerializerOptions) ?? throw new ArgumentException(nameof(serialized));
        }

        public string Serialize<TEntity>(TEntity entity) where TEntity : Entity
        {
            return JsonSerializer.Serialize(entity, typeof(TEntity), SerializerOptions);
        }
    }

    public static class JsonDocumentSerializerExtension
    {
        private static readonly JsonDocumentSerializer serializer = new JsonDocumentSerializer();

        public static TEntity FromJson<TEntity>(this string serialized) where TEntity : Entity
        {
            return serializer.Deserialize<TEntity>(serialized);
        }

        public static string ToJson<TEntity>(this TEntity entity) where TEntity : Entity
        {
            return serializer.Serialize<TEntity>(entity);
        }
    }
}
