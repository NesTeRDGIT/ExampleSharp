using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetSentMessagesReport;

namespace zms.Generic.SmsService.Application.Persistence.QueryObject.ClientQuery
{
    /// <summary>
    /// Объект запроса элементов отчета отправленных сообщений
    /// </summary>
    public interface IGetSentMessagesReportItemQueryObject
    {
        /// <summary>
        /// Получить элементы отчета отправленных сообщений за месяц и год
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="month">Месяц</param>
        /// <returns>Список элементов отчета</returns>
        public Task<IList<GetSentMessagesReportItem>> GetAsync(int year, int month);
    }
}
