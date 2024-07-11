namespace zms.Generic.SmsService.Application.Outside.Report
{
    /// <summary>
    /// Построитель отчета отправленных сообщений
    /// </summary>
    public interface ISentMessagesReportBuilder
    {
        /// <summary>
        /// Получить файл отчета
        /// </summary>
        /// <param name="data">Данные отчета</param>
        /// <param name="templateFactory">Фабрика шаблонов - получение файлового потока шаблона по имени файла</param>
        /// <param name="reportWriterFactory">Фабрика сохранения отчетов - получение файлового потока для записи данных отчета в файл с указанным именем</param>
        /// <returns>Массив байтов</returns>
        public Task<byte[]> Build(SentMessagesReportData data, Func<string, Task<Stream>> templateFactory, Func<string, Task<Stream>> reportWriterFactory);
    }
}
