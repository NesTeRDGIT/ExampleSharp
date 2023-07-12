using zms.Common.SharedKernel.Base.Domain;
using zms.Common.SharedKernel.Common.Dating;
using zms.Common.SharedKernel.Exception;

namespace zms.Generic.SmsService.Domain.OfMessage
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class Message : AggregateRoot<MessageId>
    {
        private string comment;

        private Message()
        {
            SendingPeriod = TimeInterval.Default;
            Status = Status.New;
            Comment = string.Empty;
            ExternalId = ExternalId.Default;
            ProcessedDate = OptionalDateWithTime.Default;
            SendingDate = OptionalDateWithTime.Default;
            Provider = Provider.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="createdDate">Дата создания</param>
        /// <param name="senderName">Имя отправителя</param>
        /// <param name="text">Текст</param>
        /// <param name="sendingPeriod">Период отправки сообщения</param>
        /// <param name="category">Категория сообщения</param>
        /// <param name="recipient">Получатель</param>
        /// <exception cref="BadValueDomainException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Message(MessageId id, DateWithTime createdDate, Category category, string senderName, string text, Recipient recipient, TimeInterval sendingPeriod) : this()
        {
            ArgumentNullException.ThrowIfNull(text, nameof(text));
            if (string.IsNullOrEmpty(text))
            {
                throw new BadValueDomainException("Текст СМС сообщения не может быть пустым");
            }
            Id = id ?? throw new ArgumentNullException(nameof(id));
            SenderName = senderName ?? string.Empty;
            Text = text;
            Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            CreatedDate = createdDate ?? throw new ArgumentNullException(nameof(createdDate));
            SendingPeriod = sendingPeriod ?? throw new ArgumentNullException(nameof(sendingPeriod));
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateWithTime CreatedDate { get; }

        /// <summary>
        /// Категория сообщения
        /// </summary>
        public Category Category { get; }

        /// <summary>
        /// Период отправки сообщения
        /// </summary>
        public TimeInterval SendingPeriod { get; }

        /// <summary>
        /// Получатель
        /// </summary>
        public Recipient Recipient { get; }

        /// <summary>
        /// Имя отправителя(не обязательное поле)
        /// </summary>
        public string SenderName { get; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Статус
        /// </summary>
        public Status Status { get; private set; }

        /// <summary>
        /// Внешний идентификатор
        /// </summary>
        public ExternalId ExternalId { get; private set; }

        /// <summary>
        /// Дата обработки письма
        /// </summary>
        public OptionalDateWithTime ProcessedDate { get; private set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public OptionalDateWithTime SendingDate { get; private set; }

        /// <summary>
        /// Провайдер отправки СМС
        /// </summary>
        public Provider Provider { get; private set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get => comment;
            set => comment = value ?? string.Empty;
        }

        /// <summary>
        /// Количество отправленных СМС
        /// </summary>
        public int SmsCount { get; private set; }


        /// <summary>
        /// Сообщение в обработке
        /// </summary>
        /// <param name="processedDate">Дата обработки</param>
        /// <param name="externalId">Внешний идентификатор</param>
        /// <param name="provider">Провайдер отправки СМС</param>
        /// <param name="comment">Комментарий</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ToInProcessing(DateWithTime processedDate, ExternalId externalId, Provider provider, string comment)
        {
            if (Status != Status.New)
            {
                throw new InvalidOperationDomainException(
                    $"Только сообщение со статусом: \"{Status.New.Name}\" может быть переведено в статус: \"{Status.InProcessing.Name}\"");
            }
            ExternalId = externalId ?? throw new ArgumentNullException(nameof(externalId));
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            ProcessedDate = processedDate ?? throw new ArgumentNullException(nameof(processedDate));
            Status = Status.InProcessing;
            Comment = comment ?? string.Empty;
        }


        /// <summary>
        /// Сообщение успешно обработано
        /// </summary>
        /// <param name="sendingDate">Дата отправки</param>
        /// <param name="smsCount">Количество СМС</param>
        /// <param name="comment">Комментарий</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ToSuccess(DateWithTime sendingDate, int smsCount, string comment)
        {
            if (Status != Status.InProcessing)
            {
                throw new InvalidOperationDomainException(
                    $"Только сообщение со статусом: \"{Status.InProcessing.Name}\" может быть переведено в статус: \"{Status.Sent.Name}\"");
            }
            Status = Status.Sent;
            SendingDate = sendingDate ?? throw new ArgumentNullException(nameof(sendingDate));
            if (smsCount < 0)
            {
                throw new BadValueDomainException("Количество СМС не может быть меньше нуля");
            }
            SmsCount = smsCount;
            Comment = comment ?? string.Empty;
        }

        /// <summary>
        /// Ошибка обработки сообщения
        /// </summary>
        /// <param name="processedDate">Дата обработки</param>
        /// <param name="smsCount">Количество СМС</param>
        /// <param name="comment">Комментарий</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ToFailure(DateWithTime processedDate, int smsCount, string comment)
        {
            if (Status == Status.Sent)
            {
                throw new InvalidOperationDomainException(
                    $"Сообщение со статусом \"{Status.Sent.Name}\" не может быть переведено в статус: \"{Status.Error.Name}\"");
            }
            Status = Status.Error;
            ProcessedDate = processedDate ?? throw new ArgumentNullException(nameof(processedDate));
            Comment = comment ?? string.Empty;
            if (smsCount < 0)
            {
                throw new BadValueDomainException("Количество СМС не может быть меньше нуля");
            }
            SmsCount = smsCount;
        }
    }
}