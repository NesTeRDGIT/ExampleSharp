namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetSentMessagesReport
{
    /// <summary>
    /// Элемент отчета отправленных сообщений
    /// </summary>
    public class GetSentMessagesReportItem
    {
        /// <summary>
        /// Код категории
        /// </summary>
        public string CategoryValue { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Количество смс-сообщений
        /// </summary>
        public int SmsCount { get; set; }
    }
}
