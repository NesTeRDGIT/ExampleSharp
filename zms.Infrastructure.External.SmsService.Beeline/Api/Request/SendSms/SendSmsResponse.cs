using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.SendSms
{
    /// <summary>
    /// Ответ на запрос отправки СМС
    /// </summary>
    public class SendSmsResponse : ResponseBase
    {
        public SendSmsResponse()
        {
            StatusInfo = new List<SmsStatus>();
        }

        /// <summary>
        /// Информация о СМС
        /// </summary>
        [JsonPropertyName("actions")]
        [JsonPropertyOrder(1)]
        public List<SmsStatus> StatusInfo { get; set; }
    }
}
