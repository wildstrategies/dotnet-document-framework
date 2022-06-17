using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal sealed class JsonDocumentFrameworkConverter<TObject> : JsonConverter<TObject> where TObject : IDocumentFrameworkObject
    {
        public override TObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (TObject)reader.DeserializeFrameworkObject(typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, TObject value, JsonSerializerOptions options)
        {
            writer.SerializeFrameworkObject(value, options);
        }
    }
}
