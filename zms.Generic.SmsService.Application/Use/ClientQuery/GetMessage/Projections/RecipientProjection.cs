namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections
{
    /// <summary>
    /// Проекция получателя
    /// </summary>
    public class RecipientProjection
    {
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Имя получателя
        /// </summary>
        public string Name { get; set; }
    }
}
