using Microsoft.EntityFrameworkCore;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessage.Projections;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService.QueryObject.ClientQuery
{
    public class GetMessageQueryObject : IGetMessageQueryObject
    {
        private readonly SmsServiceContext smsServiceContext;

        public GetMessageQueryObject(SmsServiceContext smsServiceContext)
        {
            this.smsServiceContext = smsServiceContext ?? throw new ArgumentNullException(nameof(smsServiceContext));
        }

        public async Task<MessageProjection> GetAsync(MessageId id)
        {
            return await smsServiceContext.Messages.Where(x => x.Id == id).Select(x => new MessageProjection
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                SenderName = x.SenderName,
                Provider = x.Provider.Name,
                Status = new StatusProjection
                {
                    Value = x.Status.Value,
                    Name = x.Status.Name
                },
                Category = new CategoryProjection
                {
                    Value = x.Category.Value,
                    Name = x.Category.Name
                },
                ProcessedDate = x.ProcessedDate,
                ExternalId = x.ExternalId,
                Text = x.Text,
                SmsCount = x.SmsCount,
                SendingDate = x.SendingDate,
                SendingPeriod = new TimeIntervalProjection
                {
                    TimeStart = $"{x.SendingPeriod.TimeStart.Hours:D2}:{x.SendingPeriod.TimeStart.Minutes:D2}",
                    TimeEnd = $"{x.SendingPeriod.TimeEnd.Hours:D2}:{x.SendingPeriod.TimeEnd.Minutes:D2}"
                },
                Comment = x.Comment,
                Recipient = new RecipientProjection
                {
                    Name = x.Recipient.Name,
                    Phone = x.Recipient.Phone.Value
                }
            }).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
