using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.UpdateArticle
{
    /// <summary>
    /// Команда обновления статьи
    /// </summary>
    public class UpdateArticleCommand : ICommand
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public UpdateField<string> Title { get; set; } = new();

        /// <summary>
        /// Содержимое
        /// </summary>
        public UpdateField<string> Content { get; set; } = new();

        /// <summary>
        /// Идентификаторы вложений, которые нужно прикрепить к статье
        /// </summary>
        public List<long> AttachAttachments { get; set; }
    }
}
