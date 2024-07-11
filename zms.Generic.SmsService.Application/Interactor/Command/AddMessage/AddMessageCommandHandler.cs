using zms.Common.SharedKernel.Common.Contact;
using zms.Common.SharedKernel.Common.Dating;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Application.Use.Command.AddMessage;
using zms.Generic.SmsService.Domain;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.Command.AddMessage
{
    public class AddMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IIdGenerator idGenerator) : IAddMessageCommandHandler
    {
        private readonly IMessageRepository messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        private readonly IUnitOfWork unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly IIdGenerator idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));

        public async Task<AddMessageCommandResponse> HandleAsync(AddMessageCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            ArgumentNullException.ThrowIfNull(command, nameof(command.Messages));

            var messageIdList = new List<MessageId>();
            foreach (var newMessage in command.Messages)
            {
                var message = await CreateMessage(newMessage);
                await messageRepository.AddAsync(message, unitOfWork);
                messageIdList.Add(message.Id);
            }

            await unitOfWork.CommitAsync();

            return new AddMessageCommandResponse
            {
                Id = messageIdList.Select(x=>x.Value).ToList()
            };
        }


        private async Task<Message> CreateMessage(MessageProjection messageProjection)
        {
            var recipient = new Recipient(messageProjection.Name, new PhoneNumber(messageProjection.Phone));

            var sendingPeriod = TimeInterval.Default;
            if (messageProjection.TimeStartPeriod.HasValue && messageProjection.TimeEndPeriod.HasValue)
            {
                sendingPeriod = new TimeInterval(new Time(messageProjection.TimeStartPeriod.Value), new Time(messageProjection.TimeEndPeriod.Value));
            }

            var category = Category.Get(messageProjection.Category);
            var messageId = await idGenerator.NewIdAsync<MessageId>();
            var message = new Message(messageId, DateWithTime.Current, category,  messageProjection.SenderName, messageProjection.Text, recipient, sendingPeriod);
            return message;
        }
    }
}
