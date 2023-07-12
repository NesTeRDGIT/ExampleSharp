namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request
{
    /// <summary>
    /// Проекция СМС
    /// </summary>
    public class SmsProjection
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Время отправки в формате HH:mm-HH.mm
        /// </summary>
        public string SendingPeriod { get; set; }
    }
}
