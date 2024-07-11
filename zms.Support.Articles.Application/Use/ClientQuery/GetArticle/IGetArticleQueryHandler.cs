using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticle
{
    /// <summary>
    /// Обработчик запроса статьи
    /// </summary>
    public interface IGetArticleQueryHandler : IQueryHandler<GetArticleQuery, GetArticleResponse>;
}
