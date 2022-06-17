using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    public sealed class DateOnlyJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(DateOnly);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) => new DateOnlyJsonConverter();
    }
}
