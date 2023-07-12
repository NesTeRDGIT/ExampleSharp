using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.GetStatus
{
    /// <summary>
    /// Запрос данных статусов сообщений
    /// </summary>
    public class GetStatusRequest : RequestBase
    {
        public GetStatusRequest(string userName, string userPassword, IEnumerable<SmsInfo> smsInfos) : base(userName, userPassword, "post_sms")
        {
            SmsInfo = new List<SmsInfo>(smsInfos);
        }

        /// <summary>
        /// Информация о СМС
        /// </summary>
        [JsonPropertyName("data")]
        [JsonPropertyOrder(1)]
        public List<SmsInfo> SmsInfo { get; set; }
    }
}
