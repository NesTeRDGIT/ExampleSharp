using zms.Common.Application.Base.Cqrs.OfCollectionQuery;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticleListItem;

namespace zms.Support.Articles.Application.Interactor.ClientQuery.GetArticleListItem
{
    public class GetArticleListItemQueryHandler(IGetArticleListItemQueryObject getArticleListItemQueryObject) : IGetArticleListItemQueryHandler
    {
        private readonly IGetArticleListItemQueryObject getArticleListItemQueryObject = getArticleListItemQueryObject ?? throw new ArgumentNullException(nameof(getArticleListItemQueryObject));

        public async Task<CollectionResponse<ArticleListItem>> HandleAsync(GetArticleListItemQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            return query.QueryingPaginationMetadata
                ? new CollectionResponse<ArticleListItem>(
                    await getArticleListItemQueryObject.GetAsync(query.LightReadParams),
                    await getArticleListItemQueryObject.GetCountAsync(query.LightReadParams)
                )
                : new CollectionResponse<ArticleListItem>(
                    await getArticleListItemQueryObject.GetAsync(query.LightReadParams));
        }
    }
}
