using zms.Common.Application.Base.Cqrs;
using zms.Common.SharedKernel.Exception;
using zms.Support.Articles.Application.Persistence.AttachmentStorage;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Application.Use.Command.UpdateArticle;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Interactor.Command.UpdateArticle
{
    public class UpdateArticleCommandHandler(
        IArticleRepository articleRepository, 
        IAttachmentRepository attachmentRepository, 
        IAttachmentStorage attachmentStorage,
        IUnitOfWork unitOfWork) : IUpdateArticleCommandHandler
    {
        private readonly IArticleRepository articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        private readonly IAttachmentRepository attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
        private readonly IAttachmentStorage attachmentStorage = attachmentStorage ?? throw new ArgumentNullException(nameof(attachmentStorage));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task HandleAsync(UpdateArticleCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var articleId = new ArticleId(command.Id);
            var article = (await articleRepository.GetAsync(ArticleSpecificationFactory.ById(articleId), unitOfWork)).FirstOrDefault();
            if (article == null)
            {
                throw new EntityNotExistDomainException($"Не удалось найти статью с идентификатором: {articleId.Value}");
            }

            command.Title.Perform(v => article.Title = new Title(v));
            command.Content.Perform(v => article.Content = new Content(v));

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

            await articleRepository.UpdateAsync(article, unitOfWork);
            await unitOfWork.CommitAsync();
        }
    }
}
