using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.GetStatus
{
    /// <summary>
    /// Ответ на запрос данных статусов сообщений
    /// </summary>
    public class GetStatusResponse : ResponseBase
    {
        public GetStatusResponse()
        {
            StatusInfo = new List<StatusInfo>();
        }

        /// <summary>
        /// Информация о СМС
        /// </summary>
        [JsonPropertyName("actions")]
        [JsonPropertyOrder(1)]
        public List<StatusInfo> StatusInfo { get; set; }
    }
}
