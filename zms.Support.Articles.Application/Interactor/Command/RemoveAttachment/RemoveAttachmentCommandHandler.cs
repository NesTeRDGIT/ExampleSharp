using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.Command.RemoveAttachment;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.Command.RemoveAttachment
{
    public class RemoveAttachmentCommandHandler(
        IAttachmentRepository attachmentRepository,
        IAttachmentStorage attachmentStorage,
        IUnitOfWork unitOfWork)
        : IRemoveAttachmentCommandHandler
    {
        private readonly IAttachmentRepository attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));

        public async Task HandleAsync(RemoveAttachmentCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var attachmentId = new AttachmentId(command.Id);
            var attachment = (await attachmentRepository.GetAsync(AttachmentSpecificationFactory.ById(attachmentId), unitOfWork)).FirstOrDefault();

            if (attachment == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти вложение с идентификатором: {attachmentId.Value}");
            }

            await attachmentStorage.RemoveAsync(attachment.StorageId, attachment.GetMetadata());
            await attachmentRepository.RemoveAsync(attachment, unitOfWork);
            await unitOfWork.CommitAsync();
        }
    }
}
