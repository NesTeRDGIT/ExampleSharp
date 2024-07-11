using Microsoft.Extensions.Logging;
using zms.Common.Application.DateTimeProvider;
using zms.Generic.SmsService.Application.Outside.SmsSender;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Application.Use.Command.SendMessage;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.Command.SendMessage
{
    public class SendMessageCommandHandler : ISendMessageCommandHandler
    {

        private readonly IMessageRepository messageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISmsSender smsSender;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ILogger<SendMessageCommandHandler> logger;

        public SendMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, ISmsSender smsSender, IDateTimeProvider dateTimeProvider, ILogger<SendMessageCommandHandler> logger)
        {
            this.messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.smsSender = smsSender ?? throw new ArgumentNullException(nameof(smsSender));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<SendMessageCommandResponse> HandleAsync(SendMessageCommand command)
        {
            var messages = await messageRepository.GetAsync(
                MessageSpecificationFactory.ByStatus(Status.New) & 
                MessageSpecificationFactory.TimePeriodInclude(dateTimeProvider.CurrentDateWithTime.GetTime()), 
                unitOfWork);
            if (messages.Count != 0)
            {
                await smsSender.ConnectAsync();
                foreach (var message in messages)
                {
                    try
                    {
                        var result = await smsSender.SendAsync(message);
                        switch (result.Result)
                        {
                            case SendResultEnum.Success:
                                message.ToInProcessing(dateTimeProvider.CurrentDateWithTime, new ExternalId(result.ExternalId), new Provider(result.ProviderName), result.Comment);
                                break;
                            case SendResultEnum.Error:
                                message.ToFailure(dateTimeProvider.CurrentDateWithTime, 0, result.Comment);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, $"Ошибка отправки СМС сообщения: {message.Id.Value}");
                    }
                    await messageRepository.UpdateAsync(message, unitOfWork);
                }
                await smsSender.DisconnectAsync();
            }
            await unitOfWork.CommitAsync();
            return new SendMessageCommandResponse();
        }
    }
}
