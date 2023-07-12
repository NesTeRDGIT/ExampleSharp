namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections
{
    /// <summary>
    /// Проекция электронного письма
    /// </summary>
    public class MessageProjection
    {
        /// <summary>
        /// Идентификация
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public StatusProjection Status { get; set; }

        /// <summary>
        /// Имя провайдера
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public CategoryProjection Category { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Период отправки сообщения
        /// </summary>
        public TimeIntervalProjection SendingPeriod { get; set; }

        /// <summary>
        /// Дата обработки
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Количество потраченных СМС
        /// </summary>
        public int SmsCount { get; set; }

        /// <summary>
        /// Дата отправки СМС
        /// </summary>
        public DateTime? SendingDate { get; set; }

        /// <summary>
        /// Внешний идентификатор
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public RecipientProjection Recipient { get; set; }
    }
}
