namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.GetStatus
{
    /// <summary>
    /// Информация о СМС
    /// </summary>
    public class SmsInfo
    {
        public SmsInfo(string id)
        {
            Id = id;
        }

        /// <summary>
        /// ID рассылки сообщений
        /// </summary>
        public string Id { get; }
    }
}
