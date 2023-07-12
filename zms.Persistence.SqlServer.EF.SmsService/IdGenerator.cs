using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Base.Domain;
using zms.Generic.SmsService.Domain;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.Common;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService
{
    /// <summary>
    /// Генератор ID
    /// </summary>
    public class IdGenerator : IdGeneratorBase, IIdGenerator 
    {
        private readonly DbContextOptions<SmsServiceContext> options;

        public IdGenerator(DbContextOptions<SmsServiceContext> options)
        {
            this.options = options;
            AddKeyParameter(typeof(MessageId), "MessageHiLo");
        }

        public T NewId<T>() where T : IEntityId
        {
            using var context = new SmsServiceContext(options);
            return NewId<T>(context);
        }
    }
}
