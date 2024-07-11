using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachments;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Support.Articles.Application.Interactor.ClientQuery.GetAttachments
{
    public class GetAttachmentsQueryHandler(IGetAttachmentsQueryObject getAttachmentsQueryObject) : IGetAttachmentsQueryHandler
    {
        private readonly IGetAttachmentsQueryObject getAttachmentsQueryObject = getAttachmentsQueryObject ?? throw new ArgumentNullException(nameof(getAttachmentsQueryObject));
        
        public async Task<GetAttachmentsResponse> HandleAsync(GetAttachmentsQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var articleId = new ArticleId(query.ArticleId);

            return new GetAttachmentsResponse
            {
                Attachments = await getAttachmentsQueryObject.GetAsync(articleId)
            };
        }
    }
}
