using zms.Support.Articles.Application.Use.ClientQuery.GetArticle;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Application.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса статьи
    /// </summary>
    public interface IGetArticleQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="articleId">Идентификатор</param>
        /// <returns></returns>
        Task<ArticleProjection> GetAsync(ArticleId articleId);
    }
}
