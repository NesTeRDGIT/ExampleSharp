using Microsoft.Extensions.Logging;
using zms.Common.Application.DateTimeProvider;
using zms.Generic.SmsService.Application.Outside.SmsStatusChecker;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Application.Use.Command.CheckStatusMessage;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.Command.CheckStatusMessage
{
    public class CheckStatusMessageCommandHandler : ICheckStatusMessageCommandHandler
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISmsStatusChecker smsStatusChecker;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ILogger<CheckStatusMessageCommandHandler> logger;

        public CheckStatusMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, ISmsStatusChecker smsStatusChecker, IDateTimeProvider dateTimeProvider, ILogger<CheckStatusMessageCommandHandler> logger)
        {
            this.messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.smsStatusChecker = smsStatusChecker ?? throw new ArgumentNullException(nameof(smsStatusChecker));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CheckStatusMessageCommandResponse> HandleAsync(CheckStatusMessageCommand command)
        {
            var messages = await messageRepository.GetAsync(MessageSpecificationFactory.ByStatus(Status.InProcessing), unitOfWork);
            if (messages.Count != 0)
            {
                await smsStatusChecker.ConnectAsync();
                foreach (var message in messages)
                {
                    try
                    {
                        var result = await smsStatusChecker.GetStatusAsync(message);
                        switch (result.Result)
                        {
                            case StatusResultEnum.None:
                                if (message.Comment != result.Comment)
                                {
                                    message.Comment = result.Comment;
                                }
                                break;
                            case StatusResultEnum.Success:
                                message.ToSuccess(result.SendingDate ?? dateTimeProvider.CurrentDateWithTime, result.SmsCount, result.Comment);
                                break;
                            case StatusResultEnum.Error:
                                message.ToFailure(result.SendingDate ?? dateTimeProvider.CurrentDateWithTime, result.SmsCount, result.Comment);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        await messageRepository.UpdateAsync(message, unitOfWork);
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, $"Ошибка проверки статуса СМС сообщения: {message.Id.Value}");
                    }
                }
                await smsStatusChecker.DisconnectAsync();
            }
            await unitOfWork.CommitAsync();
            return new CheckStatusMessageCommandResponse();
        }
    }
}
