

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections
{
    /// <summary>
    /// Проекция категории
    /// </summary>
    public class CategoryProjection
    {
        /// <summary>
        /// Код категории
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public string Name { get; set; }
    }
}
