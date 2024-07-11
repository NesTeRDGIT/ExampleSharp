using zms.Common.Application.DateTimeProvider;
using zms.Common.SharedKernel.Common.Dating;
using zms.Generic.SmsService.Application.Outside.Report;
using zms.Generic.SmsService.Application.Persistence.FileStorage;
using zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery;
using zms.Generic.SmsService.Application.Use.ClientQuery.GetSentMessagesReport;

namespace zms.Generic.SmsService.Application.Interactor.ClientQuery.GetSentMessagesReport
{
    /// <summary>
    /// <inheritdoc cref="IGetSentMessagesReportQueryHandler"/>
    /// </summary>
    public class GetSentMessagesReportQueryHandler : IGetSentMessagesReportQueryHandler
    {
        private readonly IGetSentMessagesReportItemQueryObject getSentMessagesReportItemQueryObject;
        private readonly ISentMessagesReportBuilder sentMessagesReportBuilder;
        private readonly IReportTemplateGetter reportTemplateGetter;
        private readonly IReportWriter reportWriter;
        private readonly IDateTimeProvider dateTimeProvider;

        public GetSentMessagesReportQueryHandler(
            IGetSentMessagesReportItemQueryObject getSentMessagesReportItemQueryObject,
            ISentMessagesReportBuilder getSentMessagesReportBuilder,
            IReportTemplateGetter reportTemplateGetter,
            IReportWriter reportWriter,
            IDateTimeProvider dateTimeProvider)
        {
            this.getSentMessagesReportItemQueryObject = getSentMessagesReportItemQueryObject ?? throw new ArgumentNullException(nameof(getSentMessagesReportItemQueryObject));
            this.sentMessagesReportBuilder = getSentMessagesReportBuilder ?? throw new ArgumentNullException(nameof(getSentMessagesReportBuilder));
            this.reportTemplateGetter = reportTemplateGetter ?? throw new ArgumentNullException(nameof(reportTemplateGetter));
            this.reportWriter = reportWriter;
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public async Task<GetSentMessagesReportResponse> HandleAsync(GetSentMessagesReportQuery query)
        {
            var items = await getSentMessagesReportItemQueryObject.GetAsync(query.Year, query.Month);

            return new GetSentMessagesReportResponse
            {
                Data = await sentMessagesReportBuilder.Build(new SentMessagesReportData
                {
                    Items = items,
                    Month = Month.Get(query.Month).Name,
                    Year = query.Year,
                    ContractNumber = query.ContractNumber,
                    ContractDate = query.ContractDate,
                    ReportDate = dateTimeProvider.CurrentDate.Value,
                    SmsTotalCount = items.Sum(i => i.SmsCount),
                    ResponsiblePost = query.ResponsiblePost,
                    ResponsibleName = query.ResponsibleName,
                },
                reportTemplateGetter.Get,
                fileName => reportWriter.GetWriteStream(fileName, dateTimeProvider.CurrentDate.Value)),
                FileName = $"Отчет отправленных СМС за {query.Month:D2}_{query.Year}.{DateTime.Now.Ticks}.docx"
            };
        }
    }
}
