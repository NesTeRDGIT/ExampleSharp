namespace zms.Generic.SmsService.Application.Use.Command.AddMessage
{
    /// <summary>
    /// Проекция сообщения
    /// </summary>
    public class MessageProjection
    {
        /// <summary>
        /// Категория сообщения
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Имя получателя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Время начала периода отправки сообщения
        /// </summary>
        public TimeSpan? TimeStartPeriod { get; set; }

        /// <summary>
        /// Время окончания периода отправки сообщения
        /// </summary>
        public TimeSpan? TimeEndPeriod { get; set; }
    }
}
