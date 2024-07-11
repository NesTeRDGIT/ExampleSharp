using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetSentMessagesReport
{
    /// <summary>
    /// Запрос на получение отчета отправленных сообщений
    /// </summary>
    public class GetSentMessagesReportQuery : IQuery<GetSentMessagesReportResponse>
    {
        /// <summary>
        /// Год отчета
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Месяц отчета
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime ContractDate { get; set; }

        /// <summary>
        /// Должность ответственного лица
        /// </summary>
        public string ResponsiblePost { get; set; }

        /// <summary>
        /// ФИО ответственного лица
        /// </summary>
        public string ResponsibleName { get; set; }
    }
}
