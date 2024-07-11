namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachments
{
    /// <summary>
    /// Проекция вложений
    /// </summary>
    public class AttachmentProjection
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Тип вложения
        /// </summary>
        public AttachmentTypeProjection Type { get; set; }

        /// <summary>
        /// Наименование вложения
        /// </summary>
        public string Name { get; set; }
    }
}
