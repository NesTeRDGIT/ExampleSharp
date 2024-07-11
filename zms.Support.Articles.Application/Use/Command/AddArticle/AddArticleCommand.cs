using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.AddArticle
{
    /// <summary>
    /// Команда добавления статьи
    /// </summary>
    public class AddArticleCommand : ICommand<AddArticleResponse>
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
        /// Идентификаторы вложений, которые нужно прикрепить к статье
        /// </summary>
        public List<long> AttachAttachments { get; set; }
    }
}
