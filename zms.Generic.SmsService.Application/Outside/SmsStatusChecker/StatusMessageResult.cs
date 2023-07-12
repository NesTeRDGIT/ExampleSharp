namespace zms.Generic.SmsService.Application.Outside.SmsStatusChecker
{
    /// <summary>
    /// Результат проверки статуса сообщения
    /// </summary>
    public class StatusMessageResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"> Результат обработки</param>
        /// <param name="sendingDate">Дата отправки</param>
        /// <param name="comment">Комментарий</param>
        /// <param name="smsCount">Количество отправленных СМС</param>
        public StatusMessageResult(StatusResultEnum result, DateTime? sendingDate, string comment, int smsCount)
        {
            Result = result;
            SmsCount = smsCount;
            SendingDate = sendingDate;
            Comment = comment ?? string.Empty;
        }

        /// <summary>
        /// Результат обработки
        /// </summary>
        public StatusResultEnum Result { get; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime? SendingDate { get; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Количество отправленных СМС
        /// </summary>
        public int SmsCount { get; }
    }
}
