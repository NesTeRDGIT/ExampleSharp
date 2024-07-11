using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms
{
    /// <summary>
    /// Информация о статусе СМС
    /// </summary>
    public class SmsStatus
    {
        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        [JsonPropertyName("uid")]
        [JsonPropertyOrder(1)]
        public string MessageId { get; set; }
    }
}
