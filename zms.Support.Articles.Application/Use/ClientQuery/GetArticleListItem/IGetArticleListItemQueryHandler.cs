using zms.Common.Application.Base.Cqrs.OfCollectionQuery;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem
{
    /// <summary>
    /// Обработчик запроса списка статей
    /// </summary>
    public interface IGetArticleListItemQueryHandler : ICollectionQueryHandler<GetArticleListItemQuery, ArticleListItem>;
}
