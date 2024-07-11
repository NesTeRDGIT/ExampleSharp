using zms.Common.Application.DateTimeProvider;
using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.Command.AddAttachment;
using zms.Support.Articles.Domain;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.Command.AddAttachment
{
    public class AddAttachmentCommandHandler(
        IIdGenerator idGenerator,
        IAttachmentRepository attachmentRepository,
        IArticleRepository articleRepository,
        IDateTimeProvider dateTimeProvider,
        IAttachmentStorage attachmentStorage,
        IUnitOfWork unitOfWork)
        : IAddAttachmentCommandHandler
    {
        private readonly IIdGenerator idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        private readonly IArticleRepository articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        private readonly IAttachmentRepository attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IDateTimeProvider dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));

        public async Task<AddAttachmentResponse> HandleAsync(AddAttachmentCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var name = new Name(command.Name);
            var type = command.ArticleId.HasValue ? AttachmentType.Default : AttachmentType.System;
            var articleId = command.ArticleId.HasValue ? new ArticleId(command.ArticleId.Value) : ArticleId.Default;
            if (articleId != ArticleId.Default && !await articleRepository.ExistAsync(ArticleSpecificationFactory.ById(articleId), unitOfWork))
            {
                throw new EntityNotExistDomainException($"Не удалось найти статью с идентификатором: {articleId.Value}");
            }

            var attachmentMetadata = new AttachmentMetadata(articleId, command.Name);
            var storageId = await attachmentStorage.SendAsync(command.Data, attachmentMetadata);
            var attachmentId = await idGenerator.NewIdAsync<AttachmentId>();
            var attachment = new Attachment(attachmentId, articleId, storageId, type, dateTimeProvider.CurrentDateWithTime, name);
            await attachmentRepository.AddAsync(attachment, unitOfWork);
            await unitOfWork.CommitAsync();

            return new AddAttachmentResponse
            {
                AttachmentId = attachmentId.Value
            };
        }
    }
}
