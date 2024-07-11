namespace zms.UI.ApiControllers.Articles.V1.Model
{
    /// <summary>
    /// Модель добавления статьи
    /// </summary>
    public class ArticleModel
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Вложения прикрепляемые к статье
        /// </summary>
        public List<long> AttachAttachments { get; set; }
    }
}
