using System;
using System.Globalization;
using Newtonsoft.Json;

namespace NPA.TOY
{
    internal sealed class LongAsStringJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var longValue = Convert.ToInt64(value, CultureInfo.InvariantCulture);
            writer.WriteValue(longValue.ToString(CultureInfo.InvariantCulture));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException("Cannot convert null to long.");
            }

            if (reader.TokenType == JsonToken.Integer)
            {
                return Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture);
            }

            if (reader.TokenType == JsonToken.String)
            {
                var stringValue = (string) reader.Value;
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    throw new JsonSerializationException("Cannot convert empty string to long.");
                }

                if (long.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
                {
                    return parsed;
                }
            }

            throw new JsonSerializationException($"Cannot convert token {reader.TokenType} to long.");
        }
    }
}

