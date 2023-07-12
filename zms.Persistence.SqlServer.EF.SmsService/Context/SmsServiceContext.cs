using System.Reflection;
using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Common.Dating;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.Common.ValueConversion;

namespace zms.Persistence.SqlServer.EF.SmsService.Context
{
    /// <summary>
    /// Контекст БД MsSQL для сервиса отправки почты
    /// </summary>
    public class SmsServiceContext : DbContext, IUnitOfWork
    {
        public SmsServiceContext()
        {

        }

        public SmsServiceContext(DbContextOptions<SmsServiceContext> options) : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasSequence<long>("MessageHiLo").StartsAt(1).IncrementsBy(1);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<OptionalDateWithTime>().HaveConversion<OptionalDateWithTimeConverter>();
            configurationBuilder.Properties<DateWithTime>().HaveConversion<DateWithTimeConverter>();
            configurationBuilder.Properties<Time>().HaveConversion<TimeConverter>();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
