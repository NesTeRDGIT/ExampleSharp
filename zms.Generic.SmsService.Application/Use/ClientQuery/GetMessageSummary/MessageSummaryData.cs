namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary
{
    /// <summary>
    /// Краткая информация о сообщениях
    /// </summary>
    public class MessageSummaryData
    {
        /// <summary>
        /// Всего сообщений
        /// </summary>
        public int TotalMessage { get; set; }

        /// <summary>
        /// Всего СМС
        /// </summary>
        public int TotalSms { get; set; }

        /// <summary>
        /// Новых сообщений
        /// </summary>
        public int NewMessage { get; set; }

        /// <summary>
        /// Новых СМС
        /// </summary>
        public int NewSms { get; set; }

        /// <summary>
        /// В обработке сообщений
        /// </summary>
        public int InProcessingMessage { get; set; }

        /// <summary>
        /// В обработке СМС
        /// </summary>
        public int InProcessingSms { get; set; }

        /// <summary>
        /// Отправлено сообщений
        /// </summary>
        public int SentMessage { get; set; }

        /// <summary>
        /// Отправлено СМС
        /// </summary>
        public int SentSms { get; set; }

        /// <summary>
        /// Ошибки отправки сообщений
        /// </summary>
        public int ErrorMessage { get; set; }

        /// <summary>
        /// Ошибки отправки СМС
        /// </summary>
        public int ErrorSms { get; set; }
    }
}
