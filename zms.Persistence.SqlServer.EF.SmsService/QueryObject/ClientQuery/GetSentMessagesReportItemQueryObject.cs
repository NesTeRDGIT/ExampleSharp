using Microsoft.EntityFrameworkCore;
using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetSentMessagesReport;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Persistence.SqlServer.EF.SmsService.Context;

namespace zms.Persistence.SqlServer.EF.SmsService.QueryObject.ClientQuery
{
    /// <summary>
    /// <inheritdoc cref="IGetSentMessagesReportItemQueryObject"/>
    /// </summary>
    public class GetSentMessagesReportItemQueryObject : IGetSentMessagesReportItemQueryObject
    {
        private readonly SmsServiceContext smsServiceContext;

        public GetSentMessagesReportItemQueryObject(SmsServiceContext smsServiceContext)
        {
            this.smsServiceContext = smsServiceContext ?? throw new ArgumentNullException(nameof(smsServiceContext));
        }

        public async Task<IList<GetSentMessagesReportItem>> GetAsync(int year, int month)
        {
            var beginDate = new DateTime(year, month, 1);

            var query =
                from message in smsServiceContext.Messages
                where message.ProcessedDate >= beginDate
                where message.ProcessedDate < beginDate.AddMonths(1)
                group message by new { message.Category.Value, message.Category.Name }
                into grp
                select new GetSentMessagesReportItem
                {
                    CategoryValue = grp.Key.Value,
                    CategoryName = grp.Key.Name,
                    SmsCount = grp.Sum(m => m.SmsCount)
                };

            return await query.OrderBy(i => i.CategoryValue).ToListAsync();
        }
    }
}
