using zms.Common.Application.DateTimeProvider;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.Command.PruneAttachment;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.Command.PruneAttachment
{
    public class PruneAttachmentCommandHandler(
        IAttachmentRepository attachmentRepository,
        IDateTimeProvider dateTimeProvider,
        IAttachmentStorage attachmentStorage,
        IUnitOfWork unitOfWork) : IPruneAttachmentCommandHandler
    {
        private readonly IAttachmentRepository attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));

        public async Task<PruneAttachmentResponse> HandleAsync(PruneAttachmentCommand command)
        {
            var lastDate = dateTimeProvider.CurrentDateWithTime.AddHours(-48);
            var attachments = await attachmentRepository.GetAsync(AttachmentSpecificationFactory.ByArticleId(ArticleId.Default) & AttachmentSpecificationFactory.CreatedBefore(lastDate), unitOfWork);

            foreach (var attachment in attachments)
            {
                await attachmentStorage.RemoveAsync(attachment.StorageId, attachment.GetMetadata());
                await attachmentRepository.RemoveAsync(attachment, unitOfWork);
            }

            await unitOfWork.CommitAsync();
            return new PruneAttachmentResponse
            {
                Count = attachments.Count
            };
        }
    }
}
