using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Base.Domain;
using zms.Generic.SmsService.Domain;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.Common.IdGeneration;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService
{
    /// <summary>
    /// Генератор ID
    /// </summary>
    public class IdGenerator : EntityIdGeneratorBase, IIdGenerator 
    {
        private readonly DbContextOptions<SmsServiceContext> options;

        public IdGenerator(DbContextOptions<SmsServiceContext> options)
        {
            this.options = options;
            AddKeyParameter(typeof(MessageId), "MessageHiLo");
        }

        public async Task<TEntityId> NewIdAsync<TEntityId>() where TEntityId : IEntityId
        {
            await using var context = new SmsServiceContext(options);
            return await NewId<TEntityId>(context);
        }
    }
}
