using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticle
{
    /// <summary>
    /// Запрос статьи
    /// </summary>
    public class GetArticleQuery : IQuery<GetArticleResponse>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
