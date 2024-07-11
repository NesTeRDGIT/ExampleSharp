using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using zms.Generic.SmsService.Application.Persistence.FileStorage;
using zms.Persistence.FileStorage.SmsService.Config;

namespace zms.Persistence.FileStorage.SmsService
{
    /// <summary>
    /// <inheritdoc cref="IReportTemplateGetter"/>
    /// </summary>
    public class ReportTemplateGetter(StorageLocation storageLocation) : IReportTemplateGetter
    {
        private readonly StorageLocation storageLocation = storageLocation ?? throw new ArgumentNullException(nameof(storageLocation));

        public async Task<Stream> Get(string templateFileName)
        {
            await storageLocation.Report.ScanAsync();
            var templateFile = (await storageLocation.Report.Templates.GetFileAsync(templateFileName)).FirstOrDefault();
            if (templateFile == null)
                throw new FileNotFoundException($"Файл шаблона {templateFileName} не найден в расположении {storageLocation.Report.Templates.Path}");
            var ms = new MemoryStream();
            ms.Write(templateFile.ReadAllBytes());
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
