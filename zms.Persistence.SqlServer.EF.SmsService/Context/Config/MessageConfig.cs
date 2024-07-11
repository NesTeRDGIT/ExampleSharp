using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.Common.Extensions;

namespace zms.Persistence.SqlServer.EF.SmsService.Context.Config
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(nameof(Message),b=>b.HasComment("Таблица СМС сообщений")).HasRowVersion();

            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).HasConversion(x => x.Value, l => new MessageId(l)).IsRequired().ValueGeneratedNever().HasComment("Первичный ключ");

            builder.Property(x => x.CreatedDate).IsRequired().HasComment("Дата создания");
            builder.OwnsOne(x => x.Category, bs =>
            {
                bs.Property(x => x.Value).IsRequired().HasComment("Код категории сообщения");
                bs.Property(x => x.Name).IsRequired().HasComment("Наименование категории сообщения");
            }).Navigation(x => x.Category).IsRequired();

            builder.OwnsOne(x => x.Status, bs =>
            {
                bs.Property(x => x.Value).IsRequired().HasComment("Код статуса");
                bs.Property(x => x.Name).IsRequired().HasComment("Наименование статуса");
            }).Navigation(x => x.Status).IsRequired();

            builder.OwnsOne(x => x.SendingPeriod, bs =>
            {
                bs.Property(x => x.TimeStart).IsRequired().HasComment("Начало периода времени отправки");
                bs.Property(x => x.TimeEnd).IsRequired().HasComment("Начало периода времени отправки");
            }).Navigation(x => x.SendingPeriod).IsRequired();

            builder.OwnsOne(x => x.Provider, bs =>
            {
                bs.Property(x => x.Name).IsRequired().HasComment("Наименование провайдера отправки");
            }).Navigation(x => x.Provider).IsRequired();

            builder.Property(x => x.Text).IsRequired().HasComment("Тема сообщения");
            builder.Property(x => x.SenderName).IsRequired().HasComment("Имя отправителя");

            builder.OwnsOne(x => x.Recipient, bs =>
            {
                bs.Property(x => x.Name).IsRequired().HasComment("Имя получателя");
                bs.OwnsOne(x => x.Phone, eb =>
                {
                    eb.Property(x => x.Value).IsRequired().HasComment("Номер телефона");
                }).Navigation(x => x.Phone).IsRequired();
            }).Navigation(x => x.Recipient).IsRequired();

            builder.Property(x => x.SmsCount).IsRequired().HasComment("Количество потраченных СМС на сообщение");

            builder.Property(x => x.ProcessedDate).IsRequired(false).HasComment("Дата обработки");
            builder.Property(x => x.SendingDate).IsRequired(false).HasComment("Дата отправки");
            
            builder.Property(x => x.ExternalId).IsRequired().HasConversion(x=>x.Value, s => new ExternalId(s)).HasComment("Внешний идентификатор");
            builder.Property(x => x.Comment).IsRequired().HasComment("Комментарий");
        }
    }
}
