namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticle
{
    /// <summary>
    /// Проекция статьи
    /// </summary>
    public class ArticleProjection
    {
        /// <summary>
        /// Id пакета
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
        /// Содержимое
        /// </summary>
        public string Content { get; set; }
    }
}
