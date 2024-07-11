namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem
{
    /// <summary>
    /// Проекция статьи
    /// </summary>
    public class ArticleListItem
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
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Часть содержимого
        /// </summary>
        public string ContentPart { get; set; }

        /// <summary>
        /// Количество вложений
        /// </summary>
        public long AttachmentCount { get; set; }
    }
}
