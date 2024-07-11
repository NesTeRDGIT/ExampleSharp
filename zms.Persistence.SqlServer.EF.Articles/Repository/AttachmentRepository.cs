using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Base.Specification;
using zms.Persistence.SqlServer.EF.Articles.Context;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.SqlServer.EF.Articles.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        public async Task AddAsync(Attachment attachment, IUnitOfWork unitOfWork)
        {
            var articlesContext = GetDbContext(unitOfWork);
            await articlesContext.AddAsync(attachment);
        }

        public Task RemoveAsync(Attachment attachment, IUnitOfWork unitOfWork)
        {
            var articlesContext = GetDbContext(unitOfWork);
            return Task.FromResult(articlesContext.Remove(attachment));
        }

        public Task UpdateAsync(Attachment attachment, IUnitOfWork unitOfWork)
        {
            return Task.CompletedTask;
        }

        public async Task<IList<Attachment>> GetAsync(Specification<Attachment> specification, IUnitOfWork unitOfWork)
        {
            var articlesContext = GetDbContext(unitOfWork);
            return await articlesContext.Attachments.Where(specification.ToExpression()).ToListAsync();
        }

        public async Task<bool> ExistAsync(Specification<Attachment> specification, IUnitOfWork unitOfWork)
        {
            var expertContext = GetDbContext(unitOfWork);
            return await expertContext.Attachments.Where(specification.ToExpression()).CountAsync() != 0;
        }

        private ArticlesContext GetDbContext(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not ArticlesContext ctx)
                throw new InvalidCastException("Объект unitOfWork не соответствует реализации репозитория. Ожидается объект типа ArticlesContext");
            return ctx;
        }
    }
}
