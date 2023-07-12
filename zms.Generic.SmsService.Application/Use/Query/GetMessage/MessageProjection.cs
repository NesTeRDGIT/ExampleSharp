namespace zms.Generic.SmsService.Application.Use.Query.GetMessage
{
    /// <summary>
    /// Проекция сообщения
    /// </summary>
    public class MessageProjection
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата обработки
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Значение статуса
        /// </summary>
        public string StatusValue { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }
}
