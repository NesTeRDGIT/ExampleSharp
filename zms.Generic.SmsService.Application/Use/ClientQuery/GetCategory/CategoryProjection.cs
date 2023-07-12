namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetCategory
{
    /// <summary>
    /// Проекция категории
    /// </summary>
    public class CategoryProjection
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
