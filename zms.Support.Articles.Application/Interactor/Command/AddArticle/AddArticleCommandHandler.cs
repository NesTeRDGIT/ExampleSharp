using zms.Common.Application.DateTimeProvider;
using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.Command.AddArticle;
using zms.Support.Articles.Domain;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.Command.AddArticle
{
    public class AddArticleCommandHandler(
        IIdGenerator idGenerator,
        IArticleRepository articleRepository,
        IAttachmentStorage attachmentStorage,
        IAttachmentRepository attachmentRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
        : IAddArticleCommandHandler
    {
        private readonly IIdGenerator idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        private readonly IArticleRepository articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        private readonly IAttachmentRepository attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IDateTimeProvider dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));

        public async Task<AddArticleResponse> HandleAsync(AddArticleCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var title = new Title(command.Title);
            var content = new Content(command.Content);
            var articleId = await idGenerator.NewIdAsync<ArticleId>();
            var article = new Article(articleId, dateTimeProvider.CurrentDateWithTime, title, content);

            await articleRepository.AddAsync(article, unitOfWork);

            if (command.AttachAttachments != null)
            {
                foreach (var attachmentCommand in command.AttachAttachments)
                {
                    var attachmentId = new AttachmentId(attachmentCommand);
                    var attachment = (await attachmentRepository.GetAsync(AttachmentSpecificationFactory.ById(attachmentId), unitOfWork)).FirstOrDefault();

                    if (attachment != null)
                    {
                        attachment.ChangeArticleId(articleId);
                        await attachmentStorage.UpdateAsync(attachment.StorageId, attachment.GetMetadata());
                        await attachmentRepository.UpdateAsync(attachment, unitOfWork);
                    }
                    else
                    {
                        throw new InvalidOperationDomainException($"Не удалось найти вложение {attachmentCommand} для прикрепления к статье");
                    }
                }
            }

            await unitOfWork.CommitAsync();

            return new AddArticleResponse
            {
                ArticleId = articleId
            };
        }
    }
}
