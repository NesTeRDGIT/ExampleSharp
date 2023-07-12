namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetStatus
{
    /// <summary>
    /// Проекция статуса
    /// </summary>
    public class StatusProjection
    {
        /// <summary>
        /// Код
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
