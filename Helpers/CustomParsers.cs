using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace retail_bff.Helpers
{
    public class TimeSpanJSONConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            var stringTimespan = value.ToString(); // 23:59:00
            var stringWithoutSeconds = stringTimespan.Substring(0, stringTimespan.Length - 3); // 23:59

            writer.WriteStringValue(stringWithoutSeconds);
        }
    }
}