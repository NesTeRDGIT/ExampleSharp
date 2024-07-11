using zms.Common.Application.DateTimeProvider;
using zms.Common.SharedKernel.Common.Dating;
using zms.Common.SharedKernel.Exception;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Application.Use.Command.SetMessageError;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.Command.SetMessageError
{
    /// <summary>
    /// <inheritdoc cref="ISetMessageErrorCommandHandler"/>
    /// </summary>
    public class SetMessageErrorCommandHandler : ISetMessageErrorCommandHandler
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;
        private const string mark = "(отмечено как ошибка пользователем)";

        public SetMessageErrorCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            this.messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<SetMessageErrorResponse> HandleAsync(SetMessageErrorCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            ArgumentNullException.ThrowIfNull(command.Id);
            var message = (await messageRepository.GetAsync(MessageSpecificationFactory.ById(new MessageId(command.Id)), unitOfWork)).FirstOrDefault();
            if (message != null)
            {
                message.ToFailure(message.ProcessedDate.HasValue ? new DateWithTime(message.ProcessedDate.Value!.Value) : dateTimeProvider.CurrentDateWithTime, message.SmsCount, $"{message.Comment.Replace(mark, string.Empty)}(отмечено как ошибка пользователем)");
                await unitOfWork.CommitAsync();
            }
            else
            {
                throw new EntityNotExistDomainException($"Не удалось найти сообщение с идентификатором: {command.Id}");
            }
            
            return new SetMessageErrorResponse();
        }
    }
}
