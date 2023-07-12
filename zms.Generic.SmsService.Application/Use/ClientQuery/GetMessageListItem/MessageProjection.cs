namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem
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
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Код статуса
        /// </summary>
        public string StatusValue { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Текст СМС
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата обработки
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Внешний идентификатор
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Код категории
        /// </summary>
        public string CategoryValue { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Количество затраченных СМС
        /// </summary>
        public int SmsCount { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime? SendingDate { get; set; }

        /// <summary>
        /// Временной период отправки сообщения
        /// </summary>
        public string SendingPeriod { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }
    }
}
