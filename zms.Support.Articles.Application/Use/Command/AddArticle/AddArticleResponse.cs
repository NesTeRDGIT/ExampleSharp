using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.AddArticle
{
    /// <summary>
    /// Результат команды добавления статьи
    /// </summary>
    public class AddArticleResponse : IResponse
    {
        /// <summary>
        /// Идентификатор новой статьи
        /// </summary>
        public long ArticleId { get; set; }
    }
}
