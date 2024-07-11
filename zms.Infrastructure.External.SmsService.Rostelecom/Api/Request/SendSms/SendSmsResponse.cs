using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms
{
    /// <summary>
    /// Ответ на запрос отправки СМС
    /// </summary>
    public class SendSmsResponse : ResponseBase
    {
        /// <summary>
        /// Данные о сообщении
        /// </summary>
        [JsonPropertyName("result")]
        [JsonPropertyOrder(1)]
        public SmsStatus MessageInfo { get; set; }
    }
}
