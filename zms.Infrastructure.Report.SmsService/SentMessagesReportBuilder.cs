using System.Globalization;
using zms.Generic.SmsService.Application.Outside.Report;
using zms.Infrastructure.Utility.Word;

namespace zms.Infrastructure.Report.SmsService
{
    /// <summary>
    /// <inheritdoc cref="ISentMessagesReportBuilder"/>
    /// </summary>
    public class SentMessagesReportBuilder : ISentMessagesReportBuilder
    {
        public async Task<byte[]> Build(SentMessagesReportData data, Func<string, Task<Stream>> templateFactory, Func<string, Task<Stream>> reportWriterFactory)
        {
            var templateFileName = "SentMessagesReport.docx";
            using var doc = Document.Open(await templateFactory(templateFileName));

            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");

            doc.ReplaceText([
                ("<месяц>", data.Month.ToLower()),
                ("<год>", data.Year.ToString()),
                ("<номер договора>", data.ContractNumber),
                ("<дата договора>", $"{data.ContractDate.ToShortDateString()} г."),
                ("<дата формирования отчета>", data.ReportDate.ToString("dd MMMM yyyy г.")),
                ("<должность>", data.ResponsiblePost),
                ("<ФИО>", data.ResponsibleName),
                ("<итого>", data.SmsTotalCount.ToString())
            ], true);

            var table = doc.FindTable("<пп>");
            table.FillTable(data.Items, new()
            {
                { "<пп>", d => $"{data.Items.IndexOf(d) + 1}" },
                { "<код>", d => d.CategoryValue },
                { "<текст>", d => d.CategoryName },
                { "<количество>", d => $"{d.SmsCount}" },
            });

            var fileName = templateFileName.Replace(".docx", $"_{data.ReportDate.Day:D2}.{data.ReportDate.Month:D2}.{data.ReportDate.Year}.docx");
            await using var stream = await reportWriterFactory(fileName);
            doc.Save();
            using var copyDocument =  doc.Clone(stream);
            return doc.GetBytes();
        }
    }
}
