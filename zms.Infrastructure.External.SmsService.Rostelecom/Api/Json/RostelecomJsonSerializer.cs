using System.Text.Json;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Json.Converter;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Json
{
    /// <summary>
    /// Сериализатор объектов c учетом форматов Rostelecom
    /// </summary>
    public class RostelecomJsonSerializer
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public RostelecomJsonSerializer(RostelecomOptions options)
        {
            jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default);
            jsonSerializerOptions.Converters.Add(new DateTimeJsonConverter(options.TimeZone));
        }

        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, jsonSerializerOptions);
        }

        public T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, jsonSerializerOptions);
        }
    }
}
