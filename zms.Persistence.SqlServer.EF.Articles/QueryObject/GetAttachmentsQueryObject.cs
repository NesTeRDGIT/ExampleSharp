using Microsoft.EntityFrameworkCore;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachments;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Persistence.SqlServer.EF.Articles.QueryObject
{
    public class GetAttachmentsQueryObject(ArticlesContext articlesContext) : IGetAttachmentsQueryObject
    {
        private readonly ArticlesContext articlesContext = articlesContext ?? throw new ArgumentNullException(nameof(articlesContext));

        public async Task<IList<AttachmentProjection>> GetAsync(ArticleId articleId)
        {
            var query =
                from attachment in articlesContext.Attachments
                where attachment.ArticleId == articleId
                select new AttachmentProjection
                {
                    Id = attachment.Id,
                    ArticleId = attachment.ArticleId,
                    Name = attachment.Name.Value,
                    Type = new AttachmentTypeProjection
                    {
                        Value = attachment.Type.Value,
                        Name = attachment.Type.Name
                    }
                };

            return await query.ToListAsync();
        }
    }
}
