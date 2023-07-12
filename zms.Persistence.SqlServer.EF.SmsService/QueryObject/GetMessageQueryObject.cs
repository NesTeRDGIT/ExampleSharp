using Microsoft.EntityFrameworkCore;
using zms.Generic.SmsService.Application.Persistence.QueryObject;
using zms.Generic.SmsService.Application.Use.Query.GetMessage;
using zms.Generic.SmsService.Domain.OfMessage;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService.QueryObject
{
    public class GetMessageQueryObject : IGetMessageQueryObject
    {
        private readonly SmsServiceContext smsServiceContext;

        public GetMessageQueryObject(SmsServiceContext smsServiceContext)
        {
            this.smsServiceContext = smsServiceContext ?? throw new ArgumentNullException(nameof(smsServiceContext));
        }

        public async Task<IList<MessageProjection>> GetAsync(IList<MessageId> id)
        {
            return await smsServiceContext.Messages.Where(x => id.Contains(x.Id)).Select(x => new MessageProjection
            {
                Id = x.Id,
                ProcessedDate = x.ProcessedDate,
                StatusValue = x.Status.Value,
                StatusName = x.Status.Name,
                Comment = x.Comment
            }).ToListAsync();
        }
    }
}
