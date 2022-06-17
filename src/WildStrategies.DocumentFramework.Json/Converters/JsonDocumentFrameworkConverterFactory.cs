using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal sealed class JsonDocumentFrameworkConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.GetInterfaces().Any(i => i.Equals(typeof(IDocumentFrameworkObject)));
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)(Activator.CreateInstance(
                typeof(JsonDocumentFrameworkConverter<>).MakeGenericType(typeToConvert)
            ) ?? throw new Exception());
        }
    }
}
