using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Base.Specification;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService.Repository
{
    public class MessageRepository : IMessageRepository
    {
        public async Task AddAsync(Message message, IUnitOfWork unitOfWork)
        {
            var emailServiceContext = GetDbContext(unitOfWork);
            await emailServiceContext.Messages.AddAsync(message);
        }

        public Task RemoveAsync(Message message, IUnitOfWork unitOfWork)
        {
            var emailServiceContext = GetDbContext(unitOfWork);
            return Task.FromResult(emailServiceContext.Messages.Remove(message));
        }

        public Task UpdateAsync(Message message, IUnitOfWork unitOfWork)
        {
            return Task.CompletedTask;
        }

        public async Task<IList<Message>> GetAsync(Specification<Message> specification, IUnitOfWork unitOfWork)
        {
            var emailServiceContext = GetDbContext(unitOfWork);
            return await emailServiceContext.Messages.Where(specification.ToExpression()).ToListAsync();
        }

        public async Task<bool> ExistAsync(Specification<Message> specification, IUnitOfWork unitOfWork)
        {
            var emailServiceContext = GetDbContext(unitOfWork);
            return await emailServiceContext.Messages.AnyAsync(specification.ToExpression());
        }

        private SmsServiceContext GetDbContext(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not SmsServiceContext ctx)
                throw new InvalidCastException("Объект unitOfWork не соответствует реализации репозитория. Ожидается объект типа SmsServiceContext");
            return ctx;
        }

    }
}
