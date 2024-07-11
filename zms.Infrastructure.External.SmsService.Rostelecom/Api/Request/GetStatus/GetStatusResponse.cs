using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.GetStatus
{
    /// <summary>
    /// Ответ на запрос данных статусов сообщений
    /// </summary>
    public class GetStatusResponse : ResponseBase
    {
        /// <summary>
        /// Информация о СМС
        /// </summary>
        [JsonPropertyName("result")]
        [JsonPropertyOrder(1)]
        public StatusInfo StatusInfo { get; set; }
    }
}
