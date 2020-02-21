using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WildStrategies.DocumentFramework
{
    internal class JsonDocumentConverter<TEntity> : JsonConverter<Document<TEntity>> where TEntity : Entity
    {
        public override Document<TEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string rootPropertyName = nameof(Document<TEntity>.Root);

            var values = reader.ReadValues(typeToConvert, options);

            Document<TEntity> output = DocumentHelper.CreateDocument((TEntity)(values.ContainsKey(rootPropertyName) ? values[rootPropertyName] : null));
            foreach (System.Reflection.PropertyInfo property in typeof(Document<TEntity>)
                .GetSerializableProperties().Where(x =>
                    x.CanWrite
                    && !x.Name.Equals(rootPropertyName, StringComparison.InvariantCulture)
                    && values.ContainsKey(x.Name)
                )
            )
            {
                property.SetValue(output, values[property.Name]);
            }

            return output;
        }

        public override void Write(Utf8JsonWriter writer, Document<TEntity> value, JsonSerializerOptions options)
            => writer.SerializeFrameworkObject(value, options);
    }
}
