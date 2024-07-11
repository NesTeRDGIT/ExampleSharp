using System.Text.Json;
using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Json.Converter
{
    /// <summary>
    /// Конвектор DateTime в UnixTimestamp
    /// </summary>
    /// <remarks>Количество тиков с 01-01-1970</remarks>
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly DateTime baseDate;

        /// <summary>
        /// Часовой пояс
        /// </summary>
        /// <param name="timeZone"></param>
        public DateTimeJsonConverter(int timeZone)
        {
            baseDate = new DateTime(1970, 1, 1, timeZone, 0 ,0, DateTimeKind.Utc);
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetInt64();
            return baseDate.AddSeconds(value);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Subtract(baseDate).TotalSeconds);
        }
    }
}
