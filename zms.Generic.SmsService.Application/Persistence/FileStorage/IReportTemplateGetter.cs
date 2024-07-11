namespace zms.Generic.SmsService.Application.Persistence.FileStorage
{
    /// <summary>
    /// Получатель шаблонов отчетов
    /// </summary>
    public interface IReportTemplateGetter
    {
        /// <summary>
        /// Получить поток шаблона по имени файла
        /// </summary>
        /// <param name="templateFileName">Имя файла</param>
        /// <returns>Файловый поток шаблона</returns>
        public Task<Stream> Get(string templateFileName);
    }
}
