using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal sealed class JsonValueObjectCollectionConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.GetGenericType<ValueObject>(typeof(ValueObjectCollection<>))?.Equals(typeToConvert) ?? false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            Type type = typeToConvert.GetGenericTypeArgument<ValueObject>() ?? throw new ArgumentNullException(nameof(typeToConvert));
            type = typeof(JsonValueObjectCollectionConverter<>).MakeGenericType(type);

            return (JsonConverter)(Activator.CreateInstance(type) ?? throw new Exception());
        }
    }
}
