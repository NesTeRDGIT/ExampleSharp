using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.Command.RemoveArticle;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.Command.RemoveArticle
{
    public class RemoveArticleCommandHandler(IArticleRepository articleRepository, IAttachmentRepository attachmentRepository, IAttachmentStorage attachmentStorage, IUnitOfWork unitOfWork) : IRemoveArticleCommandHandler
    {
        private readonly IArticleRepository articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        private readonly IAttachmentRepository attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));

        public async Task HandleAsync(RemoveArticleCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var articleId = new ArticleId(command.Id);
            var article = (await articleRepository.GetAsync(ArticleSpecificationFactory.ById(articleId), unitOfWork)).FirstOrDefault();

            if (article == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти статью с идентификатором: {articleId.Value}");
            }

            var attachments = await attachmentRepository.GetAsync(AttachmentSpecificationFactory.ByArticleId(articleId), unitOfWork);

            foreach (var attachment in attachments)
            {
                await attachmentStorage.RemoveAsync(attachment.StorageId, attachment.GetMetadata());
                await attachmentRepository.RemoveAsync(attachment, unitOfWork);
            }

            await articleRepository.RemoveAsync(article, unitOfWork);
            await unitOfWork.CommitAsync();
        }
    }
}
