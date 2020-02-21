using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal sealed class JsonDocumentFrameworkConverter : JsonConverter<IDocumentFrameworkObject>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.GetInterfaces().Any(i => i.Equals(typeof(IDocumentFrameworkObject)));
        }

        public override IDocumentFrameworkObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.DeserializeFrameworkObject(typeToConvert, options);

        public override void Write(Utf8JsonWriter writer, IDocumentFrameworkObject value, JsonSerializerOptions options)
            => writer.SerializeFrameworkObject(value, options);
    }
}
