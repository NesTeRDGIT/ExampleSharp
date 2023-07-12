using zms.Common.SharedKernel.Common.Contact;
using zms.Common.SharedKernel.Common.Dating;
using zms.Generic.SmsService.Application.Persistence.Repository;
using zms.Generic.SmsService.Application.Use.Command.AddMessage;
using zms.Generic.SmsService.Domain;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Generic.SmsService.Application.Interactor.Command.AddMessage
{
    public class AddMessageCommandHandler : IAddMessageCommandHandler
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IIdGenerator idGenerator;

        public AddMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IIdGenerator idGenerator)
        {
            this.messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        }

        public async Task<AddMessageCommandResponse> HandleAsync(AddMessageCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            ArgumentNullException.ThrowIfNull(command, nameof(command.Messages));

            var messageIdList = new List<long>();
            foreach (var newMessage in command.Messages.Select(CreateMessage))
            {
                await messageRepository.AddAsync(newMessage, unitOfWork);
                messageIdList.Add(newMessage.Id);
            }

            await unitOfWork.CommitAsync();

            return new AddMessageCommandResponse
            {
                Id = messageIdList
            };
        }


        private Message CreateMessage(MessageProjection messageProjection)
        {
            var recipient = new Recipient(messageProjection.Name, new PhoneNumber(messageProjection.Phone));

            var sendingPeriod = TimeInterval.Default;
            if (messageProjection.TimeStartPeriod.HasValue && messageProjection.TimeEndPeriod.HasValue)
            {
                sendingPeriod = new TimeInterval(new Time(messageProjection.TimeStartPeriod.Value), new Time(messageProjection.TimeEndPeriod.Value));
            }

            var category = Category.Get(messageProjection.Category);
            var messageId = idGenerator.NewId<MessageId>();
            var message = new Message(messageId, DateWithTime.Current, category,  messageProjection.SenderName, messageProjection.Text, recipient, sendingPeriod);
            return message;
        }
    }
}
