using System.Text.Json.Serialization;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.SendSms
{
    /// <summary>
    /// Запрос отправки СМС
    /// </summary>
    public class SendSmsRequest : RequestBase
    {
        public SendSmsRequest(string userName, string userPassword, IEnumerable<Sms> sms) : base(userName, userPassword, "post_sms")
        {
            Sms = new List<Sms>(sms);
        }

        /// <summary>
        /// Информация о СМС
        /// </summary>
        [JsonPropertyName("data")]
        [JsonPropertyOrder(1)]
        public List<Sms> Sms { get; set; }
    }
}
