namespace zms.Generic.SmsService.Application.Persistence.FileStorage
{
    /// <summary>
    /// Записыватель отчетов
    /// </summary>
    public interface IReportWriter
    {
        /// <summary>
        /// Получить поток для записи
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения отчета</param>
        /// <param name="reportDate">Дата отчета</param>
        /// <returns>Файловый поток для записи</returns>
        public Task<Stream> GetWriteStream(string fileName, DateTime reportDate);
    }
}
