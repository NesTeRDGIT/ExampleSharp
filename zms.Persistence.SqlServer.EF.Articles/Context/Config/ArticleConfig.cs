using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zms.Persistence.SqlServer.EF.Common.Extensions;
using zms.Support.Articles.Domain.OfArticle;

namespace zms.Persistence.SqlServer.EF.Articles.Context.Config
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(nameof(Article),b=>b.HasComment("Таблица статей")).HasRowVersion();

            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, l => new ArticleId(l)).IsRequired().ValueGeneratedNever().HasComment("Первичный ключ");

            builder.Property(x => x.CreatedDate).IsRequired().HasComment("Дата создания");

            builder.OwnsOne(x => x.Title, bs =>
            {
                bs.Property(x => x.Value).IsRequired().HasComment("Заголовок");
            }).Navigation(x => x.Title).IsRequired();

            builder.OwnsOne(x => x.Content, bs =>
            {
                bs.Property(x => x.Value).IsRequired().HasComment("Содержимое");
            }).Navigation(x => x.Content).IsRequired();
        }
    }
}
