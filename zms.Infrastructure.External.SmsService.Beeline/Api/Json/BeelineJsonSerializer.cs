using System.Text.Json;
using zms.Infrastructure.External.SmsService.Beeline.Api.Json.Converter;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Json
{
    /// <summary>
    /// Сериализатор объектов c учетом форматов Beeline
    /// </summary>
    public class BeelineJsonSerializer
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public BeelineJsonSerializer()
        {
            jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default);
            jsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
            jsonSerializerOptions.Converters.Add(new DateTimeNullableJsonConverter());
            jsonSerializerOptions.Converters.Add(new DecimalJsonConverter());
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
