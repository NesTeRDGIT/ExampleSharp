using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zms.Persistence.SqlServer.EF.Articles.Converter;
using zms.Persistence.SqlServer.EF.Common.Extensions;
using zms.Support.Articles.Domain.OfArticle;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Persistence.SqlServer.EF.Articles.Context.Config
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable(nameof(Attachment),b=>b.HasComment("Таблица вложений")).HasRowVersion();

            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, l => new AttachmentId(l)).IsRequired().ValueGeneratedNever().HasComment("Первичный ключ");
            builder.Property(x => x.ArticleId).HasConversion(new ArticleIdConverter()).IsRequired(false).ValueGeneratedNever().HasComment("Внешний ключ");
            builder.Property(x => x.CreatedDate).IsRequired().HasComment("Дата создания");

            builder.OwnsOne(x => x.Name, b =>
            {
                b.Property(x => x.Value).IsRequired().HasComment("Наименование");
            }).Navigation(x=>x.Name).IsRequired();

            builder.OwnsOne(x => x.Type, b =>
            {
                b.Property(x => x.Value).IsRequired().HasComment("Код типа вложения");
                b.Property(x => x.Name).IsRequired().HasComment("Наименование типа вложения");
            }).Navigation(x => x.Type).IsRequired();

            builder.Property(x => x.StorageId).HasConversion(x => x.Value, l => new StorageId(l)).IsRequired().HasComment("Идентификатор в хранилище");

            builder.HasOne<Article>().WithMany().HasForeignKey(x => x.ArticleId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
