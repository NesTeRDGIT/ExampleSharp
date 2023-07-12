using Microsoft.EntityFrameworkCore;
using zms.Common.Application.LightRead;
using zms.Common.Application.LightRead.Extensions;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageListItem;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary;
using zms.Generic.SmsService.Domain.OfMessage;

namespace zms.Persistence.SqlServer.EF.SmsService.QueryObject.ClientQuery
{
    public class MessageSummaryQueryObject : IMessageSummaryQueryObject
    {
        private readonly GetMessageListItemQueryObject getMessageListItemQueryObject;

        public MessageSummaryQueryObject(GetMessageListItemQueryObject getMessageListItemQueryObject)
        {
            this.getMessageListItemQueryObject = getMessageListItemQueryObject ?? throw new ArgumentNullException(nameof(getMessageListItemQueryObject));
        }


        public async Task<MessageSummaryData> GetAsync(LightReadParams<MessageProjection> lightReadParams)
        {
            var summary =  await getMessageListItemQueryObject.GetMessageProjections().ApplyFilter(lightReadParams).GroupBy(x => 0)
                .Select(g => new MessageSummaryData
                {
                    TotalMessage = g.Count(),
                    TotalSms = g.Select(x => x.SmsCount).Sum(),
                    NewMessage = g.Select(x => x.StatusValue == Status.New.Value ? 1 : 0).Sum(),
                    NewSms = g.Select(x => x.StatusValue == Status.New.Value ? x.SmsCount : 0).Sum(),
                    InProcessingMessage = g.Select(x => x.StatusValue == Status.InProcessing.Value ? 1 : 0).Sum(),
                    InProcessingSms = g.Select(x => x.StatusValue == Status.InProcessing.Value ? x.SmsCount : 0).Sum(),
                    SentMessage = g.Select(x => x.StatusValue == Status.Sent.Value ? 1 : 0).Sum(),
                    SentSms = g.Select(x => x.StatusValue == Status.Sent.Value ? x.SmsCount : 0).Sum(),
                    ErrorMessage = g.Select(x => x.StatusValue == Status.Error.Value ? 1 : 0).Sum(),
                    ErrorSms = g.Select(x => x.StatusValue == Status.Error.Value ? x.SmsCount : 0).Sum()
                }).FirstOrDefaultAsync();
            return summary ?? new MessageSummaryData();
        }
    }
}
