using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetSentMessagesReport
{
    /// <summary>
    /// Ответ на запрос получения отчета отправленных сообщений
    /// </summary>
    public class GetSentMessagesReportResponse : IResponse
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public byte[] Data { get; set; }
    }
}
