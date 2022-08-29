﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.Parse(JsonSerializer.Deserialize<string>(ref reader) ?? throw new NullReferenceException());
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.SerializeAsString());
        }
    }
}
