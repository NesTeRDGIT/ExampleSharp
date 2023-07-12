using Microsoft.EntityFrameworkCore;
using zms.Common.Application.LightRead;
using zms.Common.Application.LightRead.Extensions;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService.QueryObject.ClientQuery
{
    public class GetMessageListItemQueryObject : IGetMessageListItemQueryObject
    {
        private readonly SmsServiceContext smsServiceContext;

        public GetMessageListItemQueryObject(SmsServiceContext smsServiceContext)
        {
            this.smsServiceContext = smsServiceContext ?? throw new ArgumentNullException(nameof(smsServiceContext));
        }

        public async Task<List<MessageProjection>> GetAsync(LightReadParams<MessageProjection> lightReadParams)
        {
            return await GetMessageProjections().Apply(lightReadParams).ToListAsync();
        }

        public async Task<long> CountAsync(LightReadParams<MessageProjection> lightReadParams)
        {
            return await GetMessageProjections().Apply(lightReadParams).CountAsync();
        }

        internal IQueryable<MessageProjection> GetMessageProjections()
        {
            return smsServiceContext.Messages
                .Select(x => new MessageProjection
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    StatusValue = x.Status.Value,
                    StatusName = x.Status.Name,
                    CategoryValue = x.Category.Value,
                    CategoryName = x.Category.Name,
                    ProcessedDate = x.ProcessedDate,
                    Comment = x.Comment,
                    ExternalId = x.ExternalId,
                    Text = x.Text,
                    SmsCount = x.SmsCount,
                    SendingDate = x.SendingDate,
                    SendingPeriod = $"{x.SendingPeriod.TimeStart.Hours:D2}:{x.SendingPeriod.TimeStart.Minutes:D2}-{x.SendingPeriod.TimeEnd.Hours:D2}:{x.SendingPeriod.TimeEnd.Minutes:D2}",// x.Key.SendingPeriod,
                    Phone = x.Recipient.Phone.Value
                });
        }
    }
}
