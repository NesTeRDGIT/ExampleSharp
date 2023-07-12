using zms.Generic.SmsService.Application.Use.Command.CheckStatusMessage;
using zms.Generic.SmsService.Application.Use.Command.SendMessage;

namespace zms.Root.Worker.SmsService
{
    /// <summary>
    /// Сервис отправки СМС
    /// </summary>
    public class SmsService
    {
        private readonly ILogger<SmsService> logger;
        private readonly ISendMessageCommandHandler sendMessageCommandHandler;
        private readonly ICheckStatusMessageCommandHandler checkStatusMessageCommandHandler;

        public SmsService(ILogger<SmsService> logger,ISendMessageCommandHandler sendMessageCommandHandler, ICheckStatusMessageCommandHandler checkStatusMessageCommandHandler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.sendMessageCommandHandler = sendMessageCommandHandler ?? throw new ArgumentNullException(nameof(sendMessageCommandHandler));
            this.checkStatusMessageCommandHandler = checkStatusMessageCommandHandler ?? throw new ArgumentNullException(nameof(checkStatusMessageCommandHandler));
        }

        public async Task Run()
        {
            try
            {
                
                await sendMessageCommandHandler.HandleAsync(new SendMessageCommand());
                await checkStatusMessageCommandHandler.HandleAsync(new CheckStatusMessageCommand());
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Ошибка в SmsService");
            }
        }
    }
}
