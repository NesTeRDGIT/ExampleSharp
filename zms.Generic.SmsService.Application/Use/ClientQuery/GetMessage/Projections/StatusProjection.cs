

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections
{
    /// <summary>
    /// Проекция статуса
    /// </summary>
    public class StatusProjection
    {
        /// <summary>
        /// Код статуса
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string Name { get; set; }
    }
}
