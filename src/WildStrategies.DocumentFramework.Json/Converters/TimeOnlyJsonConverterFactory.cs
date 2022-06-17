using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    public sealed class TimeOnlyJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(TimeOnly);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) => new TimeOnlyJsonConverter();
    }
}
