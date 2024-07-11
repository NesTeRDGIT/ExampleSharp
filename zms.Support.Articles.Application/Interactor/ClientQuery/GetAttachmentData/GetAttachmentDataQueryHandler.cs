using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.QueryObject;
using zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.ClientQuery.GetAttachmentData
{
    public class GetAttachmentDataQueryHandler(IGetAttachmentDataQueryObject getAttachmentDataQueryObject) : IGetAttachmentDataQueryHandler
    {
        private readonly IGetAttachmentDataQueryObject getAttachmentDataQueryObject = getAttachmentDataQueryObject ?? throw new ArgumentNullException(nameof(getAttachmentDataQueryObject));

        public async Task<GetAttachmentDataResponse> HandleAsync(GetAttachmentDataQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var attachmentId = new AttachmentId(query.Id);
            var attachment = await getAttachmentDataQueryObject.GetAsync(attachmentId);

            if (attachment == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти вложение с идентификатором: {attachmentId.Value}");
            }

            return new GetAttachmentDataResponse
            {
                Attachment = attachment
            };
        }
    }
}
