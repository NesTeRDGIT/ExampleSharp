using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetArticle;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Application.Interactor.ClientQuery.GetArticle
{
    public class GetArticleQueryHandler(IGetArticleQueryObject getArticleQueryObject) : IGetArticleQueryHandler
    {
        private readonly IGetArticleQueryObject getArticleQueryObject = getArticleQueryObject ?? throw new ArgumentNullException(nameof(getArticleQueryObject));

        public async Task<GetArticleResponse> HandleAsync(GetArticleQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var articleId = new ArticleId(query.Id);
            var article = await getArticleQueryObject.GetAsync(articleId);
            if (article == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти статью с идентификатором: {articleId.Value}");
            }

            return new GetArticleResponse
            {
                Article = article
            };
        }
    }
}
