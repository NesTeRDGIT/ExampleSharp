using System;
using System.IO;
using System.Threading.Tasks;
using zms.Generic.SmsService.Application.Persistence.FileStorage;
using zms.Persistence.FileStorage.SmsService.Config;

namespace zms.Persistence.FileStorage.SmsService
{
    /// <summary>
    /// <inheritdoc cref="IReportWriter"/>
    /// </summary>
    public class ReportWriter : IReportWriter
    {
        private readonly StorageLocation storageLocation;

        public ReportWriter(StorageLocation storageLocation)
        {
            this.storageLocation = storageLocation ?? throw new ArgumentNullException(nameof(storageLocation));
        }

        public async Task<Stream> GetWriteStream(string fileName, DateTime reportDate)
        {
            var saveFile = await storageLocation.Report.Reports.ByMonth(reportDate).CreateFileAsync(fileName);
            return saveFile.GetWriteStream();
        }
    }
}
