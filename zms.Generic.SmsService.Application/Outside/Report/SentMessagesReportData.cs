using zms.Generic.SmsService.Application.Interactor.ClientQuery.GetSentMessagesReport;

namespace zms.Generic.SmsService.Application.Outside.Report
{
    /// <summary>
    /// Данные отчета отправленных сообщений
    /// </summary>
    public class SentMessagesReportData
    {
        /// <summary>
        /// Месяц
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime ContractDate { get; set; }

        /// <summary>
        /// Дата формирования отчета
        /// </summary>
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// Итого количество смс
        /// </summary>
        public int SmsTotalCount { get; set; }

        /// <summary>
        /// Должность ответственного лица
        /// </summary>
        public string ResponsiblePost { get; set; }

        /// <summary>
        /// ФИО ответственного лица
        /// </summary>
        public string ResponsibleName { get; set; }

        /// <summary>
        /// Элементы таблицы
        /// </summary>
        public IList<GetSentMessagesReportItem> Items { get; set; }
    }
}
