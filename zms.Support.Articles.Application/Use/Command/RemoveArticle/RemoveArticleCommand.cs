using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.RemoveArticle
{
    /// <summary>
    /// Команда удаления статьи
    /// </summary>
    public class RemoveArticleCommand : ICommand
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
