using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Exception;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.SqlServer.EF.Articles.QueryObject
{
    public class GetAttachmentDataQueryObject(ArticlesContext articlesContext, IAttachmentStorage attachmentStorage)
        : IGetAttachmentDataQueryObject
    {
        private readonly ArticlesContext articlesContext = articlesContext ?? throw new ArgumentNullException(nameof(articlesContext));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));


        public async Task<AttachmentDataProjection> GetAsync(AttachmentId attachmentId)
        {
            var query =
                from attachment in articlesContext.Attachments
                where attachment.Id == attachmentId
                select new 
                {
                    attachment.Id,
                    attachment.Name,
                    attachment.StorageId
                };
            var result = await query.AsNoTracking().FirstOrDefaultAsync();
            if (result == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти вложение с идентификатором: {attachmentId.Value}");
            }

            var stream = await attachmentStorage.GetReadStreamAsync(result.StorageId);

            return new AttachmentDataProjection
            {
                Id = result.Id,
                Name = result.Name.Value,
                Stream = stream
            };
        }
    }
}
