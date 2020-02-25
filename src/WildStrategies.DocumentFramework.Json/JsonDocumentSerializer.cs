using NodaTime.Serialization.SystemTextJson;
using System.Text.Json;

namespace WildStrategies.DocumentFramework
{
    public abstract class JsonDocumentSerializerBase
    {
        private static JsonSerializerOptions _serializerOptions;

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

                    _serializerOptions.Converters.Add(new JsonDocumentConverterFactory());
                    _serializerOptions.Converters.Add(new JsonDocumentFrameworkConverterFactory());
                    _serializerOptions.Converters.Add(new JsonValueObjectCollectionConverterFactory());
                    _serializerOptions.ConfigureForNodaTime(NodaTime.DateTimeZoneProviders.Tzdb);
                }

                return _serializerOptions;
            }
        }
    }

    /// <summary>
    ///     Serialize and deserialize documents in JSON format
    /// </summary>
    public sealed class JsonDocumentSerializer : JsonDocumentSerializerBase, IDocumentSerializer<string>
    {
        public Document<TEntity> Deserialize<TEntity>(string serialized) where TEntity : Entity
        {
            return JsonSerializer.Deserialize<Document<TEntity>>(serialized, SerializerOptions);
        }

        public string Serialize<TEntity>(Document<TEntity> document) where TEntity : Entity
        {
            return JsonSerializer.Serialize(document, typeof(Document<TEntity>), SerializerOptions);
        }
    }

    public static class JsonDocumentSerializerExtension
    {
        private static readonly JsonDocumentSerializer serializer = new JsonDocumentSerializer();

        public static Document<TEntity> FromJson<TEntity>(this string serialized) where TEntity : Entity
        {
            return serializer.Deserialize<TEntity>(serialized);
        }

        public static string ToJson<TEntity>(this Document<TEntity> document) where TEntity : Entity
        {
            return serializer.Serialize<TEntity>(document);
        }
    }
}
