using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetMessageSummary;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetMessageSummary
{
    public class GetMessageSummaryQueryHandler : IGetMessageSummaryQueryHandler
    {
        private readonly IMessageSummaryQueryObject messageSummaryQueryObject;

        public GetMessageSummaryQueryHandler(IMessageSummaryQueryObject messageSummaryQueryObject)
        {
            this.messageSummaryQueryObject = messageSummaryQueryObject ?? throw new ArgumentNullException(nameof(messageSummaryQueryObject));
        }

        public async Task<GetMessageSummaryResponse> HandleAsync(GetMessageSummaryQuery query)
        {
            ArgumentNullException.ThrowIfNull(query,nameof(query));
            ArgumentNullException.ThrowIfNull(query.LightReadParams, nameof(query.LightReadParams));

            return new GetMessageSummaryResponse
            {
                Summary = await messageSummaryQueryObject.GetAsync(query.LightReadParams)
            };
        }
    }
}
