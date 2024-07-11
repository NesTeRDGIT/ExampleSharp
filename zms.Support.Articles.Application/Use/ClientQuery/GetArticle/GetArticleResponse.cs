using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticle
{
    /// <summary>
    /// Ответ на запрос статьи
    /// </summary>
    public class GetArticleResponse : IResponse
    {
        /// <summary>
        /// Статья
        /// </summary>
        public ArticleProjection Article { get; set; }
    }
}
