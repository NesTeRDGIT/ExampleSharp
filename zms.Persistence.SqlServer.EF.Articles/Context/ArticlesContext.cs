using System.Reflection;
using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Common.Dating;
using zms.Persistence.SqlServer.EF.Common.ValueConversion;
using zms.Support.Articles.Application.Persistence.Repository;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.SqlServer.EF.Articles.Context
{
    /// <summary>
    /// Контекст БД SQL для обращений
    /// </summary>
    public class ArticlesContext : DbContext, IUnitOfWork
    {
        public ArticlesContext()
        {
         
        }

        public ArticlesContext(DbContextOptions<ArticlesContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.HasSequence<long>("ArticleHiLo").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<long>("AttachmentHiLo").StartsAt(1).IncrementsBy(1);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<OptionalDate>().HaveConversion<OptionalDateConverter>();
            configurationBuilder.Properties<Date>().HaveConversion<DateConverter>();
            configurationBuilder.Properties<DateWithTime>().HaveConversion<DateWithTimeConverter>();
            configurationBuilder.Properties<OptionalDateWithTime>().HaveConversion<OptionalDateWithTimeConverter>();
        }

        /// <summary>
        /// Статьи
        /// </summary>
        public virtual DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        public virtual DbSet<Attachment> Attachments { get; set; }


        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
