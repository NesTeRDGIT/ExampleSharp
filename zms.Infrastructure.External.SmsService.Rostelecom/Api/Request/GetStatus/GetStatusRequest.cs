using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.Common;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.GetStatus
{
    /// <summary>
    /// Запрос данных статусов сообщений
    /// </summary>
    public class GetStatusRequest : RequestBase
    {
        public GetStatusRequest(SmsInfo smsInfo) 
        {
            SmsInfo = smsInfo ?? throw new ArgumentNullException(nameof(smsInfo));
        }



        /// <summary>
        /// Информация о СМС
        /// </summary>
        public SmsInfo SmsInfo { get; set; }
    }
}
